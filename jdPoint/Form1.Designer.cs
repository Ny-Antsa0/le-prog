namespace jdPoint;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.pnlGrid = new System.Windows.Forms.Panel();
        this.lblColumns = new System.Windows.Forms.Label();
        this.lblRows = new System.Windows.Forms.Label();
        this.numColumns = new System.Windows.Forms.NumericUpDown();
        this.numRows = new System.Windows.Forms.NumericUpDown();
        this.btnApply = new System.Windows.Forms.Button();
        this.btnRestart = new System.Windows.Forms.Button();
        this.lblSaveName = new System.Windows.Forms.Label();
        this.txtSaveName = new System.Windows.Forms.TextBox();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnLoad = new System.Windows.Forms.Button();
        this.lblCurrentPlayer = new System.Windows.Forms.Label();
        this.lblCurrentPlayerValue = new System.Windows.Forms.Label();
        this.lblMissilesPrimary = new System.Windows.Forms.Label();
        this.lblMissilesPrimaryValue = new System.Windows.Forms.Label();
        this.lblMissilesSecondary = new System.Windows.Forms.Label();
        this.lblMissilesSecondaryValue = new System.Windows.Forms.Label();
        this.lblMissilesConfig = new System.Windows.Forms.Label();
        this.numMissilesPrimary = new System.Windows.Forms.NumericUpDown();
        this.lblMissilesConfigSeparator = new System.Windows.Forms.Label();
        this.numMissilesSecondary = new System.Windows.Forms.NumericUpDown();
        this.lblMissilePower = new System.Windows.Forms.Label();
        this.numMissilePower = new System.Windows.Forms.NumericUpDown();
        this.lblMissileTargetColumn = new System.Windows.Forms.Label();
        this.numMissileTargetColumn = new System.Windows.Forms.NumericUpDown();
        this.lblMissileTargetRow = new System.Windows.Forms.Label();
        this.numMissileTargetRow = new System.Windows.Forms.NumericUpDown();
        this.btnFireMissile = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.numMissilePower)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissileTargetColumn)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissileTargetRow)).BeginInit();
        this.grpAction = new System.Windows.Forms.GroupBox();
        this.rdoActionMissile = new System.Windows.Forms.RadioButton();
        this.rdoActionPlace = new System.Windows.Forms.RadioButton();
        this.lblActionHint = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.numMissilesPrimary)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissilesSecondary)).BeginInit();
        this.grpAction.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
        this.SuspendLayout();
        // 
        // pnlGrid
        // 
        this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.pnlGrid.BackColor = System.Drawing.Color.White;
        this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlGrid.Location = new System.Drawing.Point(12, 162);
        this.pnlGrid.Name = "pnlGrid";
        this.pnlGrid.Size = new System.Drawing.Size(956, 276);
        this.pnlGrid.TabIndex = 0;
        this.pnlGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGrid_Paint);
        this.pnlGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlGrid_MouseClick);
        // 
        // lblColumns
        // 
        this.lblColumns.AutoSize = true;
        this.lblColumns.Location = new System.Drawing.Point(12, 20);
        this.lblColumns.Name = "lblColumns";
        this.lblColumns.Size = new System.Drawing.Size(113, 15);
        this.lblColumns.TabIndex = 1;
        this.lblColumns.Text = "Cases en largeur :";
        // 
        // lblRows
        // 
        this.lblRows.AutoSize = true;
        this.lblRows.Location = new System.Drawing.Point(260, 20);
        this.lblRows.Name = "lblRows";
        this.lblRows.Size = new System.Drawing.Size(113, 15);
        this.lblRows.TabIndex = 2;
        this.lblRows.Text = "Cases en hauteur :";
        // 
        // numColumns
        // 
        this.numColumns.Location = new System.Drawing.Point(131, 18);
        this.numColumns.Maximum = new decimal(new int[] {200, 0, 0, 0});
        this.numColumns.Minimum = new decimal(new int[] {1, 0, 0, 0});
        this.numColumns.Name = "numColumns";
        this.numColumns.Size = new System.Drawing.Size(91, 23);
        this.numColumns.TabIndex = 3;
        this.numColumns.Value = new decimal(new int[] {10, 0, 0, 0});
        // 
        // numRows
        // 
        this.numRows.Location = new System.Drawing.Point(379, 18);
        this.numRows.Maximum = new decimal(new int[] {200, 0, 0, 0});
        this.numRows.Minimum = new decimal(new int[] {1, 0, 0, 0});
        this.numRows.Name = "numRows";
        this.numRows.Size = new System.Drawing.Size(91, 23);
        this.numRows.TabIndex = 4;
        this.numRows.Value = new decimal(new int[] {10, 0, 0, 0});
        // 
        // btnApply
        // 
        this.btnApply.Location = new System.Drawing.Point(500, 17);
        this.btnApply.Name = "btnApply";
        this.btnApply.Size = new System.Drawing.Size(120, 25);
        this.btnApply.TabIndex = 5;
        this.btnApply.Text = "Appliquer";
        this.btnApply.UseVisualStyleBackColor = true;
        this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
        // 
        // btnRestart
        // 
        this.btnRestart.Location = new System.Drawing.Point(640, 17);
        this.btnRestart.Name = "btnRestart";
        this.btnRestart.Size = new System.Drawing.Size(120, 25);
        this.btnRestart.TabIndex = 6;
        this.btnRestart.Text = "Recommencer";
        this.btnRestart.UseVisualStyleBackColor = true;
        this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
        // 
        // lblSaveName
        // 
        this.lblSaveName.AutoSize = true;
        this.lblSaveName.Location = new System.Drawing.Point(12, 58);
        this.lblSaveName.Name = "lblSaveName";
        this.lblSaveName.Size = new System.Drawing.Size(87, 15);
        this.lblSaveName.TabIndex = 7;
        this.lblSaveName.Text = "Nom partie :";
        // 
        // txtSaveName
        // 
        this.txtSaveName.Location = new System.Drawing.Point(105, 55);
        this.txtSaveName.Name = "txtSaveName";
        this.txtSaveName.Size = new System.Drawing.Size(365, 23);
        this.txtSaveName.TabIndex = 8;
        this.txtSaveName.Text = "partie1";
        // 
        // btnSave
        // 
        this.btnSave.Location = new System.Drawing.Point(500, 54);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(120, 25);
        this.btnSave.TabIndex = 9;
        this.btnSave.Text = "Sauvegarder";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        // 
        // btnLoad
        // 
        this.btnLoad.Location = new System.Drawing.Point(640, 54);
        this.btnLoad.Name = "btnLoad";
        this.btnLoad.Size = new System.Drawing.Size(120, 25);
        this.btnLoad.TabIndex = 10;
        this.btnLoad.Text = "Charger";
        this.btnLoad.UseVisualStyleBackColor = true;
        this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
        // 
        // lblCurrentPlayer
        // 
        this.lblCurrentPlayer.AutoSize = true;
        this.lblCurrentPlayer.Location = new System.Drawing.Point(12, 98);
        this.lblCurrentPlayer.Name = "lblCurrentPlayer";
        this.lblCurrentPlayer.Size = new System.Drawing.Size(0, 15);
        this.lblCurrentPlayer.TabIndex = 11;
        this.lblCurrentPlayer.Text = "";
        // 
        // lblCurrentPlayerValue
        // 
        this.lblCurrentPlayerValue.AutoSize = true;
        this.lblCurrentPlayerValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblCurrentPlayerValue.Location = new System.Drawing.Point(12, 98);
        this.lblCurrentPlayerValue.Name = "lblCurrentPlayerValue";
        this.lblCurrentPlayerValue.Size = new System.Drawing.Size(20, 15);
        this.lblCurrentPlayerValue.TabIndex = 12;
        this.lblCurrentPlayerValue.Text = "R";
        // 
        // lblMissilesPrimary
        // 
        this.lblMissilesPrimary.AutoSize = true;
        this.lblMissilesPrimary.Location = new System.Drawing.Point(12, 121);
        this.lblMissilesPrimary.Name = "lblMissilesPrimary";
        this.lblMissilesPrimary.Size = new System.Drawing.Size(40, 15);
        this.lblMissilesPrimary.TabIndex = 13;
        this.lblMissilesPrimary.Text = "R. :";
        // 
        // lblMissilesPrimaryValue
        // 
        this.lblMissilesPrimaryValue.AutoSize = true;
        this.lblMissilesPrimaryValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblMissilesPrimaryValue.Location = new System.Drawing.Point(55, 121);
        this.lblMissilesPrimaryValue.Name = "lblMissilesPrimaryValue";
        this.lblMissilesPrimaryValue.Size = new System.Drawing.Size(14, 15);
        this.lblMissilesPrimaryValue.TabIndex = 14;
        this.lblMissilesPrimaryValue.Text = "3";
        // 
        // lblMissilesSecondary
        // 
        this.lblMissilesSecondary.AutoSize = true;
        this.lblMissilesSecondary.Location = new System.Drawing.Point(75, 121);
        this.lblMissilesSecondary.Name = "lblMissilesSecondary";
        this.lblMissilesSecondary.Size = new System.Drawing.Size(31, 15);
        this.lblMissilesSecondary.TabIndex = 15;
        this.lblMissilesSecondary.Text = "B. :";
        // 
        // lblMissilesSecondaryValue
        // 
        this.lblMissilesSecondaryValue.AutoSize = true;
        this.lblMissilesSecondaryValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblMissilesSecondaryValue.Location = new System.Drawing.Point(108, 121);
        this.lblMissilesSecondaryValue.Name = "lblMissilesSecondaryValue";
        this.lblMissilesSecondaryValue.Size = new System.Drawing.Size(14, 15);
        this.lblMissilesSecondaryValue.TabIndex = 16;
        this.lblMissilesSecondaryValue.Text = "3";
        // 
        // lblMissilesConfig
        // 
        this.lblMissilesConfig.AutoSize = true;
        this.lblMissilesConfig.Location = new System.Drawing.Point(620, 98);
        this.lblMissilesConfig.Name = "lblMissilesConfig";
        this.lblMissilesConfig.Size = new System.Drawing.Size(60, 15);
        this.lblMissilesConfig.TabIndex = 17;
        this.lblMissilesConfig.Text = "Stock:";
        // 
        // numMissilesPrimary
        // 
        this.numMissilesPrimary.Location = new System.Drawing.Point(780, 95);
        this.numMissilesPrimary.Maximum = new decimal(new int[] {99, 0, 0, 0});
        this.numMissilesPrimary.Name = "numMissilesPrimary";
        this.numMissilesPrimary.Size = new System.Drawing.Size(53, 23);
        this.numMissilesPrimary.TabIndex = 18;
        this.numMissilesPrimary.Value = new decimal(new int[] {3, 0, 0, 0});
        // 
        // lblMissilesConfigSeparator
        // 
        this.lblMissilesConfigSeparator.AutoSize = true;
        this.lblMissilesConfigSeparator.Location = new System.Drawing.Point(839, 98);
        this.lblMissilesConfigSeparator.Name = "lblMissilesConfigSeparator";
        this.lblMissilesConfigSeparator.Size = new System.Drawing.Size(12, 15);
        this.lblMissilesConfigSeparator.TabIndex = 19;
        this.lblMissilesConfigSeparator.Text = "/";
        // 
        // numMissilesSecondary
        // 
        this.numMissilesSecondary.Location = new System.Drawing.Point(857, 95);
        this.numMissilesSecondary.Maximum = new decimal(new int[] {99, 0, 0, 0});
        this.numMissilesSecondary.Name = "numMissilesSecondary";
        this.numMissilesSecondary.Size = new System.Drawing.Size(53, 23);
        this.numMissilesSecondary.TabIndex = 20;
        this.numMissilesSecondary.Value = new decimal(new int[] {3, 0, 0, 0});
        // 
        // lblMissilePower
        // 
        this.lblMissilePower.AutoSize = true;
        this.lblMissilePower.Location = new System.Drawing.Point(620, 130);
        this.lblMissilePower.Name = "lblMissilePower";
        this.lblMissilePower.Size = new System.Drawing.Size(20, 15);
        this.lblMissilePower.TabIndex = 21;
        this.lblMissilePower.Text = "P:";
        // 
        // numMissilePower
        // 
        this.numMissilePower.DecimalPlaces = 1;
        this.numMissilePower.Increment = new decimal(new int[] {1, 0, 0, 65536});
        this.numMissilePower.Location = new System.Drawing.Point(703, 128);
        this.numMissilePower.Maximum = new decimal(new int[] {9, 0, 0, 0});
        this.numMissilePower.Minimum = new decimal(new int[] {1, 0, 0, 0});
        this.numMissilePower.Name = "numMissilePower";
        this.numMissilePower.Size = new System.Drawing.Size(63, 23);
        this.numMissilePower.TabIndex = 22;
        this.numMissilePower.Value = new decimal(new int[] {5, 0, 0, 0});
        // 
        // lblMissileTargetColumn  — libellé mis à jour : "X (1‑9):"
        // 
        this.lblMissileTargetColumn.AutoSize = true;
        this.lblMissileTargetColumn.Location = new System.Drawing.Point(660, 130);
        this.lblMissileTargetColumn.Name = "lblMissileTargetColumn";
        this.lblMissileTargetColumn.Size = new System.Drawing.Size(50, 15);
        this.lblMissileTargetColumn.TabIndex = 23;
        this.lblMissileTargetColumn.Text = "X (1-9):";
        // 
        // numMissileTargetColumn  — decimal de 1,0 a 9,0 (pas 0,1)
        // 
        this.numMissileTargetColumn.DecimalPlaces = 1;
        this.numMissileTargetColumn.Increment = new decimal(new int[] {1, 0, 0, 65536});
        this.numMissileTargetColumn.Location = new System.Drawing.Point(715, 128);
        this.numMissileTargetColumn.Minimum = new decimal(new int[] {1, 0, 0, 0});
        this.numMissileTargetColumn.Maximum = new decimal(new int[] {9, 0, 0, 0});
        this.numMissileTargetColumn.Name = "numMissileTargetColumn";
        this.numMissileTargetColumn.Size = new System.Drawing.Size(60, 23);
        this.numMissileTargetColumn.TabIndex = 24;
        this.numMissileTargetColumn.Value = new decimal(new int[] {1, 0, 0, 0});
        // 
        // lblMissileTargetRow
        // 
        this.lblMissileTargetRow.AutoSize = true;
        this.lblMissileTargetRow.Location = new System.Drawing.Point(782, 130);
        this.lblMissileTargetRow.Name = "lblMissileTargetRow";
        this.lblMissileTargetRow.Size = new System.Drawing.Size(20, 15);
        this.lblMissileTargetRow.TabIndex = 25;
        this.lblMissileTargetRow.Text = "Y:";
        // 
        // numMissileTargetRow
        // 
        this.numMissileTargetRow.Location = new System.Drawing.Point(804, 128);
        this.numMissileTargetRow.Name = "numMissileTargetRow";
        this.numMissileTargetRow.Size = new System.Drawing.Size(45, 23);
        this.numMissileTargetRow.TabIndex = 26;
        // 
        // btnFireMissile
        // 
        this.btnFireMissile.Location = new System.Drawing.Point(858, 128);
        this.btnFireMissile.Name = "btnFireMissile";
        this.btnFireMissile.Size = new System.Drawing.Size(75, 23);
        this.btnFireMissile.TabIndex = 27;
        this.btnFireMissile.Text = "Tirer";
        this.btnFireMissile.UseVisualStyleBackColor = true;
        this.btnFireMissile.Click += new System.EventHandler(this.btnFireMissile_Click);
        // 
        // grpAction
        // 
        this.grpAction.Controls.Add(this.rdoActionMissile);
        this.grpAction.Controls.Add(this.rdoActionPlace);
        this.grpAction.Location = new System.Drawing.Point(320, 90);
        this.grpAction.Name = "grpAction";
        this.grpAction.Size = new System.Drawing.Size(274, 58);
        this.grpAction.TabIndex = 24;
        this.grpAction.TabStop = false;
        this.grpAction.Text = "Action du tour";
        // 
        // rdoActionMissile
        // 
        this.rdoActionMissile.AutoSize = true;
        this.rdoActionMissile.Location = new System.Drawing.Point(132, 24);
        this.rdoActionMissile.Name = "rdoActionMissile";
        this.rdoActionMissile.Size = new System.Drawing.Size(115, 19);
        this.rdoActionMissile.TabIndex = 1;
        this.rdoActionMissile.Text = "Lancer un missile";
        this.rdoActionMissile.UseVisualStyleBackColor = true;
        // 
        // rdoActionPlace
        // 
        this.rdoActionPlace.AutoSize = true;
        this.rdoActionPlace.Checked = true;
        this.rdoActionPlace.Location = new System.Drawing.Point(15, 24);
        this.rdoActionPlace.Name = "rdoActionPlace";
        this.rdoActionPlace.Size = new System.Drawing.Size(90, 19);
        this.rdoActionPlace.TabIndex = 0;
        this.rdoActionPlace.TabStop = true;
        this.rdoActionPlace.Text = "Poser un pion";
        this.rdoActionPlace.UseVisualStyleBackColor = true;
        // 
        // lblActionHint
        // 
        this.lblActionHint.AutoSize = true;
        this.lblActionHint.Location = new System.Drawing.Point(130, 121);
        this.lblActionHint.Name = "lblActionHint";
        this.lblActionHint.Size = new System.Drawing.Size(0, 0);
        this.lblActionHint.TabIndex = 26;
        this.lblActionHint.Text = "";
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(980, 450);
        this.Controls.Add(this.btnFireMissile);
        this.Controls.Add(this.numMissileTargetRow);
        this.Controls.Add(this.lblMissileTargetRow);
        this.Controls.Add(this.numMissileTargetColumn);
        this.Controls.Add(this.lblMissileTargetColumn);
        this.Controls.Add(this.numMissilePower);
        this.Controls.Add(this.lblMissilePower);
        this.Controls.Add(this.numMissilesSecondary);
        this.Controls.Add(this.lblMissilesConfigSeparator);
        this.Controls.Add(this.numMissilesPrimary);
        this.Controls.Add(this.lblMissilesConfig);
        this.Controls.Add(this.lblActionHint);
        this.Controls.Add(this.grpAction);
        this.Controls.Add(this.lblMissilesSecondaryValue);
        this.Controls.Add(this.lblMissilesSecondary);
        this.Controls.Add(this.lblMissilesPrimaryValue);
        this.Controls.Add(this.lblMissilesPrimary);
        this.Controls.Add(this.lblCurrentPlayerValue);
        this.Controls.Add(this.lblCurrentPlayer);
        this.Controls.Add(this.btnLoad);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.txtSaveName);
        this.Controls.Add(this.lblSaveName);
        this.Controls.Add(this.btnRestart);
        this.Controls.Add(this.btnApply);
        this.Controls.Add(this.numRows);
        this.Controls.Add(this.numColumns);
        this.Controls.Add(this.lblRows);
        this.Controls.Add(this.lblColumns);
        this.Controls.Add(this.pnlGrid);
        this.MinimumSize = new System.Drawing.Size(600, 350);
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Grille personnalisable";
        ((System.ComponentModel.ISupportInitialize)(this.numMissilePower)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissileTargetColumn)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissileTargetRow)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissilesPrimary)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numMissilesSecondary)).EndInit();
        this.grpAction.ResumeLayout(false);
        this.grpAction.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
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
    private System.Windows.Forms.Label lblCurrentPlayer;
    private System.Windows.Forms.Label lblCurrentPlayerValue;
    private System.Windows.Forms.Label lblMissilesPrimary;
    private System.Windows.Forms.Label lblMissilesPrimaryValue;
    private System.Windows.Forms.Label lblMissilesSecondary;
    private System.Windows.Forms.Label lblMissilesSecondaryValue;
    private System.Windows.Forms.Label lblMissilesConfig;
    private System.Windows.Forms.NumericUpDown numMissilesPrimary;
    private System.Windows.Forms.Label lblMissilesConfigSeparator;
    private System.Windows.Forms.NumericUpDown numMissilesSecondary;
    private System.Windows.Forms.Label lblMissilePower;
    private System.Windows.Forms.NumericUpDown numMissilePower;
    private System.Windows.Forms.Label lblMissileTargetColumn;
    private System.Windows.Forms.NumericUpDown numMissileTargetColumn;
    private System.Windows.Forms.Label lblMissileTargetRow;
    private System.Windows.Forms.NumericUpDown numMissileTargetRow;
    private System.Windows.Forms.Button btnFireMissile;
    private System.Windows.Forms.GroupBox grpAction;
    private System.Windows.Forms.RadioButton rdoActionMissile;
    private System.Windows.Forms.RadioButton rdoActionPlace;
    private System.Windows.Forms.Label lblActionHint;
}