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
                // await WindowUtil.ResizeLandscapeResponsiveAsync();
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // 1) Open Order window (new OS window)
            var page = new OrderWindowPage { BackgroundColor = Color.FromArgb("#0E1116") };
            var win = new Window(page)
            {
                Width = 1720,
                Height = 200,
                Title = "Order"
            };

            Application.Current?.OpenWindow(win);

            // 2) Close the current window that hosts the login page
            // Prefer this.Window if available
            var loginWindow = this.Window;

            // Fallback if this.Window is null (e.g., wrapped in NavigationPage)
            if (loginWindow == null)
            {
                loginWindow = Application.Current?.Windows?
                    .FirstOrDefault(w =>
                        w.Page == this ||
                        (w.Page is NavigationPage nav && nav.CurrentPage == this) ||
                        (w.Page?.Navigation?.NavigationStack.Contains(this) ?? false));
            }

            // Close after a tiny delay to ensure the new window is up
            if (loginWindow != null)
            {
                await Task.Delay(50); // small safety delay
                Application.Current.CloseWindow(loginWindow);
            }
        }
    }
    }
    



    

