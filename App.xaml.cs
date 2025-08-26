namespace ArcTriggerMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage()) 
            {
                BackgroundColor = Color.FromArgb("#0E1116"), // sayfanın dışı
                BarBackgroundColor = Color.FromArgb("#0E1116"), // üst bar
                BarTextColor = Colors.White

            };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Width  = 450; // login portrait
            window.Height = 650;
            return window;
        }
    }
}