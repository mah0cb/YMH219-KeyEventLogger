using KeyEventLogger.Models;
using KeyEventLogger.Services;

namespace KeyEventLogger.Forms;

/// <summary>Klavye olay kaydedicinin ana penceresi. Olayları yalnızca bu pencere odaktayken alır.</summary>
public partial class AnaForm : Form
{
    private readonly KeyEventLoggerService _logger = new();
    private readonly LogFileWriter _dosyaYazici = new();
    private KayitFiltresi? _filtre;

    public AnaForm()
    {
        InitializeComponent();
        dtpBaslangic.Value = DateTime.Today;
        dtpBitis.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
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
        if (_filtre is null || _filtre.Uygun(kayit))
        {
            ListViewItem oge = OgeOlustur(kayit);
            lvKayitlar.Items.Add(oge);
            oge.EnsureVisible();
        }
        KayitSayisiniGoster();
        IstatistikleriGuncelle();
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
        IstatistikleriGuncelle();
        YazmaAlaninaOdaklan();
    }

    private void btnKaydet_Click(object? sender, EventArgs e)
    {
        IReadOnlyList<KeyEventRecord> kayitlar = GorunenKayitlar();
        if (kayitlar.Count == 0)
        {
            MessageBox.Show(this, "Kaydedilecek kayıt yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            sfdKaydet.FileName = $"klavye-kayitlari-{DateTime.Now:yyyyMMdd-HHmmss}.txt";
            if (sfdKaydet.ShowDialog(this) == DialogResult.OK)
            {
                DosyayaKaydet(sfdKaydet.FileName, kayitlar);
            }
        }
        YazmaAlaninaOdaklan();
    }

    private void DosyayaKaydet(string dosyaYolu, IReadOnlyList<KeyEventRecord> kayitlar)
    {
        if (_dosyaYazici.Yaz(dosyaYolu, kayitlar, out string hata))
        {
            lblDurumCubugu.Text = $"{kayitlar.Count} kayıt yazıldı: {dosyaYolu}";
        }
        else
        {
            MessageBox.Show(this, hata, "Kaydetme hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnFiltrele_Click(object? sender, EventArgs e)
    {
        if (dtpBaslangic.Value > dtpBitis.Value)
        {
            MessageBox.Show(this, "Başlangıç zamanı bitiş zamanından sonra olamaz.", "Geçersiz aralık",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            _filtre = new KayitFiltresi(dtpBaslangic.Value, dtpBitis.Value);
            ListeyiYenile();
        }
        YazmaAlaninaOdaklan();
    }

    private void btnFiltreTemizle_Click(object? sender, EventArgs e)
    {
        _filtre = null;
        ListeyiYenile();
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

    /// <summary>Filtre varsa filtrelenmiş, yoksa tüm kayıtları verir.</summary>
    private IReadOnlyList<KeyEventRecord> GorunenKayitlar() =>
        _filtre is null ? _logger.Records : _filtre.Uygula(_logger.Records);

    private void ListeyiYenile()
    {
        lvKayitlar.BeginUpdate();
        lvKayitlar.Items.Clear();
        lvKayitlar.Items.AddRange(GorunenKayitlar().Select(OgeOlustur).ToArray());
        lvKayitlar.EndUpdate();
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
        lblDurumCubugu.Text = $"Görüntülenen: {lvKayitlar.Items.Count} / Toplam kayıt: {_logger.Records.Count}"
            + (_filtre is null ? "" : " (filtre açık)");

    private void IstatistikleriGuncelle()
    {
        var istatistik = new TusIstatistikleri(_logger.Records);
        lblToplamTus.Text = $"Toplam tuş sayısı: {istatistik.ToplamTus}";
        lstEnSikTuslar.Items.Clear();
        foreach ((string tusAdi, int adet) in istatistik.EnSikTuslar)
        {
            lstEnSikTuslar.Items.Add($"{lstEnSikTuslar.Items.Count + 1}. {tusAdi}: {adet}");
        }
    }
}
