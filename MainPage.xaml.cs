using ArcTriggerMAUI.Pages;

namespace ArcTriggerMAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();

            var count = Application.Current?.Windows?.Count ?? 0;
            if (count <= 1)
            {
                // optional: main window behavior here (or nothing)
                // await WindowUtil.ResizeLandscapeResponsiveAsync();
            }
        }

        private void OnLoginClicked(object? sender, EventArgs e)
        {
            var page = new OrderWindowPage();

            var win = new Window(page)
            {
                Width = 1720,  // initial size before handler
                Height = 200,
                Title = "Order"
            };

            Application.Current?.OpenWindow(win);

            // enforce size and center after native handler is ready
            _ = WindowUtil.ResizeAsync(win, 1720, 200, center: true, lockResize: false);
        }
    }



    }

