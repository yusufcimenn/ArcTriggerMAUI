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

        private void OnLoginClicked(object? sender, EventArgs e)
        {
           
             Navigation.PushAsync(new OrderWindowPage());
        }



    }
}
