using Odev3Proje.Models;
using Odev3Proje.Services;
using System.Collections.ObjectModel; // Listeyi anlık güncellemek için gerekli

namespace Odev3Proje
{
    public partial class YapilacaklarPage : ContentPage
    {
        GorevService _service = new GorevService();

        public YapilacaklarPage()
        {
            InitializeComponent();
        }

        // Sayfa görüntülendiğinde çalışır (Geri gelindiğinde listeyi yenilemek için)
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await VerileriYukle();
        }

        private async Task VerileriYukle()
        {
            loading.IsVisible = true;
            loading.IsRunning = true;

            try
            {
                // Service'den verileri Firebase üzerinden çekiyoruz
                var liste = await _service.GorevleriGetir();

                // Tarihe göre sırala (En yeni en üstte dursun)
                cvGorevler.ItemsSource = new ObservableCollection<Gorev>(liste.OrderByDescending(x => x.TarihSaat));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Veriler yüklenirken hata oluştu: " + ex.Message, "Tamam");
            }
            finally
            {
                loading.IsRunning = false;
                loading.IsVisible = false;
            }
        }

        // YENİ EKLEME BUTONU
        private async void Ekle_Clicked(object sender, EventArgs e)
        {
            // Boş bir detay sayfasına git
            await Navigation.PushAsync(new GorevDetayPage());
        }

        // DÜZENLEME (Kalem Butonu)
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var gorev = button?.BindingContext as Gorev;

            if (gorev != null)
            {
                // Seçilen görevi parametre olarak gönderip detay sayfasını açıyoruz
                await Navigation.PushAsync(new GorevDetayPage(gorev));
            }
        }

        // SİLME (Çöp Kutusu Butonu)
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var gorev = button?.BindingContext as Gorev;

            if (gorev != null)
            {
                // Kullanıcıdan silme onayı alınıyor
                bool cevap = await DisplayAlert("Silinsin mi?", $"'{gorev.Baslik}' başlıklı görevi silmek istiyor musunuz?", "Evet", "Hayır");

                if (cevap)
                {
                    loading.IsVisible = true;
                    loading.IsRunning = true;

                    // Firebase'den sil
                    await _service.GorevSil(gorev.Key);

                    // Listeyi yenile ki silinen ekrandan gitsin
                    await VerileriYukle();
                }
            }
        }

        // CHECKBOX (Yapıldı/Yapılmadı Durumu)
        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var cb = sender as CheckBox;
            var gorev = cb?.BindingContext as Gorev;

            // Null kontrolü ve gereksiz tetiklenmeleri önleme
            if (gorev != null && gorev.YapildiMi != e.Value)
            {
                gorev.YapildiMi = e.Value;
                // Değişikliği veritabanına anında yansıt
                await _service.GorevGuncelle(gorev);
            }
        }
    }
}