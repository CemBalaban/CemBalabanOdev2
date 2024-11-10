using Microsoft.Maui.Graphics;


namespace CemBalabanOdev2;

public partial class ColorPicker : ContentPage
{
	public ColorPicker()
	{
		InitializeComponent();
	}

    public void OnColorSliderChanged(object sender, ValueChangedEventArgs e)
    {
        int red = (int)redSlider.Value;
        int green = (int)greenSlider.Value;
        int blue = (int)blueSlider.Value;

        Color renk = Color.FromRgb(red, green, blue);
        ColorDisplay.Color = renk;

        rgb.Text = $"RGB({red}, {green}, {blue})";

        string hex = $"#{red:X2}{green:X2}{blue:X2}";
        rgbCode.Text = hex;
    }
    private async void OnCopyButtonClicked(object sender, EventArgs e)
    {
        // Hex kodunu panoya kopyala
        if (!string.IsNullOrEmpty(rgbCode.Text))
        {
            await Clipboard.SetTextAsync(rgbCode.Text);
            await DisplayAlert("Kopyalandý", "Renk kodu panoya kopyalandý!", "Tamam");
        }
    }
}