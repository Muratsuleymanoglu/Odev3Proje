using System.Globalization;

namespace Odev3Proje
{
    // Bu sınıf, Yapılacaklar sayfasındaki CheckBox işaretlendiğinde
    // metnin üzerini çizmek (Strikethrough) için kullanılır.
    public class BoolToStrikethroughConverter : IValueConverter
    {
        // Değer sayfadan gelirken (Binding) çalışır
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Eğer gelen değer bool ise ve true ise (iş yapıldıysa)
            if (value is bool isCompleted && isCompleted)
            {
                // Metnin üzerini çiz
                return TextDecorations.Strikethrough;
            }
            // Değilse normal yazı olsun
            return TextDecorations.None;
        }

        // Değer arayüzden koda giderken çalışır (Burada kullanılmıyor)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}