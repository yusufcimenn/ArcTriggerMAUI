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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await WindowUtil.ResizePortraitResponsiveAsync();
        }

        private void OnLoginClicked(object? sender, EventArgs e)
        {
           
             Navigation.PushAsync(new OrderWindowPage());
        }



    }
}
