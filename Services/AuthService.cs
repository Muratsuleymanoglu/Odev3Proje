using Firebase.Auth;
using Firebase.Auth.Providers;
using System.Threading.Tasks;

namespace Odev3Proje.Services
{
    public class AuthService
    {
        // Firebase Konsolundan aldığımız Web API Key ve Domain bilgileri
        private const string WebApiKey = "AIzaSyCTC_9IpAXKSEgFIQ2FOmE_4MyfnxE7LR8";
        private const string AuthDomain = "odev3-af4c7.firebaseapp.com";

        // Firebase Kimlik Doğrulama İstemcisi
        FirebaseAuthClient client;

        public AuthService()
        {
            // Konfigürasyon ayarlarını yapıyoruz
            var config = new FirebaseAuthConfig
            {
                ApiKey = WebApiKey,
                AuthDomain = AuthDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    // Email ve Şifre ile giriş sağlayıcısını ekliyoruz
                    new EmailProvider()
                }
            };

            // İstemciyi başlatıyoruz
            client = new FirebaseAuthClient(config);
        }

        // Giriş Yapma Metodu
        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                // Email ve şifre ile Firebase'e soruyoruz
                var userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);
                // Başarılıysa kullanıcı token'ını (kimlik anahtarını) döndürüyoruz
                return await userCredential.User.GetIdTokenAsync();
            }
            catch
            {
                // Hata olursa null döndür (Login sayfasında kontrol edeceğiz)
                return null;
            }
        }

        // Kayıt Olma Metodu
        public async Task<string> RegisterAsync(string email, string password)
        {
            try
            {
                // Yeni kullanıcı oluşturuyoruz
                var userCredential = await client.CreateUserWithEmailAndPasswordAsync(email, password);
                // Oluşan kullanıcının token'ını alıyoruz
                return await userCredential.User.GetIdTokenAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}