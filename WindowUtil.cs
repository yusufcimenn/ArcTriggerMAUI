using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace ArcTriggerMAUI
{
    public static class WindowUtil
    {
        // Basic fixed size
        public static Task ResizeAsync(double w, double h, bool center = true, bool lockResize = false)
            => ResizeCoreAsync(w, h, center, lockResize);

        // Portrait responsive: base 450x650, use a fraction of screen
        public static async Task ResizePortraitResponsiveAsync(
            double baseW = 400, double baseH = 650,
            double widthRatio = 0.32, double heightRatio = 0.82,
            double minW = 300, double minH = 500,
            bool center = true, bool lockResize = false)
        {
            var (workW, workH) = GetWorkAreaDip();
            var maxW = workW * widthRatio;
            var maxH = workH * heightRatio;

            var scaleW = maxW / baseW;
            var scaleH = maxH / baseH;
            var scale = System.Math.Min(scaleW, scaleH);

            var w = System.Math.Max(baseW * scale, minW);
            var h = System.Math.Max(baseH * scale, minH);

            await ResizeCoreAsync(w, h, center, lockResize);
        }

        // Landscape responsive: base 1200x360, use a fraction of screen
        public static async Task ResizeLandscapeResponsiveAsync(
            double baseW = 1920, double baseH = 200,
            double widthRatio = 0.80, double heightRatio = 0.35,
            double minW = 1600, double minH = 200,
            bool center = true, bool lockResize = false)
        {
            var (workW, workH) = GetWorkAreaDip();
            var maxW = workW * widthRatio;
            var maxH = workH * heightRatio;

            var scaleW = maxW / baseW;
            var scaleH = maxH / baseH;
            var scale = System.Math.Min(scaleW, scaleH);

            var w = System.Math.Max(baseW * scale, minW);
            var h = System.Math.Max(baseH * scale, minH);

            await ResizeCoreAsync(w, h, center, lockResize);
        }

        // Get work area size in device independent pixels
        private static (double widthDip, double heightDip) GetWorkAreaDip()
        {
#if WINDOWS
            var win = Application.Current?.Windows.FirstOrDefault();
            if (win?.Handler?.PlatformView is Microsoft.UI.Xaml.Window native)
            {
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(native);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                var area = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(
                    windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                return (area.WorkArea.Width, area.WorkArea.Height); // effective pixels
            }
#endif
            // Fallback for other platforms
            var info = DeviceDisplay.Current.MainDisplayInfo;
            var wDip = info.Width / info.Density;
            var hDip = info.Height / info.Density;
            return (wDip, hDip);
        }

        private static async Task ResizeCoreAsync(double w, double h, bool center, bool lockResize)
        {
            var win = Application.Current?.Windows.FirstOrDefault();
            if (win == null) return;

            win.Width = w;
            win.Height = h;

#if WINDOWS
            // wait handler
            int tries = 0;
            while (win.Handler?.PlatformView == null && tries++ < 10)
                await Task.Delay(50);

            if (win.Handler?.PlatformView is Microsoft.UI.Xaml.Window native)
            {
                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(native);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                var appWin = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                if (appWin != null)
                {
                    appWin.Resize(new Windows.Graphics.SizeInt32((int)w, (int)h));

                    if (center)
                    {
                        var area = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(
                            appWin.Id, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                        var x = area.WorkArea.X + (area.WorkArea.Width - (int)w) / 2;
                        var y = area.WorkArea.Y + (area.WorkArea.Height - (int)h) / 2;
                        appWin.Move(new Windows.Graphics.PointInt32(x, y));
                    }

                    if (lockResize && appWin.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
                        p.IsResizable = false;

                    // optional: make native background transparent so outer white does not show
                    // native.Content.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                }
            }
#endif
        }
    }
}
