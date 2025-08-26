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
            var page = new OrderWindowPage
            {
                BackgroundColor = Color.FromArgb("#0E1116") // sayfa zemini koyu
            };

            var win = new Window(page)
            {
                Width = 1720,
                Height = 200,
                Title = "Order"
            };

            Application.Current?.OpenWindow(win);

            // Enforce size, center, remove border/title, disable maximize
            _ = WindowUtil.ResizeAsync(win, 1720, 200, center: true, lockResize: false);
            _ = WindowUtil.MakeBorderlessFixedAsync(win, allowMinimize: true); // minimize kalsın
        }
    }
    }
    



    

