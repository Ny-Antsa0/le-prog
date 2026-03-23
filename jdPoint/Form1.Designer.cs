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
        this.grpAction = new System.Windows.Forms.GroupBox();
        this.rbPlacePoint = new System.Windows.Forms.RadioButton();
        this.rbFireMissile = new System.Windows.Forms.RadioButton();
        this.grpMissile = new System.Windows.Forms.GroupBox();
        this.lblTargetRow = new System.Windows.Forms.Label();
        this.lblPower = new System.Windows.Forms.Label();
        this.numPower = new System.Windows.Forms.NumericUpDown();
        this.btnFireMissile = new System.Windows.Forms.Button();
        this.lblCurrentPlayer = new System.Windows.Forms.Label();
        this.grpAction.SuspendLayout();
        this.grpMissile.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPower)).BeginInit();
        this.SuspendLayout();
        // 
        // pnlGrid
        // 
        this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.pnlGrid.BackColor = System.Drawing.Color.White;
        this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlGrid.Location = new System.Drawing.Point(12, 88);
        this.pnlGrid.Name = "pnlGrid";
        this.pnlGrid.Size = new System.Drawing.Size(956, 350);
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
        this.numColumns.Maximum = new decimal(new int[] {
        200,
        0,
        0,
        0});
        this.numColumns.Minimum = new decimal(new int[] {
        2,
        0,
        0,
        0});
        this.numColumns.Name = "numColumns";
        this.numColumns.Size = new System.Drawing.Size(91, 23);
        this.numColumns.TabIndex = 3;
        this.numColumns.Value = new decimal(new int[] {
        10,
        0,
        0,
        0});
        // 
        // numRows
        // 
        this.numRows.Location = new System.Drawing.Point(379, 18);
        this.numRows.Maximum = new decimal(new int[] {
        200,
        0,
        0,
        0});
        this.numRows.Minimum = new decimal(new int[] {
        2,
        0,
        0,
        0});
        this.numRows.Name = "numRows";
        this.numRows.Size = new System.Drawing.Size(91, 23);
        this.numRows.TabIndex = 4;
        this.numRows.Value = new decimal(new int[] {
        10,
        0,
        0,
        0});
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
        this.lblCurrentPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblCurrentPlayer.ForeColor = System.Drawing.Color.Firebrick;
        this.lblCurrentPlayer.Location = new System.Drawing.Point(12, 54);
        this.lblCurrentPlayer.Name = "lblCurrentPlayer";
        this.lblCurrentPlayer.Size = new System.Drawing.Size(142, 18);
        this.lblCurrentPlayer.TabIndex = 11;
        this.lblCurrentPlayer.Text = "Joueur Rouge (Poser)";
        // 
        // grpAction
        // 
        this.grpAction.Controls.Add(this.rbPlacePoint);
        this.grpAction.Controls.Add(this.rbFireMissile);
        this.grpAction.Location = new System.Drawing.Point(980, 12);
        this.grpAction.Name = "grpAction";
        this.grpAction.Size = new System.Drawing.Size(180, 80);
        this.grpAction.TabIndex = 12;
        this.grpAction.TabStop = false;
        this.grpAction.Text = "Action du tour";
        // 
        // rbPlacePoint
        // 
        this.rbPlacePoint.AutoSize = true;
        this.rbPlacePoint.Checked = true;
        this.rbPlacePoint.Location = new System.Drawing.Point(10, 25);
        this.rbPlacePoint.Name = "rbPlacePoint";
        this.rbPlacePoint.Size = new System.Drawing.Size(104, 19);
        this.rbPlacePoint.TabIndex = 0;
        this.rbPlacePoint.TabStop = true;
        this.rbPlacePoint.Text = "Poser un point";
        this.rbPlacePoint.UseVisualStyleBackColor = true;
        // 
        // rbFireMissile
        // 
        this.rbFireMissile.AutoSize = true;
        this.rbFireMissile.Location = new System.Drawing.Point(10, 50);
        this.rbFireMissile.Name = "rbFireMissile";
        this.rbFireMissile.Size = new System.Drawing.Size(118, 19);
        this.rbFireMissile.TabIndex = 1;
        this.rbFireMissile.Text = "Tirer un missile";
        this.rbFireMissile.UseVisualStyleBackColor = true;
        // 
        // grpMissile
        // 
        this.grpMissile.Controls.Add(this.lblTargetRow);
        this.grpMissile.Controls.Add(this.lblPower);
        this.grpMissile.Controls.Add(this.numPower);
        this.grpMissile.Controls.Add(this.btnFireMissile);
        this.grpMissile.Enabled = false;
        this.grpMissile.Location = new System.Drawing.Point(980, 100);
        this.grpMissile.Name = "grpMissile";
        this.grpMissile.Size = new System.Drawing.Size(180, 120);
        this.grpMissile.TabIndex = 13;
        this.grpMissile.TabStop = false;
        this.grpMissile.Text = "Lancement de missile";
        // 
        // lblTargetRow
        // 
        this.lblTargetRow.AutoSize = true;
        this.lblTargetRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.lblTargetRow.Location = new System.Drawing.Point(10, 25);
        this.lblTargetRow.Name = "lblTargetRow";
        this.lblTargetRow.Size = new System.Drawing.Size(150, 30);
        this.lblTargetRow.TabIndex = 0;
        this.lblTargetRow.Text = "Ligne cible: Calculée";
        // 
        // lblPower
        // 
        this.lblPower.AutoSize = true;
        this.lblPower.Location = new System.Drawing.Point(10, 60);
        this.lblPower.Name = "lblPower";
        this.lblPower.Size = new System.Drawing.Size(53, 15);
        this.lblPower.TabIndex = 1;
        this.lblPower.Text = "Puissance:";
        // 
        // numPower
        // 
        this.numPower.Location = new System.Drawing.Point(110, 58);
        this.numPower.Maximum = new decimal(new int[] {
        9,
        0,
        0,
        0});
        this.numPower.Minimum = new decimal(new int[] {
        1,
        0,
        0,
        0});
        this.numPower.Name = "numPower";
        this.numPower.Size = new System.Drawing.Size(60, 23);
        this.numPower.TabIndex = 2;
        this.numPower.Value = new decimal(new int[] {
        5,
        0,
        0,
        0});
        // 
        // btnFireMissile
        // 
        this.btnFireMissile.Location = new System.Drawing.Point(45, 90);
        this.btnFireMissile.Name = "btnFireMissile";
        this.btnFireMissile.Size = new System.Drawing.Size(90, 25);
        this.btnFireMissile.TabIndex = 3;
        this.btnFireMissile.Text = "Tirer";
        this.btnFireMissile.UseVisualStyleBackColor = true;
        this.btnFireMissile.Click += new System.EventHandler(this.btnFireMissile_Click);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1180, 450);
        this.Controls.Add(this.grpMissile);
        this.Controls.Add(this.grpAction);
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
        this.Text = "Jeu de points avec missiles";
        ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPower)).EndInit();
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
