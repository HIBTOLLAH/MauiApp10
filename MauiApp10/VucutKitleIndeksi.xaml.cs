namespace MauiApp10;

public partial class VucutKitleIndeksi : ContentPage
{
	public VucutKitleIndeksi()
	{
		InitializeComponent();
	}


    private void CalculateButton_Clicked1(object sender, EventArgs e)
    {
        // Kullan�c�n�n girdi�i kilo ve boy de�erlerini al�n
        double weight = Convert.ToDouble(WeightEntry.Text);
        double height = Convert.ToDouble(HeightEntry.Text) / 100; // cm cinsinden boyu metre cinsine �evirin

        // V�cut kitle indeksi hesaplamas�
        double vki = weight / (height * height);

        // VKI'ye g�re durumu belirleyin
        string status;
        if (vki < 16)
            status = "�leri D�zeyde Zay�f";
        else if (vki >= 16 && vki <= 16.99)
            status = "Orta D�zeyde Zay�f";
        else if (vki >= 17 && vki <= 18.49)
            status = "Hafif D�zeyde Zay�f";
        else if (vki >= 18.50 && vki <= 24.9)
            status = "Normal Kilo";
        else if (vki >= 25 && vki <= 29.99)
            status = "Hafif �i�man / Fazla Kilolu";
        else if (vki >= 30 && vki <= 34.99)
            status = "1. Derecede Obez";
        else if (vki >= 35 && vki <= 39.99)
            status = "2. Derecede Obez";
        else
            status = "3. Derecede Obez";

        // Sonu�lar� etiketlere yazd�r�n
        VkiLabel.Text = $"VKI: {vki:F2}";
        StatusLabel.Text = $"Durum: {status}";
    }

    private void WeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'dan gelen de�eri kilo giri�ine yans�t�n
        WeightEntry.Text = e.NewValue.ToString();
    }

    private void HeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'dan gelen de�eri boy giri�ine yans�t�n
        HeightEntry.Text = e.NewValue.ToString();
    }
}
