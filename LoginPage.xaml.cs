using Odev3Proje.Services;

namespace Odev3Proje;

public partial class LoginPage : ContentPage
{
    AuthService _authService;
    // Kullanıcının o an "Giriş" modunda mı "Kayıt" modunda mı olduğunu tutar
    bool isLoginMode = true;

    public LoginPage()
    {
        InitializeComponent();
        _authService = new AuthService(); // Servisi başlat
    }

    // Giriş veya Kayıt butonuna basılınca çalışır
    private async void OnActionClicked(object sender, EventArgs e)
    {
        string email = entEmail.Text;
        string password = entPassword.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Uyarı", "Alanları doldurunuz.", "Tamam");
            return;
        }

        loading.IsRunning = true;
        btnAction.IsEnabled = false;

        try
        {
            if (isLoginMode)
            {
                // --- GİRİŞ MODU ---
                string token = await _authService.LoginAsync(email, password);

                if (!string.IsNullOrEmpty(token))
                {
                    // Giriş başarılı, ana sayfaya git
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Hata", "Giriş yapılamadı. E-posta veya şifre hatalı.", "Tamam");
                }
            }
            else
            {
                // --- KAYIT MODU ---
                string token = await _authService.RegisterAsync(email, password);

                if (!string.IsNullOrEmpty(token))
                {
                    // Kayıt başarılı oldu
                    await DisplayAlert("Başarılı", "Kayıt işlemi tamamlandı! Şimdi giriş yapabilirsiniz.", "Tamam");

                    // Ekrani otomatik olarak "Giriş Yap" moduna çeviriyoruz
                    isLoginMode = true;
                    lblBaslik.Text = "GİRİŞ YAP";
                    btnAction.Text = "GİRİŞ YAP";
                    lblSwitch.Text = "Hesabım yok mu? Kaydol";

                    // Şifre alanını temizleme
                   
                }
                else
                {
                    await DisplayAlert("Hata", "Kayıt oluşturulamadı. Lütfen tekrar deneyin.", "Tamam");
                }
            }
        }
        catch (Exception ex)
        {
            // Şifre kısa veya mail formatı bozuksa buraya düşer
            await DisplayAlert("Hata", ex.Message, "Tamam");
        }
        finally
        {
            loading.IsRunning = false;
            btnAction.IsEnabled = true;
        }
    }

    // "Hesabım yok" / "Zaten hesabım var" yazısına tıklanınca
    private void OnSwitchModeTapped(object sender, EventArgs e)
    {
        isLoginMode = !isLoginMode; // Modu tersine çevir

        // Ekran yazılarını moda göre güncelle
        lblBaslik.Text = isLoginMode ? "GİRİŞ YAP" : "KAYIT OL";
        btnAction.Text = isLoginMode ? "GİRİŞ YAP" : "KAYIT OL";
        lblSwitch.Text = isLoginMode ? "Hesabım yok mu? Kaydol" : "Zaten hesabım var? Giriş Yap";
    }
}