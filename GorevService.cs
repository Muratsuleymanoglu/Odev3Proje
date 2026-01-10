using Firebase.Database;
using Firebase.Database.Query;
using Odev3Proje.Models;

namespace Odev3Proje.Services
{
    public class GorevService
    {
        // Firebase Realtime Database bağlantı linki
        private const string FirebaseDatabaseUrl = "https://odev3-af4c7-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly FirebaseClient firebase;

        public GorevService()
        {
            // Veritabanı istemcisini başlatıyoruz
            firebase = new FirebaseClient(FirebaseDatabaseUrl);
        }

        // Veritabanındaki tüm görevleri çeken metot
        public async Task<List<Gorev>> GorevleriGetir()
        {
            // "Gorevler" tablosuna (düğümüne) git ve verileri bir kere oku
            var items = await firebase
                .Child("Gorevler")
                .OnceAsync<Gorev>();

            // Gelen karmaşık veriyi bizim 'Gorev' sınıfımıza dönüştür
            return items.Select(item => new Gorev
            {
                Key = item.Key, // Silme/Güncelleme için bu Key (ID) çok önemli
                Baslik = item.Object.Baslik,
                Detay = item.Object.Detay,
                TarihSaat = item.Object.TarihSaat, // Tarih bilgisini de çekiyoruz
                YapildiMi = item.Object.YapildiMi // Checkbox durumu
            }).ToList();
        }

        // Yeni görev ekleme
        public async Task GorevEkle(Gorev gorev)
        {
            // "Gorevler" altına yeni bir veri postala
            await firebase
                .Child("Gorevler")
                .PostAsync(gorev);
        }

        // Mevcut görevi güncelleme (Düzenleme veya Checkbox değişimi için)
        public async Task GorevGuncelle(Gorev gorev)
        {
            // İlgili Key'e (ID) sahip görevi bul ve üzerine yaz (Put)
            await firebase
                .Child("Gorevler")
                .Child(gorev.Key)
                .PutAsync(gorev);
        }

        // Görev silme
        public async Task GorevSil(string key)
        {
            // İlgili Key'i bul ve sil
            await firebase
                .Child("Gorevler")
                .Child(key)
                .DeleteAsync();
        }
    }
}