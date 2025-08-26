using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace ArcTriggerMAUI
{
    public static partial class WindowUtil
    {
        // Resize a specific window instance
        public static async Task ResizeAsync(Window target, double w, double h, bool center = true, bool lockResize = false)
        {
            if (target == null) return;

            target.Width = w;
            target.Height = h;

#if WINDOWS
            int tries = 0;
            while (target.Handler?.PlatformView == null && tries++ < 10)
                await Task.Delay(50);

            if (target.Handler?.PlatformView is Microsoft.UI.Xaml.Window native)
            {
                var hwnd     = WinRT.Interop.WindowNative.GetWindowHandle(native);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                var appWin   = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                if (appWin != null)
                {
                    appWin.Resize(new Windows.Graphics.SizeInt32((int)w, (int)h));

                    if (center)
                    {
                        var area = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(
                            appWin.Id, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
                        var x = area.WorkArea.X + (area.WorkArea.Width  - (int)w) / 2;
                        var y = area.WorkArea.Y + (area.WorkArea.Height - (int)h) / 2;
                        appWin.Move(new Windows.Graphics.PointInt32(x, y));
                    }

                    if (lockResize && appWin.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
                        p.IsResizable = false;
                }
            }
#endif
        }
    }
}
