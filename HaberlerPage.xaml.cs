using System.Runtime.CompilerServices;
using System.Xml.Linq; // XML işlemi için gerekli
namespace Odev3Proje;

public partial class HaberlerPage : ContentPage
{
    public HaberlerPage()
    {
        InitializeComponent();
        // Sayfa ilk açıldığında varsayılan olarak Manşet haberlerini getir
        HaberleriCek("https://www.trthaber.com/manset_articles.rss");
    }

    private async void HaberleriCek(string url)
    {
        loading.IsRunning = true; // Yükleniyor...
        listHaberler.ItemsSource = null; // Listeyi temizle

        try
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                // Burada JSON yerine XML Parsing kullanıyoruz çünkü TRT RSS verisi XML formatında daha stabil.
                XDocument doc = XDocument.Parse(response);

                // XML içindeki <item> etiketlerini bulup Haber nesnesine çeviriyoruz (LINQ ile)
                var haberler = doc.Descendants("item").Select(x => new Haber
                {
                    Baslik = x.Element("title")?.Value,
                    Link = x.Element("link")?.Value,
                    Ozet = x.Element("description")?.Value, // Haber özet metni
                    Tarih = x.Element("pubDate")?.Value,
                    // Görseli bulmaya çalışıyoruz, yoksa varsayılan resim koyuyoruz
                    Gorsel = x.Element("enclosure")?.Attribute("url")?.Value ?? "dotnet_bot.png"
                }).ToList();

                listHaberler.ItemsSource = haberler;
            }
        }
        catch
        {
            await DisplayAlert("Hata", "Haberler yüklenemedi. İnternet bağlantınızı kontrol edin.", "Tamam");
        }

        loading.IsRunning = false; // Yükleme bitti
    }

    // Kategori butonlarına (Spor, Ekonomi vb.) basınca çalışır
    private void KategoriSec_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        // XAML'da CommandParameter olarak verdiğimiz "spor", "ekonomi" gibi değerleri alıyoruz
        string kategori = btn.CommandParameter.ToString();

        // URL'i dinamik olarak oluşturuyoruz
        string url = $"https://www.trthaber.com/{kategori}_articles.rss";

        HaberleriCek(url);
    }

    // Listeden bir habere tıklanınca çalışır
    private async void OnHaberSecildi(object sender, SelectionChangedEventArgs e)
    {
        // Seçilen öğeyi 'Haber' tipine dönüştür
        if (e.CurrentSelection.FirstOrDefault() is Haber secilenHaber)
        {
            // Detay sayfasına git ve veriyi taşı
            await Navigation.PushAsync(new HaberDetayPage(secilenHaber));
        }
        // Seçimi kaldır (böylece aynı habere tekrar tıklanabilir)
        ((CollectionView)sender).SelectedItem = null;
    }
}