using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Odev3Proje.Models;

namespace Odev3Proje
{
    public partial class KurlarPage : ContentPage
    {
        public KurlarPage()
        {
            InitializeComponent();
        }

        // Sayfa her ekrana geldiğinde çalışır
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VerileriGetir(); // API'den verileri çek
        }

        private async void VerileriGetir()
        {
            // Kullanıcıya yükleniyor (ActivityIndicator) göster
            loading.IsVisible = true;
            loading.IsRunning = true;
            listKurlar.IsVisible = false; // Yükleme bitene kadar listeyi gizle

            // JSON Veri Kaynağı (truncgil API)
            string url = "https://finans.truncgil.com/today.json";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Bazı siteler tarayıcı gibi görünmeyen istekleri reddeder, o yüzden User-Agent ekliyoruz
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                    var response = await client.GetStringAsync(url);

                    List<Doviz> dovizListesi = new List<Doviz>();

                    // Gelen JSON verisini parçalıyoruz
                    using (JsonDocument doc = JsonDocument.Parse(response))
                    {
                        var root = doc.RootElement;

                        // Hangi dövizleri istiyorsak kodlarını listeliyoruz
                        // API'deki isimler ile buradakiler eşleşmeli
                        foreach (var element in root.EnumerateObject())
                        {
                            string kod = element.Name;

                            // Sadece istediklerimizi alalım (Dolar, Euro, Altın vb.)
                            bool isteniyor = (kod == "USD" || kod == "EUR" || kod == "GBP" || kod.Contains("altin") || kod == "gumus");

                            if (isteniyor)
                            {
                                var detay = element.Value; // O dövizin detay bilgileri

                                // JSON içinden Alış/Satış/Değişim verilerini güvenli şekilde oku
                                string alis = detay.TryGetProperty("Alış", out var alisProp) ? alisProp.ToString() : "-";
                                string satis = detay.TryGetProperty("Satış", out var satisProp) ? satisProp.ToString() : "-";
                                string degisim = detay.TryGetProperty("Değişim", out var degProp) ? degProp.ToString() : "%0.00";

                                // Değişim negatif mi pozitif mi? (Ok yönü için)
                                bool artti = !degisim.Contains("-");

                                // Listemize ekle
                                dovizListesi.Add(new Doviz
                                {
                                    // İsimleri Türkçeleştiriyoruz
                                    Tur = kod.Replace("USD", "Dolar")
                                             .Replace("EUR", "Euro")
                                             .Replace("GBP", "Sterlin")
                                             .Replace("gram-altin", "Gram Altın")
                                             .Replace("ceyrek-altin", "Çeyrek Altın")
                                             .Replace("gumus", "Gümüş"),

                                    Alis = alis,
                                    Satis = satis,
                                    Degisim = degisim,
                                    // Görsel zenginlik: Yukarı/Aşağı ok
                                    Yon = artti ? "▲" : "▼",
                                    // Değişime göre Yeşil veya Kırmızı renk
                                    Renk = artti ? "#00FF00" : "#FF0000"
                                });
                            }
                        }
                    }
                    // Hazırladığımız listeyi ekrana bas
                    listKurlar.ItemsSource = dovizListesi;
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                await DisplayAlert("Hata", "Veriler çekilemedi: " + ex.Message, "Tamam");
            }
            finally
            {
                // İşlem bitince (başarılı veya hatalı) yüklenme ekranını kapat
                loading.IsRunning = false;
                loading.IsVisible = false;
                listKurlar.IsVisible = true;
            }
        }

        // Toolbar'daki Yenile butonuna basınca
        private void OnRefreshClicked(object sender, EventArgs e)
        {
            VerileriGetir();
        }
    }
}