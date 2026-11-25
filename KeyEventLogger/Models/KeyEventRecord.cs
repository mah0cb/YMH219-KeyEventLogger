using System.Globalization;

namespace KeyEventLogger.Models;

/// <summary>Tek bir klavye olayını temsil eden, oluşturulduktan sonra değişmeyen kayıt.</summary>
public class KeyEventRecord
{
    public KeyEventRecord(DateTime zaman, OlayTipi olayTipi, string tusAdi, Keys keyCode, Keys degistiriciler)
    {
        Zaman = zaman;
        OlayTipi = olayTipi;
        TusAdi = tusAdi;
        KeyCode = keyCode;
        Ctrl = degistiriciler.HasFlag(Keys.Control);
        Shift = degistiriciler.HasFlag(Keys.Shift);
        Alt = degistiriciler.HasFlag(Keys.Alt);
    }

    public DateTime Zaman { get; }
    public OlayTipi OlayTipi { get; }
    public string TusAdi { get; }

    /// <summary>Tuş kodu. KeyPress olayında tuş kodu bulunmadığı için Keys.None olur.</summary>
    public Keys KeyCode { get; }

    public bool Ctrl { get; }
    public bool Shift { get; }
    public bool Alt { get; }

    /// <summary>Zamanın milisaniyeli metin hali.</summary>
    public string ZamanMetni => Zaman.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

    /// <summary>Basılı değiştirici tuşlar, örn. "Ctrl+Shift". Hiçbiri yoksa "-".</summary>
    public string DegistiriciMetni
    {
        get
        {
            var adlar = new List<string>();
            if (Ctrl) adlar.Add("Ctrl");
            if (Shift) adlar.Add("Shift");
            if (Alt) adlar.Add("Alt");
            return adlar.Count == 0 ? "-" : string.Join("+", adlar);
        }
    }

    /// <summary>KeyDown ve KeyUp olayından kayıt üretir.</summary>
    public static KeyEventRecord Olustur(OlayTipi olayTipi, KeyEventArgs e) =>
        new(DateTime.Now, olayTipi, TusAdiBul(e.KeyCode), e.KeyCode, e.Modifiers);

    /// <summary>KeyPress olayından kayıt üretir.</summary>
    public static KeyEventRecord Olustur(KeyPressEventArgs e, Keys degistiriciler) =>
        new(DateTime.Now, OlayTipi.KeyPress, KarakterAdiBul(e.KeyChar), Keys.None, degistiriciler);

    public override string ToString() => $"{ZamanMetni} | {OlayTipi} | {TusAdi} | {DegistiriciMetni}";

    // Keys içinde aynı değere sahip adlar var (Enter/Return, PageDown/Next); okunaklı olanı seçilir.
    private static string TusAdiBul(Keys keyCode) => keyCode switch
    {
        Keys.Enter => "Enter",
        Keys.PageUp => "PageUp",
        Keys.PageDown => "PageDown",
        Keys.CapsLock => "CapsLock",
        Keys.PrintScreen => "PrintScreen",
        >= Keys.D0 and <= Keys.D9 => ((char)keyCode).ToString(),
        _ => keyCode.ToString()
    };

    private static string KarakterAdiBul(char karakter) => karakter switch
    {
        ' ' => "Space",
        '\r' => "Enter",
        '\n' => "Ctrl+Enter",
        '\t' => "Tab",
        '\b' => "Backspace",
        (char)27 => "Escape",
        _ when char.IsControl(karakter) => $"0x{(int)karakter:X2}",
        _ => karakter.ToString()
    };
}
