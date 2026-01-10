using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace Odev3Proje
{
    public partial class AyarlarPage : ContentPage
    {
        public AyarlarPage()
        {
            InitializeComponent();
        }

        // Sayfa her açıldığında kontrol et
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AyarlariSenkronizeEt();
        }

        private void AyarlariSenkronizeEt()
        {
            //. Mevcut tema ayarını al
            var gecerliTema = Application.Current.UserAppTheme;

            // Eğer tema "Belirsiz" (Unspecified) ise, 
            // uygulamanın o an sistemden alıp kullandığı temaya (RequestedTheme) bak.
            if (gecerliTema == AppTheme.Unspecified)
            {
                gecerliTema = Application.Current.RequestedTheme;
            }

            //  Switch'in durumunu buna göre ayarla (Dark ise True, değilse False)
            // Döngüye girmemesi için sadece değer farklıysa ata
            if (swTema.IsToggled != (gecerliTema == AppTheme.Dark))
            {
                swTema.IsToggled = (gecerliTema == AppTheme.Dark);
            }
        }

        private void swTema_Toggled(object sender, ToggledEventArgs e)
        {
            // Kullanıcı değiştirdiğinde kesin olarak Dark veya Light olarak ayarla
            if (e.Value) // Açıldıysa
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else // Kapandıysa
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
        }
    }
}