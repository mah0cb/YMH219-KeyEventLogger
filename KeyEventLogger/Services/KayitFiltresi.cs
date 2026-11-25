using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>Kayıtları tarih/saat aralığına göre süzer. Sınırlar saniye düzeyindedir ve aralığa dahildir.</summary>
public class KayitFiltresi
{
    private readonly DateTime _baslangic;
    private readonly DateTime _bitisSonrasi;

    public KayitFiltresi(DateTime baslangic, DateTime bitis)
    {
        _baslangic = SaniyeyeYuvarla(baslangic);
        // Seçilen son saniyenin milisaniyeleri de aralığa girsin.
        _bitisSonrasi = SaniyeyeYuvarla(bitis).AddSeconds(1);
    }

    public bool Uygun(KeyEventRecord kayit) => kayit.Zaman >= _baslangic && kayit.Zaman < _bitisSonrasi;

    public IReadOnlyList<KeyEventRecord> Uygula(IEnumerable<KeyEventRecord> kayitlar) =>
        kayitlar.Where(Uygun).ToList();

    private static DateTime SaniyeyeYuvarla(DateTime zaman) =>
        new(zaman.Ticks - zaman.Ticks % TimeSpan.TicksPerSecond, zaman.Kind);
}
