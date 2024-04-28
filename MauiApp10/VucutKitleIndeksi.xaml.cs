namespace MauiApp10;

public partial class VucutKitleIndeksi : ContentPage
{
	public VucutKitleIndeksi()
	{
		InitializeComponent();
	}


    private void CalculateButton_Clicked1(object sender, EventArgs e)
    {
        // Kullanýcýnýn girdiði kilo ve boy deðerlerini alýn
        double weight = Convert.ToDouble(WeightEntry.Text);
        double height = Convert.ToDouble(HeightEntry.Text) / 100; // cm cinsinden boyu metre cinsine çevirin

        // Vücut kitle indeksi hesaplamasý
        double vki = weight / (height * height);

        // VKI'ye göre durumu belirleyin
        string status;
        if (vki < 16)
            status = "Ýleri Düzeyde Zayýf";
        else if (vki >= 16 && vki <= 16.99)
            status = "Orta Düzeyde Zayýf";
        else if (vki >= 17 && vki <= 18.49)
            status = "Hafif Düzeyde Zayýf";
        else if (vki >= 18.50 && vki <= 24.9)
            status = "Normal Kilo";
        else if (vki >= 25 && vki <= 29.99)
            status = "Hafif Þiþman / Fazla Kilolu";
        else if (vki >= 30 && vki <= 34.99)
            status = "1. Derecede Obez";
        else if (vki >= 35 && vki <= 39.99)
            status = "2. Derecede Obez";
        else
            status = "3. Derecede Obez";

        // Sonuçlarý etiketlere yazdýrýn
        VkiLabel.Text = $"VKI: {vki:F2}";
        StatusLabel.Text = $"Durum: {status}";
    }

    private void WeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'dan gelen deðeri kilo giriþine yansýtýn
        WeightEntry.Text = e.NewValue.ToString();
    }

    private void HeightSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider'dan gelen deðeri boy giriþine yansýtýn
        HeightEntry.Text = e.NewValue.ToString();
    }
}
