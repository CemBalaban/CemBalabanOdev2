namespace CemBalabanOdev2;

public partial class BMICalculator : ContentPage
{

    public BMICalculator()
    {
        InitializeComponent();
    }

    public void CalculateBMI_Pressed(object sender, EventArgs e)
    {
        var heightInMeter = heightSlider.Value / 100;
        var weightInKg = weightSlider.Value;

        double bmi = weightInKg / (heightInMeter * heightInMeter);

        endeks.Text = "V�cut Kitle Endeksiniz: " + bmi;

        if (bmi < 16)
        {
            durum.Text = "Durum: �leri D�zeyde Zay�f";
        }
        else if (bmi > 16 && bmi < 16.99)
        {
            durum.Text = "Durum: Orta D�zeyde Zay�f";
        }
        else if (bmi > 17 && bmi < 18.49)
        {
            durum.Text = "Durum: Hafif D�zeyde Zay�f";
        }
        else if (bmi > 18.50 && bmi < 24.99)
        {
            durum.Text = "Durum: Normal Kilolu";
        }
        else if (bmi > 25 && bmi < 29.99)
        {
            durum.Text = "Durum: Hafif �i�man/Fazla Kilolu";
        }
        else if (bmi > 30 && bmi < 34.99)
        {
            durum.Text = "Durum: 1. Derece Obez";
        }
        else if (bmi > 35 && bmi < 39.99)
        {
            durum.Text = "Durum: 2. Derece Obez";
        }
        else
        {
            durum.Text = "Durum: 3. Derece Obez/Morbid";
        }

        

    }
}