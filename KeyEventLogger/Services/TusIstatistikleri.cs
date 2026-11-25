using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>Kayıtlardan tuş istatistiği çıkarır. Aynı basış tekrar sayılmasın diye yalnızca KeyDown kullanılır.</summary>
public class TusIstatistikleri
{
    private const int EnSikTusSayisi = 5;

    // ponytail: her tuşta tüm kayıtlar yeniden sayılır; on binlerce kayıtta yavaşlarsa sayaç sözlüğüne geçilir.
    public TusIstatistikleri(IEnumerable<KeyEventRecord> kayitlar)
    {
        List<KeyEventRecord> basislar = kayitlar.Where(k => k.OlayTipi == OlayTipi.KeyDown).ToList();

        ToplamTus = basislar.Count;
        EnSikTuslar = basislar
            .GroupBy(k => k.TusAdi)
            .Select(grup => (TusAdi: grup.Key, Adet: grup.Count()))
            .OrderByDescending(tus => tus.Adet)
            .Take(EnSikTusSayisi)
            .ToList();
    }

    public int ToplamTus { get; }

    /// <summary>En çok basılan tuşlar, çoktan aza sıralı.</summary>
    public IReadOnlyList<(string TusAdi, int Adet)> EnSikTuslar { get; }
}
