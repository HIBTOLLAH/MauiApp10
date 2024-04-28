namespace MauiApp10;

public partial class KrediHesaplama : ContentPage
{
    public KrediHesaplama()
    {
        InitializeComponent();
    }

    private void HesaplamaButton_Clicked(object sender, EventArgs e)
    {
        double principal = Convert.ToDouble(PrincipalEntry.Text); // Kredi tutarý
        double interestRate = Convert.ToDouble(InterestRateEntry.Text) / 100; // Faiz oraný (yüzde cinsinden)
        int months = Convert.ToInt32(MonthsEntry.Text); // Vade (aylýk)

        // Brut faiz oraný hesaplama
        double brutFaiz = 0;
        if (LoanTypePicker.SelectedItem.ToString() == "Konut")
            brutFaiz = 0.08;
        else if (LoanTypePicker.SelectedItem.ToString() == "Ihtiyac")
            brutFaiz = 0.15;
        else if (LoanTypePicker.SelectedItem.ToString() == "Ticari")
            brutFaiz = 0.05;
        else if (LoanTypePicker.SelectedItem.ToString() == "Tasit")
            brutFaiz = 0.15;

        // Taksit hesaplama
        double monthlyPayment = (principal * interestRate) / (1 - Math.Pow(1 + brutFaiz, -months));

        // Toplam ödeme hesaplama
        double totalPayment = monthlyPayment * months;

        // Toplam faiz hesaplama
        double totalInterest = totalPayment - principal;

        // KKDF ve BSMV hesaplamasý
        double kkdf = 0;
        double bsmv = 0;
        if (LoanTypePicker.SelectedItem.ToString() == "Ihtiyac")
        {
            kkdf = 0.15;
            bsmv = 0.10;
        }
        else if (LoanTypePicker.SelectedItem.ToString() == "Tasit")
        {
            kkdf = 0.15;
            bsmv = 0.5;
        }
        else if (LoanTypePicker.SelectedItem.ToString() == "Konut")
        {
            kkdf = 0;
            bsmv = 0;
        }
        else if (LoanTypePicker.SelectedItem.ToString() == "Ticari")
        {
            kkdf = 0.05;
            bsmv = 0.5;
        }

        // Taksit, toplam ödeme ve toplam faiz miktarýný gösterme
        MonthlyPaymentLabel.Text = $"Aylýk Taksit: {monthlyPayment.ToString("C")}";
        TotalPaymentLabel.Text = $"Toplam Ödeme: {totalPayment.ToString("C")}";
        TotalInterestLabel.Text = $"Toplam Faiz: {totalInterest.ToString("C")}";
    }
}






