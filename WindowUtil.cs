using System.Threading.Tasks;
using Microsoft.Maui.Controls;

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

                    if (lockResize && appWin.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p1)
                        p1.IsResizable = false;
                }
            }
#endif
        }

        // Make a window borderless and disable maximize
        public static async Task MakeBorderlessFixedAsync(Window target, bool allowMinimize = true)
        {
#if WINDOWS
    if (target == null) return;

    // Handler hazır olana kadar bekle
    int tries = 0;
    while (target.Handler?.PlatformView == null && tries++ < 10)
        await Task.Delay(50);

    if (target.Handler?.PlatformView is Microsoft.UI.Xaml.Window native)
    {
        var hwnd     = WinRT.Interop.WindowNative.GetWindowHandle(native);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWin   = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
        if (appWin == null) return;

        // Standart başlık şeridini gizle ve butonları şeffaf yap
        appWin.TitleBar.ExtendsContentIntoTitleBar = true;
        appWin.TitleBar.BackgroundColor = Microsoft.UI.Colors.Transparent;
        appWin.TitleBar.InactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
        appWin.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
        appWin.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;

        // Yeniden boyutlandırma / büyütme kapalı, minimize opsiyonel
        if (appWin.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
        {
            p.IsResizable   = false;
            p.IsMaximizable = false;
            p.IsMinimizable = allowMinimize;
        }

        // İçerik kökünün arka planını şeffaf/koyu yap (UIElement değilse)
        var brush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);

        if (native.Content is Microsoft.UI.Xaml.Controls.Panel panel)
            panel.Background = brush;
        else if (native.Content is Microsoft.UI.Xaml.Controls.ContentPresenter presenter)
            presenter.Background = brush;
        else if (native.Content is Microsoft.UI.Xaml.Controls.Control control)
            control.Background = brush;
        // not: UIElement'te Background yok; o yüzden bu üç tipten birine denk gelirse ayarlanır.
    }
#endif
        }

    }
}
