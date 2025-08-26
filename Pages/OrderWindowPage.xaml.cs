namespace ArcTriggerMAUI.Pages;

public partial class OrderWindowPage : ContentPage
{
    public OrderWindowPage()
    {
        InitializeComponent();
    }
    private void OnAutoFetchClicked(object sender, EventArgs e)
    {
        // TODO implement later
    }

    // Symbol and option selectors
    private void OnSymbolChanged(object sender, EventArgs e)
    {
        // TODO implement later
    }

    private void OnCallOptionCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // TODO implement later
    }

    private void OnPutOptionCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // TODO implement later
    }

    // Trigger price
    private void OnTriggerPriceTextChanged(object sender, TextChangedEventArgs e)
    {
        // TODO implement later
    }

    private void OnIncreaseTriggerClicked(object sender, EventArgs e)
    {
        // TODO implement later
    }

    private void OnDecreaseTriggerClicked(object sender, EventArgs e)
    {
        // TODO implement later
    }

    // Strike and expiration
    private void OnStrikeChanged(object sender, EventArgs e)
    {
        // TODO implement later
    }

    private void OnExpirationChanged(object sender, EventArgs e)
    {
        // TODO implement later
    }

    // Position size
    private void OnPositionTextChanged(object sender, TextChangedEventArgs e)
    {
        // TODO implement later
    }

    private void OnPositionPresetClicked(object sender, EventArgs e)
    {
        // TODO implement later 2000-5000-10000$
    }



    // Stop loss
    private void OnStopLossTextChanged(object sender, TextChangedEventArgs e)
    {
        // TODO implement later
    }


    private void OnStopLossPreset(object sender, EventArgs e)
    {
        // TODO implement later   0.2-0.5-1.0 preset
    }

    // Final action
    private void OnCreateOrderClicked(object sender, EventArgs e)
    {
        // TODO implement later
    }
    private void OnAddOrderClicked(object sender, EventArgs e)
    {
#if WINDOWS || MACCATALYST
    var page = new OrderWindowPage();

    var win = new Window(page)
    {
        Title = "Order",
        Width = 640,   // pencere geniþliði
        Height = 150   // pencere yüksekliði
    };

    Application.Current?.OpenWindow(win);
#else
        // Mobil platformlarda çoklu OS penceresi yok; istersen modal aç
        // await Navigation.PushModalAsync(new OrderWindowPage());
#endif
    }
    // Cancel butonunda bu pencereyi kapat
    void OnCancelClicked(object sender, EventArgs e)
    {
        var w = this.Window;
        if (w != null && Application.Current != null)
            Application.Current.CloseWindow(w);
    }
}