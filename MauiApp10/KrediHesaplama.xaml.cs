namespace MauiApp10;

public partial class KrediHesaplama : ContentPage
{
    public KrediHesaplama()
    {
        InitializeComponent();
    }

    private void HesaplamaButton_Clicked(object sender, EventArgs e)
    {
        double principal = Convert.ToDouble(PrincipalEntry.Text); // Kredi tutar�
        double interestRate = Convert.ToDouble(InterestRateEntry.Text) / 100; // Faiz oran� (y�zde cinsinden)
        int months = Convert.ToInt32(MonthsEntry.Text); // Vade (ayl�k)

        // Brut faiz oran� hesaplama
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

        // Toplam �deme hesaplama
        double totalPayment = monthlyPayment * months;

        // Toplam faiz hesaplama
        double totalInterest = totalPayment - principal;

        // KKDF ve BSMV hesaplamas�
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

        // Taksit, toplam �deme ve toplam faiz miktar�n� g�sterme
        MonthlyPaymentLabel.Text = $"Ayl�k Taksit: {monthlyPayment.ToString("C")}";
        TotalPaymentLabel.Text = $"Toplam �deme: {totalPayment.ToString("C")}";
        TotalInterestLabel.Text = $"Toplam Faiz: {totalInterest.ToString("C")}";
    }
}






