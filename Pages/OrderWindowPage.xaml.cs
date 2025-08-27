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
        if (sender is Picker picker && picker.SelectedIndex != -1)
        {
            var symbol = picker.Items[picker.SelectedIndex];
            Console.WriteLine($"Selected symbol: {symbol}");
        }
    }

    private void OnCallOptionCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) Console.WriteLine("Order type: Call");
    }

    private void OnPutOptionCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) Console.WriteLine("Order type: Put");
    }

    // Trigger price
    private void OnTriggerPriceTextChanged(object sender, TextChangedEventArgs e)
    {
        Console.WriteLine($"Trigger price: {e.NewTextValue}");
    }

    private void OnIncreaseTriggerClicked(object sender, EventArgs e)
    {
        if (this.FindByName<Entry>("TriggerEntry") is Entry entry && decimal.TryParse(entry.Text, out decimal value))
        {
            entry.Text = (value + 1).ToString("F2");
        }
    }

    private void OnDecreaseTriggerClicked(object sender, EventArgs e)
    {
        if (this.FindByName<Entry>("TriggerEntry") is Entry entry && decimal.TryParse(entry.Text, out decimal value))
        {
            entry.Text = (value - 1).ToString("F2");
        }
    }

    // Strike and expiration
    private void OnStrikeChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedIndex != -1)
        {
            Console.WriteLine($"Strike: {picker.Items[picker.SelectedIndex]}");
        }
    }

    private void OnExpirationChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedIndex != -1)
        {
            Console.WriteLine($"Expiration: {picker.Items[picker.SelectedIndex]}");
        }
    }

    // Position size
    private void OnPositionTextChanged(object sender, TextChangedEventArgs e)
    {
        Console.WriteLine($"Position size: {e.NewTextValue}");
    }

    private void OnPositionPresetClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && this.FindByName<Entry>("PositionEntry") is Entry entry)
        {
            entry.Text = btn.Text.Replace("$", "");
        }
    }

    // Stop loss
    private void OnStopLossTextChanged(object sender, TextChangedEventArgs e)
    {
        Console.WriteLine($"Stop loss: {e.NewTextValue}");
    }

    private void OnStopLossPreset(object sender, EventArgs e)
    {
        if (sender is Button btn && this.FindByName<Entry>("StopLossEntry") is Entry entry)
        {
            entry.Text = btn.Text.Replace("$", "");
        }
    }

    // Final action
    private void OnCreateOrderClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Order created!");
    }
    private void OnProfitPresetClicked(object sender, EventArgs e)
    {
        //TODO IMPLEMENT LATER
    }

    private void OnTrailClicked(object sender, EventArgs e)
    {
        //TODO IMPLEMENT LATER
    }
    private void OnBreakevenClicked(object sender, EventArgs e)
    {
        //TODO IMPLEMENT LATER
    }
    private void OnOffsetPresetClicked(object sender, EventArgs e)
    {
        //TODO IMPLEMENT LATER
    }

    private void OnAddOrderClicked(object sender, EventArgs e)
    {
        var page = new OrderWindowPage();

        var win = new Window(page)
        {
            Width = 1720,
            Height = 200,
            Title = "Order"
        };

        Application.Current?.OpenWindow(win);

        _ = WindowUtil.ResizeAsync(win, 1720, 200, center: true, lockResize: false);
    }

    // Cancel butonunda bu pencereyi kapat
    void OnCancelClicked(object sender, EventArgs e)
    {
        var w = this.Window;
        if (w != null && Application.Current != null)
            Application.Current.CloseWindow(w);
    }
}
