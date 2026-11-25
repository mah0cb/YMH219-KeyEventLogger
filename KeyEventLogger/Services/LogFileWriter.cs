using System.Security;
using System.Text;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>Klavye kayıtlarını .txt dosyasına yazar.</summary>
public class LogFileWriter
{
    /// <summary>Kayıtları dosyaya yazar. Hata olursa false döner ve Türkçe hata mesajını verir.</summary>
    public bool Yaz(string dosyaYolu, IEnumerable<KeyEventRecord> kayitlar, out string hata)
    {
        hata = "";
        try
        {
            using var yazici = new StreamWriter(dosyaYolu, append: false, Encoding.UTF8);
            foreach (KeyEventRecord kayit in kayitlar)
            {
                yazici.WriteLine(kayit);
            }
            return true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or SecurityException)
        {
            hata = "Bu dosyaya yazma izniniz yok. Başka bir konum seçin.";
        }
        catch (Exception ex) when (ex is IOException or ArgumentException or NotSupportedException)
        {
            hata = $"Dosya yazılamadı: {ex.Message}";
        }
        return false;
    }
}
