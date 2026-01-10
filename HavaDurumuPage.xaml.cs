using System.Collections.ObjectModel;
using Newtonsoft.Json; // JSON Kayıt işlemi için

namespace Odev3Proje;

public partial class HavaDurumuPage : ContentPage
{
    // Ekranda görünecek liste (ObservableCollection: Listeye ekleme yapınca arayüz otomatik güncellenir)
    ObservableCollection<Sehir> SehirlerListesi = new ObservableCollection<Sehir>();

    // Kayıt dosyasının yolu (Telefonda özel bir klasöre kaydediyoruz)
    string dosyaYolu = Path.Combine(FileSystem.Current.AppDataDirectory, "sehirler.json");

    public HavaDurumuPage()
    {
        InitializeComponent();
        listSehirler.ItemsSource = SehirlerListesi;
        KayitliSehirleriYukle(); // Uygulama açılınca eski kaydedilmiş şehirleri getir
    }

    // 1. ŞEHİR EKLEME BUTONU
    private void OnEkleClicked(object sender, EventArgs e)
    {
        string girilenIsim = entSehir.Text;

        if (!string.IsNullOrWhiteSpace(girilenIsim))
        {
            // Şehir ismini büyük harfe çevirip ekliyoruz
            var yeniSehir = new Sehir { Isim = girilenIsim.ToUpper() };
            SehirlerListesi.Add(yeniSehir);

            entSehir.Text = ""; // Giriş kutusunu temizle
            Kaydet(); // Değişikliği kalıcı olarak telefona kaydet
        }
    }

    // 2. ŞEHİR SİLME (Listeden "Sil" butonuna basınca)
    private void OnSilClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        // CommandParameter ile hangi şehrin silineceği bilgisini alıyoruz
        var silinecekSehir = btn.CommandParameter as Sehir;

        if (silinecekSehir != null)
        {
            SehirlerListesi.Remove(silinecekSehir);
            Kaydet(); // Silinmiş halini kaydet
        }
    }

    // 3. TÜMÜNÜ TEMİZLE
    private void OnTemizleClicked(object sender, EventArgs e)
    {
        SehirlerListesi.Clear();
        Kaydet();
    }

    // --- JSON KAYIT İŞLEMLERİ (Ödev Şartı: Persistence) ---

    // Listeyi JSON formatına çevirip telefona dosyalar
    void Kaydet()
    {
        string json = JsonConvert.SerializeObject(SehirlerListesi);
        File.WriteAllText(dosyaYolu, json);
    }

    // Uygulama açılırken dosyadan okur
    void KayitliSehirleriYukle()
    {
        if (File.Exists(dosyaYolu))
        {
            string json = File.ReadAllText(dosyaYolu);
            // JSON'u tekrar listeye çevir
            var liste = JsonConvert.DeserializeObject<List<Sehir>>(json);

            if (liste != null)
            {
                foreach (var sehir in liste)
                {
                    SehirlerListesi.Add(sehir);
                }
            }
        }
        else
        {
           
            SehirlerListesi.Add(new Sehir { Isim = "BARTIN" });
        }
    }
}