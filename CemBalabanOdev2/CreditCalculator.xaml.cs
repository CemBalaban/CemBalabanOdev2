using System.Security.Cryptography;

namespace CemBalabanOdev2;

public partial class CreditCalculator : ContentPage
{
	public CreditCalculator()
	{
		InitializeComponent();
	}
    private void OnHesaplaClicked(object sender, EventArgs e)
    {
        if (double.TryParse(txtKrediTutari.Text, out double krediTutari) &&
            int.TryParse(txtVade.Text, out int vade) && double.TryParse(txtFaizOrani.Text, out double oran))
        {
            double KKDF = 0;
            double BSMV = 0;

            switch (creditType.SelectedItem)
            {
                case "Konut Kredisi":
                    KKDF = 0;
                    BSMV = 0;
                    break;
                case "İhtiyaç Kredisi":
                    KKDF = 0.15;
                    BSMV = 0.10;
                    break;
                case "Taşıt Kredisi":
                    KKDF = 0.15;
                    BSMV = 0.05; 
                    break;
                case "Ticari Kredi":
                    KKDF = 0;
                    BSMV = 0.05;
                    break;
                default:
                    lblSonuc.Text = "Lütfen bir kredi tipi seçin.";
                    return;
            }

            double brutFaiz = ((oran + (oran * BSMV) + (oran * KKDF )) / 100);
            double taksit = ((Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1)) * krediTutari;
            double toplam = taksit * vade;

            lblSonuc.Text = $"Aylık Taksit: {taksit:F2} ₺";
            lblToplam.Text = $"Ödenecek Tutar: {toplam:F2} ₺";
        }
        else
        {
            lblSonuc.Text = "Lütfen geçerli sayılar girin.";
        }
    }
    private void OnYeniHesaplaClicked(object sender, EventArgs e)
    {
        txtKrediTutari.Text = string.Empty;
        txtFaizOrani.Text = string.Empty;
        txtVade.Text = string.Empty;
        lblSonuc.Text = string.Empty;
        lblToplam.Text = string.Empty;
        creditType.SelectedIndex = -1;
    }

}