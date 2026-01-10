namespace Odev3Proje;

public partial class HaberDetayPage : ContentPage
{
    string haberLink;
    string haberBaslik;

    public HaberDetayPage(Haber haber)
    {
        InitializeComponent();

        // Gelen verileri ekrana bas
        lblBaslik.Text = haber.Baslik;
        lblIcerik.Text = haber.Ozet;
        imgHaber.Source = haber.Gorsel;
        haberLink = haber.Link;
        haberBaslik = haber.Baslik;
    }

    // Siteye gitme
    private async void OnOpenWebClicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri(haberLink));
    }

    // Paylaşma Butonu 
    private async void OnShareClicked(object sender, EventArgs e)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = haberLink,
            Title = haberBaslik
        });
    }
}