namespace MauiApp10;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}
    private void ColorSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider deðerlerinden renk oluþtur
        Color color = Color.FromRgb((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value);
        ColorBox.Color = color;
        string colorHex = ColorBox.Color.ToHex();
        ColorCodeLabel.Text = $"Renk Kodu: {colorHex}";

    }
    private void CopyColorButton_Clicked(object sender, EventArgs e)
    {
        // Renk kodunu panoya kopyala
        string colorHex = ColorBox.Color.ToHex();
        Clipboard.SetTextAsync(colorHex);
    }
    
        private void RandomColorButton_Clicked(object sender, EventArgs e)
        {
            // Generate random color
            Random random = new Random();
            Color randomColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));

            // Update sliders and color box
            RedSlider.Value = randomColor.Red;
            GreenSlider.Value = randomColor.Green;
            BlueSlider.Value = randomColor.Blue;
            ColorBox.Color = randomColor;

            UpdateColorCodeLabel(); // Update color code label
        }

        private void UpdateColorCodeLabel()
        {
            // Update color code label text
            ColorCodeLabel.Text = ColorBox.Color.ToHex();
        }
    }



