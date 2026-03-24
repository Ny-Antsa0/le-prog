namespace jdPoint;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        // Déclarations
        this.pnlGrid          = new System.Windows.Forms.Panel();
        this.grpGameMode      = new System.Windows.Forms.GroupBox();
        this.rbPvP            = new System.Windows.Forms.RadioButton();
        this.rbPvAI           = new System.Windows.Forms.RadioButton();
        this.grpGridSettings  = new System.Windows.Forms.GroupBox();
        this.lblColumns       = new System.Windows.Forms.Label();
        this.numColumns       = new System.Windows.Forms.NumericUpDown();
        this.lblRows          = new System.Windows.Forms.Label();
        this.numRows          = new System.Windows.Forms.NumericUpDown();
        this.btnApply         = new System.Windows.Forms.Button();
        this.grpGameControls  = new System.Windows.Forms.GroupBox();
        this.btnRestart       = new System.Windows.Forms.Button();
        this.lblSaveName      = new System.Windows.Forms.Label();
        this.txtSaveName      = new System.Windows.Forms.TextBox();
        this.btnSave          = new System.Windows.Forms.Button();
        this.btnLoad          = new System.Windows.Forms.Button();
        this.lblCurrentPlayer = new System.Windows.Forms.Label();
        this.grpAction        = new System.Windows.Forms.GroupBox();
        this.rbPlacePoint     = new System.Windows.Forms.RadioButton();
        this.rbFireMissile    = new System.Windows.Forms.RadioButton();
        this.grpMissile       = new System.Windows.Forms.GroupBox();
        this.lblTargetRow     = new System.Windows.Forms.Label();
        this.lblPower         = new System.Windows.Forms.Label();
        this.numPower         = new System.Windows.Forms.NumericUpDown();
        this.btnFireMissile   = new System.Windows.Forms.Button();

        this.grpGameMode.SuspendLayout();
        this.grpGridSettings.SuspendLayout();
        this.grpGameControls.SuspendLayout();
        this.grpAction.SuspendLayout();
        this.grpMissile.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPower)).BeginInit();
        this.SuspendLayout();

        // ──────────────────────────────────────────────────
        // RANGÉE 1  (Y=12, hauteur=75)
        // Disposition : [Mode 200] [Grille 320] [Contrôles 380] [Joueur 170]
        // ──────────────────────────────────────────────────

        // grpGameMode  X=12
        this.grpGameMode.Location  = new System.Drawing.Point(12, 12);
        this.grpGameMode.Size      = new System.Drawing.Size(200, 75);
        this.grpGameMode.Text      = "🎮 Mode de jeu";
        this.grpGameMode.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.grpGameMode.ForeColor = System.Drawing.Color.White;
        this.grpGameMode.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
        this.grpGameMode.TabIndex  = 0;
        this.grpGameMode.TabStop   = false;
        this.grpGameMode.Controls.Add(this.rbPvP);
        this.grpGameMode.Controls.Add(this.rbPvAI);

        this.rbPvP.Text      = "👥 Joueur vs Joueur";
        this.rbPvP.Location  = new System.Drawing.Point(12, 22);
        this.rbPvP.Size      = new System.Drawing.Size(170, 20);
        this.rbPvP.Checked   = true;
        this.rbPvP.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.rbPvP.ForeColor = System.Drawing.Color.White;
        this.rbPvP.TabIndex  = 0;

        this.rbPvAI.Text      = "🤖 Joueur vs IA";
        this.rbPvAI.Location  = new System.Drawing.Point(12, 46);
        this.rbPvAI.Size      = new System.Drawing.Size(170, 20);
        this.rbPvAI.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.rbPvAI.ForeColor = System.Drawing.Color.White;
        this.rbPvAI.TabIndex  = 1;

        // grpGridSettings  X=224
        this.grpGridSettings.Location  = new System.Drawing.Point(224, 12);
        this.grpGridSettings.Size      = new System.Drawing.Size(320, 75);
        this.grpGridSettings.Text      = "⚙️ Paramètres de la grille";
        this.grpGridSettings.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.grpGridSettings.ForeColor = System.Drawing.Color.White;
        this.grpGridSettings.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
        this.grpGridSettings.TabIndex  = 1;
        this.grpGridSettings.TabStop   = false;
        this.grpGridSettings.Controls.Add(this.lblColumns);
        this.grpGridSettings.Controls.Add(this.numColumns);
        this.grpGridSettings.Controls.Add(this.lblRows);
        this.grpGridSettings.Controls.Add(this.numRows);
        this.grpGridSettings.Controls.Add(this.btnApply);

        this.lblColumns.Text      = "Largeur :";
        this.lblColumns.Location  = new System.Drawing.Point(12, 24);
        this.lblColumns.Size      = new System.Drawing.Size(65, 18);
        this.lblColumns.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.lblColumns.ForeColor = System.Drawing.Color.White;
        this.lblColumns.TabIndex  = 0;

        this.numColumns.Location = new System.Drawing.Point(80, 22);
        this.numColumns.Size     = new System.Drawing.Size(55, 22);
        this.numColumns.Minimum  = 5;
        this.numColumns.Maximum  = 20;
        this.numColumns.Value    = 10;
        this.numColumns.TabIndex = 1;

        this.lblRows.Text      = "Hauteur :";
        this.lblRows.Location  = new System.Drawing.Point(12, 50);
        this.lblRows.Size      = new System.Drawing.Size(65, 18);
        this.lblRows.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.lblRows.ForeColor = System.Drawing.Color.White;
        this.lblRows.TabIndex  = 2;

        this.numRows.Location = new System.Drawing.Point(80, 48);
        this.numRows.Size     = new System.Drawing.Size(55, 22);
        this.numRows.Minimum  = 5;
        this.numRows.Maximum  = 20;
        this.numRows.Value    = 10;
        this.numRows.TabIndex = 3;

        this.btnApply.Text     = "Appliquer";
        this.btnApply.Location = new System.Drawing.Point(155, 32);
        this.btnApply.Size     = new System.Drawing.Size(100, 28);
        this.btnApply.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnApply.TabIndex = 4;
        this.btnApply.Click   += new System.EventHandler(this.btnApply_Click);

        // grpGameControls  X=556
        this.grpGameControls.Location  = new System.Drawing.Point(556, 12);
        this.grpGameControls.Size      = new System.Drawing.Size(420, 75);
        this.grpGameControls.Text      = "🎯 Contrôles de jeu";
        this.grpGameControls.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.grpGameControls.ForeColor = System.Drawing.Color.White;
        this.grpGameControls.BackColor = System.Drawing.Color.FromArgb(200, 100, 0);
        this.grpGameControls.TabIndex  = 2;
        this.grpGameControls.TabStop   = false;
        this.grpGameControls.Controls.Add(this.btnRestart);
        this.grpGameControls.Controls.Add(this.lblSaveName);
        this.grpGameControls.Controls.Add(this.txtSaveName);
        this.grpGameControls.Controls.Add(this.btnSave);
        this.grpGameControls.Controls.Add(this.btnLoad);

        this.btnRestart.Text     = "Recommencer";
        this.btnRestart.Location = new System.Drawing.Point(10, 28);
        this.btnRestart.Size     = new System.Drawing.Size(110, 28);
        this.btnRestart.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnRestart.TabIndex = 0;
        this.btnRestart.Click   += new System.EventHandler(this.btnRestart_Click);

        this.lblSaveName.Text      = "Nom :";
        this.lblSaveName.Location  = new System.Drawing.Point(130, 34);
        this.lblSaveName.Size      = new System.Drawing.Size(38, 18);
        this.lblSaveName.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.lblSaveName.ForeColor = System.Drawing.Color.White;
        this.lblSaveName.TabIndex  = 1;

        this.txtSaveName.Text     = "partie1";
        this.txtSaveName.Location = new System.Drawing.Point(172, 31);
        this.txtSaveName.Size     = new System.Drawing.Size(90, 22);
        this.txtSaveName.Font     = new System.Drawing.Font("Segoe UI", 9F);
        this.txtSaveName.TabIndex = 2;

        this.btnSave.Text     = "Sauver";
        this.btnSave.Location = new System.Drawing.Point(270, 28);
        this.btnSave.Size     = new System.Drawing.Size(70, 28);
        this.btnSave.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnSave.TabIndex = 3;
        this.btnSave.Click   += new System.EventHandler(this.btnSave_Click);

        this.btnLoad.Text     = "Charger";
        this.btnLoad.Location = new System.Drawing.Point(348, 28);
        this.btnLoad.Size     = new System.Drawing.Size(70, 28);
        this.btnLoad.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnLoad.TabIndex = 4;
        this.btnLoad.Click   += new System.EventHandler(this.btnLoad_Click);

        // lblCurrentPlayer  X=988 (collé à droite)
        this.lblCurrentPlayer.Text      = "🔴 Joueur Rouge";
        this.lblCurrentPlayer.Location  = new System.Drawing.Point(988, 12);
        this.lblCurrentPlayer.Size      = new System.Drawing.Size(188, 75);
        this.lblCurrentPlayer.Font      = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblCurrentPlayer.ForeColor = System.Drawing.Color.White;
        this.lblCurrentPlayer.BackColor = System.Drawing.Color.FromArgb(0, 150, 136);
        this.lblCurrentPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblCurrentPlayer.TabIndex  = 11;

        // ──────────────────────────────────────────────────
        // RANGÉE 2  (Y=100, hauteur=80)
        // Disposition : [Action 200] [Missile 300] — même Top
        // ──────────────────────────────────────────────────

        // grpAction  X=12
        this.grpAction.Location  = new System.Drawing.Point(12, 100);
        this.grpAction.Size      = new System.Drawing.Size(200, 80);
        this.grpAction.Text      = "🎲 Action du tour";
        this.grpAction.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.grpAction.ForeColor = System.Drawing.Color.White;
        this.grpAction.BackColor = System.Drawing.Color.FromArgb(56, 142, 60);
        this.grpAction.TabIndex  = 3;
        this.grpAction.TabStop   = false;
        this.grpAction.Controls.Add(this.rbPlacePoint);
        this.grpAction.Controls.Add(this.rbFireMissile);

        this.rbPlacePoint.Text      = "📍 Poser un point";
        this.rbPlacePoint.Location  = new System.Drawing.Point(12, 22);
        this.rbPlacePoint.Size      = new System.Drawing.Size(170, 20);
        this.rbPlacePoint.Checked   = true;
        this.rbPlacePoint.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.rbPlacePoint.ForeColor = System.Drawing.Color.White;
        this.rbPlacePoint.TabIndex  = 0;

        this.rbFireMissile.Text      = "🚀 Tirer un missile";
        this.rbFireMissile.Location  = new System.Drawing.Point(12, 46);
        this.rbFireMissile.Size      = new System.Drawing.Size(170, 20);
        this.rbFireMissile.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.rbFireMissile.ForeColor = System.Drawing.Color.White;
        this.rbFireMissile.TabIndex  = 1;

        // grpMissile  X=224
        this.grpMissile.Location  = new System.Drawing.Point(224, 100);
        this.grpMissile.Size      = new System.Drawing.Size(300, 80);
        this.grpMissile.Text      = "🚀 Missile";
        this.grpMissile.Enabled   = false;
        this.grpMissile.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.grpMissile.ForeColor = System.Drawing.Color.White;
        this.grpMissile.BackColor = System.Drawing.Color.FromArgb(183, 28, 28);
        this.grpMissile.TabIndex  = 4;
        this.grpMissile.TabStop   = false;
        this.grpMissile.Controls.Add(this.lblTargetRow);
        this.grpMissile.Controls.Add(this.lblPower);
        this.grpMissile.Controls.Add(this.numPower);
        this.grpMissile.Controls.Add(this.btnFireMissile);

        this.lblTargetRow.Text      = "Ligne cible : calculée";
        this.lblTargetRow.Location  = new System.Drawing.Point(10, 22);
        this.lblTargetRow.Size      = new System.Drawing.Size(160, 18);
        this.lblTargetRow.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.lblTargetRow.ForeColor = System.Drawing.Color.White;
        this.lblTargetRow.TabIndex  = 0;

        this.lblPower.Text      = "Puissance :";
        this.lblPower.Location  = new System.Drawing.Point(10, 48);
        this.lblPower.Size      = new System.Drawing.Size(75, 18);
        this.lblPower.Font      = new System.Drawing.Font("Segoe UI", 9F);
        this.lblPower.ForeColor = System.Drawing.Color.White;
        this.lblPower.TabIndex  = 1;

        this.numPower.Location = new System.Drawing.Point(90, 46);
        this.numPower.Size     = new System.Drawing.Size(55, 22);
        this.numPower.Minimum  = 1;
        this.numPower.Maximum  = 9;
        this.numPower.Value    = 5;
        this.numPower.TabIndex = 2;

        this.btnFireMissile.Text     = "Tirer 🚀";
        this.btnFireMissile.Location = new System.Drawing.Point(158, 43);
        this.btnFireMissile.Size     = new System.Drawing.Size(80, 28);
        this.btnFireMissile.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnFireMissile.TabIndex = 3;
        this.btnFireMissile.Click   += new System.EventHandler(this.btnFireMissile_Click);

        // ──────────────────────────────────────────────────
        // GRILLE DE JEU  (Y=194, prend toute la largeur)
        // ──────────────────────────────────────────────────

        this.pnlGrid.Location    = new System.Drawing.Point(12, 194);
        this.pnlGrid.Size        = new System.Drawing.Size(1160, 320);
        this.pnlGrid.Anchor      = System.Windows.Forms.AnchorStyles.Top
                                 | System.Windows.Forms.AnchorStyles.Bottom
                                 | System.Windows.Forms.AnchorStyles.Left
                                 | System.Windows.Forms.AnchorStyles.Right;
        this.pnlGrid.BackColor   = System.Drawing.Color.FromArgb(232, 244, 253);
        this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlGrid.TabIndex    = 5;
        this.pnlGrid.Paint      += new System.Windows.Forms.PaintEventHandler(this.pnlGrid_Paint);
        this.pnlGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlGrid_MouseClick);

        // ──────────────────────────────────────────────────
        // FORMULAIRE PRINCIPAL
        // ──────────────────────────────────────────────────

        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor           = System.Drawing.Color.FromArgb(30, 30, 40);
        this.ClientSize          = new System.Drawing.Size(1184, 530);
        this.MinimumSize         = new System.Drawing.Size(700, 400);
        this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text                = "Jeu de points avec missiles";
        this.Name                = "Form1";

        this.Controls.Add(this.grpGameMode);
        this.Controls.Add(this.grpGridSettings);
        this.Controls.Add(this.grpGameControls);
        this.Controls.Add(this.lblCurrentPlayer);
        this.Controls.Add(this.grpAction);
        this.Controls.Add(this.grpMissile);
        this.Controls.Add(this.pnlGrid);

        ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPower)).EndInit();
        this.grpGameMode.ResumeLayout(false);
        this.grpGameMode.PerformLayout();
        this.grpGridSettings.ResumeLayout(false);
        this.grpGridSettings.PerformLayout();
        this.grpGameControls.ResumeLayout(false);
        this.grpGameControls.PerformLayout();
        this.grpAction.ResumeLayout(false);
        this.grpAction.PerformLayout();
        this.grpMissile.ResumeLayout(false);
        this.grpMissile.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Panel pnlGrid;
    private System.Windows.Forms.Label lblColumns;
    private System.Windows.Forms.Label lblRows;
    private System.Windows.Forms.NumericUpDown numColumns;
    private System.Windows.Forms.NumericUpDown numRows;
    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.Button btnRestart;
    private System.Windows.Forms.Label lblSaveName;
    private System.Windows.Forms.TextBox txtSaveName;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.GroupBox grpGameMode;
    private System.Windows.Forms.RadioButton rbPvP;
    private System.Windows.Forms.RadioButton rbPvAI;
    private System.Windows.Forms.GroupBox grpGridSettings;
    private System.Windows.Forms.GroupBox grpGameControls;
    private System.Windows.Forms.GroupBox grpAction;
    private System.Windows.Forms.RadioButton rbPlacePoint;
    private System.Windows.Forms.RadioButton rbFireMissile;
    private System.Windows.Forms.GroupBox grpMissile;
    private System.Windows.Forms.Label lblTargetRow;
    private System.Windows.Forms.Label lblPower;
    private System.Windows.Forms.NumericUpDown numPower;
    private System.Windows.Forms.Button btnFireMissile;
    private System.Windows.Forms.Label lblCurrentPlayer;
}