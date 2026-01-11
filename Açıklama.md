##📱 Çok Fonksiyonlu Asistan Uygulaması (.NET MAUI)
Bu proje, Bartın Üniversitesi Bilgisayar Mühendisliği Bölümü 3. Sınıf "Mobil Uygulama Geliştirme" dersi kapsamında Ödev 3 olarak geliştirilmiştir.
Uygulama; güncel haber takibi, döviz kurları, yapılacaklar listesi ve hava durumu takibi gibi günlük ihtiyaçları tek bir platformda toplar.

##👨‍💻 Geliştirici Bilgileri
Ad Soyad: Murat Süleymanoğlu

Öğrenci No: 22010310067

Bölüm: Bilgisayar Mühendisliği

##🚀 Proje Özellikleri ve Kullanılan Teknolojiler
Proje .NET MAUI (Multi-platform App UI) kullanılarak geliştirilmiş olup aşağıdaki teknik isterleri karşılamaktadır:

#1. 🔐 Kullanıcı İşlemleri (Firebase Authentication)
Kullanıcılar e-posta ve şifre ile Kayıt Olabilir ve Giriş Yapabilir.

Giriş ekranı tasarımı ve animasyonlu geçişler.

#2. ✅ Yapılacaklar Listesi (Firebase Realtime Database)
Bulut tabanlı CRUD (Ekle, Oku, Güncelle, Sil) işlemleri.

FirebaseDatabase.net kütüphanesi ile Asenkron (Async/Await) veri iletişimi.

Görevlerin yapıldı/yapılmadı durumunun CheckBox ile anlık güncellenmesi.

Tamamlanan görevlerin üzerinin otomatik çizilmesi (Converter yapısı).

#3. 📰 Haberler (XML / RSS Parsing)
TRT Haber RSS servisinden XML formatında veri çekme.

XDocument ve LINQ kullanılarak verilerin parse edilmesi.

Kategorilere göre (Spor, Ekonomi, Teknoloji vb.) dinamik haber filtreleme.

Haber detayına tıklandığında ilgili habere gitme ve Paylaşma (Share API) özelliği.

#4. 💰 Döviz Kurları (JSON API)
Truncgil Finans API kullanılarak anlık döviz verilerinin çekilmesi.

HttpClient ve System.Text.Json ile veri işleme.

Dolar, Euro, Altın gibi değerlerin anlık değişimi ve renk kodları (Artış/Azalış).

#5. ⛅ Hava Durumu (Yerel Depolama / Persistence)
Kullanıcının belirlediği şehirlerin listelenmesi.

Verilerin JSON formatında cihazın yerel hafızasına (FileSystem.AppDataDirectory) kaydedilmesi.

Uygulama kapatılıp açıldığında şehir listesinin korunması.

MGM (Meteoroloji Genel Müdürlüğü) kaynaklı ikon gösterimi.

#6. ⚙️ Ayarlar ve Tema
Uygulama genelinde Açık (Light) ve Koyu (Dark) mod desteği.

Ayarlar sayfasından tema değiştirme ve tercihin anlık uygulanması.

##📦 Kullanılan Kütüphaneler (NuGet Packages)
Projenin çalışması için aşağıdaki paketlerin yüklü olması gerekmektedir:

FirebaseAuthentication.net (4.1.0)

FirebaseDatabase.net (5.0.0)

Newtonsoft.Json (13.0.3)

Microsoft.Maui.Controls

##🛠️ Kurulum ve Çalıştırma
Projeyi bilgisayarınıza klonlayın:

Visual Studio 2022'yi açın ve .sln dosyasını seçin.

Solution Explorer'da projeye sağ tıklayıp "Restore NuGet Packages" seçeneğine tıklayarak gerekli kütüphaneleri indirin.

Hedef cihazı (Android Emulator veya Windows Machine) seçerek projeyi başlatın (F5).

##📷 Ekran Görüntüleri

#Giriş Ekranı

<img width="1419" height="690" alt="image" src="https://github.com/user-attachments/assets/ece18d6f-0bf5-43d7-b1df-dc874f2e88d7" />

#AnaSayfa Ekranı

<img width="1919" height="1017" alt="image" src="https://github.com/user-attachments/assets/2cdd400c-c91a-4402-a6b3-18cc623d38c4" />

#Güncel Kurlar Ekranı

<img width="1919" height="1014" alt="image" src="https://github.com/user-attachments/assets/807201f6-1c5c-48d9-a75b-22eb8e04a2ca" />

#Haberler Ekranı

<img width="1919" height="1017" alt="image" src="https://github.com/user-attachments/assets/7872f02e-50ca-47aa-be04-13fe81d70abe" />

#Haber Detay Ekranı

<img width="1919" height="1021" alt="image" src="https://github.com/user-attachments/assets/2cf97868-9613-4cbd-89e2-98c7c195ece7" />

#Hava Durumu Ekranı

<img width="1918" height="1017" alt="image" src="https://github.com/user-attachments/assets/66fc6c95-ddaa-4955-8081-dfdf9b821886" />

#Yapılacaklar Ekranı

<img width="1919" height="1016" alt="image" src="https://github.com/user-attachments/assets/d0134ee4-0ee1-4d99-a7fa-dfb8304d7fd3" />

#Ayarlar Ekranı

<img width="1919" height="1021" alt="image" src="https://github.com/user-attachments/assets/befcd7f3-ab38-49db-9d7c-945045359f17" />

##🔗 Kaynaklar ve API'ler
Haber Kaynağı: TRT Haber RSS

Döviz Verisi: Truncgil Finans API

Hava Durumu İkonları: Meteoroloji Genel Müdürlüğü


## 🎥 Proje Tanıtım Videosu

Aşağıdaki görsele tıklayarak uygulamanın tanıtım videosunu izleyebilirsiniz:

[Tanıtım Videosu](https://youtu.be/rPXOP_XKx5w)










