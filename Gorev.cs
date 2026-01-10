using System;

namespace Odev3Proje
{
    public class Gorev
    {
        public string Key { get; set; }
        public string Baslik { get; set; }
        public string Detay { get; set; }
        public DateTime TarihSaat { get; set; }
        public bool YapildiMi { get; set; }

        public string TarihGosterim => TarihSaat.ToString("dd.MM.yyyy");
        public string SaatGosterim => TarihSaat.ToString("HH:mm");
    }
}