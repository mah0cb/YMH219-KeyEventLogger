using KeyEventLogger.Models;
using KeyEventLogger.Services;

namespace KeyEventLogger.Forms;

/// <summary>Klavye olay kaydedicinin ana penceresi. Olayları yalnızca bu pencere odaktayken alır.</summary>
public partial class AnaForm : Form
{
    private readonly KeyEventLoggerService _logger = new();

    public AnaForm()
    {
        InitializeComponent();
        _logger.RecordAdded += Logger_RecordAdded;
        DurumuGuncelle();
        ActiveControl = txtYazmaAlani;
    }

    // KeyPreview açık olduğu için tuş olayları önce forma gelir.
    private void AnaForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (chkKeyDown.Checked)
        {
            _logger.Add(KeyEventRecord.Olustur(OlayTipi.KeyDown, e));
        }
        MenuOdaginiEngelle(e);
    }

    private void AnaForm_KeyUp(object? sender, KeyEventArgs e)
    {
        if (chkKeyUp.Checked)
        {
            _logger.Add(KeyEventRecord.Olustur(OlayTipi.KeyUp, e));
        }
        MenuOdaginiEngelle(e);
    }

    private void AnaForm_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (chkKeyPress.Checked)
        {
            _logger.Add(KeyEventRecord.Olustur(e, ModifierKeys));
        }
    }

    /// <summary>Alt ve F10 pencere menüsüne odaklanıp sonraki tuşu yutmasın diye kayıt sırasında engellenir.</summary>
    private void MenuOdaginiEngelle(KeyEventArgs e)
    {
        if (_logger.IsRecording && (e.KeyCode is Keys.Menu or Keys.F10))
        {
            e.Handled = true;
        }
    }

    private void Logger_RecordAdded(object? sender, KeyEventRecord kayit)
    {
        ListViewItem oge = OgeOlustur(kayit);
        lvKayitlar.Items.Add(oge);
        oge.EnsureVisible();
        KayitSayisiniGoster();
    }

    private void btnBaslat_Click(object? sender, EventArgs e)
    {
        _logger.Start();
        DurumuGuncelle();
        YazmaAlaninaOdaklan();
    }

    private void btnDurdur_Click(object? sender, EventArgs e)
    {
        _logger.Stop();
        DurumuGuncelle();
        YazmaAlaninaOdaklan();
    }

    private void btnTemizle_Click(object? sender, EventArgs e)
    {
        _logger.Clear();
        txtYazmaAlani.Clear();
        lvKayitlar.Items.Clear();
        KayitSayisiniGoster();
        YazmaAlaninaOdaklan();
    }

    private void OlaySecimi_Click(object? sender, EventArgs e) => YazmaAlaninaOdaklan();

    /// <summary>Space/Enter bir butonu tetiklemesin diye odağı yazma alanına verir.</summary>
    private void YazmaAlaninaOdaklan() => txtYazmaAlani.Focus();

    private void DurumuGuncelle()
    {
        bool acik = _logger.IsRecording;
        Color renk = acik ? Color.Green : Color.Red;
        lblDurumNokta.ForeColor = renk;
        lblDurum.ForeColor = renk;
        lblDurum.Text = acik ? "Kayıt AÇIK" : "Kayıt KAPALI";
        btnBaslat.Enabled = !acik;
        btnDurdur.Enabled = acik;

        // Kayıt sırasında Tab yazma alanında kalır; Ctrl+Tab ile de butonlara geçilemez.
        txtYazmaAlani.AcceptsTab = acik;
        foreach (ButtonBase kontrol in Controls.OfType<ButtonBase>())
        {
            kontrol.TabStop = !acik;
        }
        KayitSayisiniGoster();
    }

    private static ListViewItem OgeOlustur(KeyEventRecord kayit) => new(new[]
    {
        kayit.ZamanMetni,
        kayit.OlayTipi.ToString(),
        kayit.TusAdi,
        kayit.KeyCode == Keys.None ? "-" : kayit.KeyCode.ToString(),
        kayit.DegistiriciMetni
    });

    private void KayitSayisiniGoster() =>
        lblDurumCubugu.Text = $"Toplam kayıt: {_logger.Records.Count}";
}
