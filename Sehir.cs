namespace Odev3Proje
{
    public class Sehir
    {
        public string Isim { get; set; } // Şehir Adı 

        // MGM'den o şehrin hava durumu resmini getiren link
        public string ResimUrl => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={IsimDuzelt(Isim)}&basla=1&bitir=5&rC=111&rZ=fff";

        // Türkçe karakterleri düzelten fonksiyon 
        private string IsimDuzelt(string sehir)
        {
            if (string.IsNullOrEmpty(sehir)) return "";
            return sehir.ToUpper()
                .Replace("Ç", "C")
                .Replace("Ğ", "G")
                .Replace("İ", "I")
                .Replace("Ö", "O")
                .Replace("Ş", "S")
                .Replace("Ü", "U")
                .Replace(" ", ""); // Boşlukları sil
        }
    }
}