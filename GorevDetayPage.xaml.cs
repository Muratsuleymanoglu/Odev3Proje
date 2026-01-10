using System;
using Microsoft.Maui.Controls;
using Odev3Proje.Services; // Namespace eklendi
using Odev3Proje.Models;   // Namespace eklendi

namespace Odev3Proje
{
    public partial class GorevDetayPage : ContentPage
    {
        GorevService _service = new GorevService();
        Gorev _duzenlenecekGorev;
        bool _duzenlemeModu = false;

        // Constructor 1: Yeni kayıt için (Parametresiz)
        public GorevDetayPage()
        {
            InitializeComponent();
            _duzenlemeModu = false;
            // Tarih ve saati şimdiki zamana ayarla
            dpTarih.Date = DateTime.Now;
            tpSaat.Time = DateTime.Now.TimeOfDay;
        }

        // Constructor 2: Düzenleme için (Gorev parametresi alır)
        public GorevDetayPage(Gorev gorev)
        {
            InitializeComponent();
            _duzenlenecekGorev = gorev;
            _duzenlemeModu = true;

            // Gelen verileri kutucuklara doldur
            txtBaslik.Text = gorev.Baslik;
            txtDetay.Text = gorev.Detay;
            dpTarih.Date = gorev.TarihSaat.Date;
            tpSaat.Time = gorev.TarihSaat.TimeOfDay;
        }

        // Kaydet Butonu
        private async void Kaydet_Clicked(object sender, EventArgs e)
        {
            // Validasyon: Başlık boş olamaz
            if (string.IsNullOrWhiteSpace(txtBaslik.Text))
            {
                await DisplayAlert("Uyarı", "Başlık alanı boş geçilemez.", "Tamam");
                return;
            }

            // Seçilen Tarih ve Saati birleştir
            DateTime tamTarih = dpTarih.Date + tpSaat.Time;

            if (_duzenlemeModu)
            {
                // Var olan nesneyi güncelle
                _duzenlenecekGorev.Baslik = txtBaslik.Text;
                _duzenlenecekGorev.Detay = txtDetay.Text;
                _duzenlenecekGorev.TarihSaat = tamTarih;

                // Veritabanında güncelle
                await _service.GorevGuncelle(_duzenlenecekGorev);
            }
            else
            {
                // Yeni nesne oluştur
                var yeniGorev = new Gorev
                {
                    Baslik = txtBaslik.Text,
                    Detay = txtDetay.Text,
                    TarihSaat = tamTarih,
                    YapildiMi = false // Yeni görev yapılmamış olarak başlar
                };

                // Veritabanına ekle
                await _service.GorevEkle(yeniGorev);
            }

            // İşlem bitince önceki sayfaya dön
            await Navigation.PopAsync();
        }

        // İptal Butonu
        private async void Iptal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}