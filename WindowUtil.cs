using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace ArcTriggerMAUI  // kendi namespace'in buysa dokunma
{
    public static class WindowUtil
    {
        // center: true ise pencere ekranda ortalanir
        // lockResize: true ise pencere elde buyutulemez/kucultulemez (Windows)
        public static async Task ResizeAsync(double w, double h, bool center = true, bool lockResize = false)
        {
            var win = Application.Current?.Windows.FirstOrDefault();
            if (win == null) return;

            // Apply size on MAUI side immediately
            win.Width = w;
            win.Height = h;

#if WINDOWS
            // Wait until native handler is ready
            int tries = 0;
            while (win.Handler?.PlatformView == null && tries++ < 10)
                await Task.Delay(50);

            if (win.Handler?.PlatformView is Microsoft.UI.Xaml.Window native)
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
