namespace Odev3Proje;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Kullanıcı çıkış yapınca tekrar Login sayfasına gönder
        await Current.GoToAsync("//LoginPage");
        Application.Current.MainPage = new LoginPage();}
}