namespace KeyEventLogger.Forms;

partial class AnaForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        btnBaslat = new Button();
        btnDurdur = new Button();
        btnTemizle = new Button();
        btnKaydet = new Button();
        lblDurumNokta = new Label();
        lblDurum = new Label();
        lblOlaylar = new Label();
        chkKeyDown = new CheckBox();
        chkKeyUp = new CheckBox();
        chkKeyPress = new CheckBox();
        lblYazmaAlani = new Label();
        txtYazmaAlani = new TextBox();
        lvKayitlar = new ListView();
        clmZaman = new ColumnHeader();
        clmOlay = new ColumnHeader();
        clmTus = new ColumnHeader();
        clmKeyCode = new ColumnHeader();
        clmDegistirici = new ColumnHeader();
        stsDurum = new StatusStrip();
        lblDurumCubugu = new ToolStripStatusLabel();
        sfdKaydet = new SaveFileDialog();
        stsDurum.SuspendLayout();
        SuspendLayout();
        // 
        // btnBaslat
        // 
        btnBaslat.Location = new Point(12, 12);
        btnBaslat.Name = "btnBaslat";
        btnBaslat.Size = new Size(100, 30);
        btnBaslat.TabIndex = 0;
        btnBaslat.Text = "Başlat";
        btnBaslat.UseVisualStyleBackColor = true;
        btnBaslat.Click += btnBaslat_Click;
        // 
        // btnDurdur
        // 
        btnDurdur.Location = new Point(118, 12);
        btnDurdur.Name = "btnDurdur";
        btnDurdur.Size = new Size(100, 30);
        btnDurdur.TabIndex = 1;
        btnDurdur.Text = "Durdur";
        btnDurdur.UseVisualStyleBackColor = true;
        btnDurdur.Click += btnDurdur_Click;
        // 
        // btnTemizle
        // 
        btnTemizle.Location = new Point(224, 12);
        btnTemizle.Name = "btnTemizle";
        btnTemizle.Size = new Size(100, 30);
        btnTemizle.TabIndex = 2;
        btnTemizle.Text = "Temizle";
        btnTemizle.UseVisualStyleBackColor = true;
        btnTemizle.Click += btnTemizle_Click;
        // 
        // btnKaydet
        // 
        btnKaydet.Location = new Point(330, 12);
        btnKaydet.Name = "btnKaydet";
        btnKaydet.Size = new Size(130, 30);
        btnKaydet.TabIndex = 3;
        btnKaydet.Text = ".txt olarak kaydet";
        btnKaydet.UseVisualStyleBackColor = true;
        btnKaydet.Click += btnKaydet_Click;
        // 
        // lblDurumNokta
        // 
        lblDurumNokta.AutoSize = true;
        lblDurumNokta.Font = new Font("Segoe UI", 18F);
        lblDurumNokta.ForeColor = Color.Red;
        lblDurumNokta.Location = new Point(480, 8);
        lblDurumNokta.Name = "lblDurumNokta";
        lblDurumNokta.TabIndex = 4;
        lblDurumNokta.Text = "●";
        // 
        // lblDurum
        // 
        lblDurum.AutoSize = true;
        lblDurum.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDurum.ForeColor = Color.Red;
        lblDurum.Location = new Point(515, 16);
        lblDurum.Name = "lblDurum";
        lblDurum.TabIndex = 5;
        lblDurum.Text = "Kayıt KAPALI";
        // 
        // lblOlaylar
        // 
        lblOlaylar.AutoSize = true;
        lblOlaylar.Location = new Point(12, 53);
        lblOlaylar.Name = "lblOlaylar";
        lblOlaylar.TabIndex = 6;
        lblOlaylar.Text = "Yakalanacak olaylar:";
        // 
        // chkKeyDown
        // 
        chkKeyDown.AutoSize = true;
        chkKeyDown.CheckState = CheckState.Checked;
        chkKeyDown.Checked = true;
        chkKeyDown.Location = new Point(140, 52);
        chkKeyDown.Name = "chkKeyDown";
        chkKeyDown.TabIndex = 7;
        chkKeyDown.Text = "KeyDown";
        chkKeyDown.UseVisualStyleBackColor = true;
        chkKeyDown.Click += OlaySecimi_Click;
        // 
        // chkKeyUp
        // 
        chkKeyUp.AutoSize = true;
        chkKeyUp.CheckState = CheckState.Checked;
        chkKeyUp.Checked = true;
        chkKeyUp.Location = new Point(230, 52);
        chkKeyUp.Name = "chkKeyUp";
        chkKeyUp.TabIndex = 8;
        chkKeyUp.Text = "KeyUp";
        chkKeyUp.UseVisualStyleBackColor = true;
        chkKeyUp.Click += OlaySecimi_Click;
        // 
        // chkKeyPress
        // 
        chkKeyPress.AutoSize = true;
        chkKeyPress.CheckState = CheckState.Checked;
        chkKeyPress.Checked = true;
        chkKeyPress.Location = new Point(310, 52);
        chkKeyPress.Name = "chkKeyPress";
        chkKeyPress.TabIndex = 9;
        chkKeyPress.Text = "KeyPress";
        chkKeyPress.UseVisualStyleBackColor = true;
        chkKeyPress.Click += OlaySecimi_Click;
        // 
        // lblYazmaAlani
        // 
        lblYazmaAlani.AutoSize = true;
        lblYazmaAlani.Location = new Point(12, 82);
        lblYazmaAlani.Name = "lblYazmaAlani";
        lblYazmaAlani.TabIndex = 10;
        lblYazmaAlani.Text = "Yazma alanı (kayıt açıkken buraya yazın):";
        // 
        // txtYazmaAlani
        // 
        txtYazmaAlani.AcceptsReturn = true;
        txtYazmaAlani.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtYazmaAlani.Location = new Point(12, 102);
        txtYazmaAlani.Multiline = true;
        txtYazmaAlani.Name = "txtYazmaAlani";
        txtYazmaAlani.ScrollBars = ScrollBars.Vertical;
        txtYazmaAlani.Size = new Size(940, 60);
        txtYazmaAlani.TabIndex = 11;
        // 
        // lvKayitlar
        // 
        lvKayitlar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lvKayitlar.Columns.AddRange(new ColumnHeader[] { clmZaman, clmOlay, clmTus, clmKeyCode, clmDegistirici });
        lvKayitlar.FullRowSelect = true;
        lvKayitlar.GridLines = true;
        lvKayitlar.Location = new Point(12, 216);
        lvKayitlar.Name = "lvKayitlar";
        lvKayitlar.Size = new Size(712, 372);
        lvKayitlar.TabIndex = 18;
        lvKayitlar.UseCompatibleStateImageBehavior = false;
        lvKayitlar.View = View.Details;
        // 
        // clmZaman
        // 
        clmZaman.Text = "Zaman";
        clmZaman.Width = 180;
        // 
        // clmOlay
        // 
        clmOlay.Text = "Olay";
        clmOlay.Width = 90;
        // 
        // clmTus
        // 
        clmTus.Text = "Tuş";
        clmTus.Width = 130;
        // 
        // clmKeyCode
        // 
        clmKeyCode.Text = "KeyCode";
        clmKeyCode.Width = 130;
        // 
        // clmDegistirici
        // 
        clmDegistirici.Text = "Değiştiriciler";
        clmDegistirici.Width = 150;
        // 
        // stsDurum
        // 
        stsDurum.Items.AddRange(new ToolStripItem[] { lblDurumCubugu });
        stsDurum.Location = new Point(0, 599);
        stsDurum.Name = "stsDurum";
        stsDurum.Size = new Size(964, 22);
        stsDurum.TabIndex = 20;
        // 
        // lblDurumCubugu
        // 
        lblDurumCubugu.Name = "lblDurumCubugu";
        lblDurumCubugu.Text = "Hazır";
        // 
        // sfdKaydet
        // 
        sfdKaydet.DefaultExt = "txt";
        sfdKaydet.Filter = "Metin dosyası (*.txt)|*.txt";
        sfdKaydet.Title = "Kayıtları kaydet";
        // 
        // AnaForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(964, 621);
        Controls.Add(btnBaslat);
        Controls.Add(btnDurdur);
        Controls.Add(btnTemizle);
        Controls.Add(btnKaydet);
        Controls.Add(lblDurumNokta);
        Controls.Add(lblDurum);
        Controls.Add(lblOlaylar);
        Controls.Add(chkKeyDown);
        Controls.Add(chkKeyUp);
        Controls.Add(chkKeyPress);
        Controls.Add(lblYazmaAlani);
        Controls.Add(txtYazmaAlani);
        Controls.Add(lvKayitlar);
        Controls.Add(stsDurum);
        KeyPreview = true;
        MinimumSize = new Size(940, 500);
        Name = "AnaForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Klavye Olay Kaydedici";
        KeyDown += AnaForm_KeyDown;
        KeyPress += AnaForm_KeyPress;
        KeyUp += AnaForm_KeyUp;
        stsDurum.ResumeLayout(false);
        stsDurum.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Button btnBaslat;
    private Button btnDurdur;
    private Button btnTemizle;
    private Button btnKaydet;
    private Label lblDurumNokta;
    private Label lblDurum;
    private Label lblOlaylar;
    private CheckBox chkKeyDown;
    private CheckBox chkKeyUp;
    private CheckBox chkKeyPress;
    private Label lblYazmaAlani;
    private TextBox txtYazmaAlani;
    private ListView lvKayitlar;
    private ColumnHeader clmZaman;
    private ColumnHeader clmOlay;
    private ColumnHeader clmTus;
    private ColumnHeader clmKeyCode;
    private ColumnHeader clmDegistirici;
    private StatusStrip stsDurum;
    private ToolStripStatusLabel lblDurumCubugu;
    private SaveFileDialog sfdKaydet;
}
