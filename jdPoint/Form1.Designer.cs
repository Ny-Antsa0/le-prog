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
        this.pnlGrid.Location = new System.Drawing.Point(12, 56);
        this.pnlGrid.Name = "pnlGrid";
        this.pnlGrid.Size = new System.Drawing.Size(776, 382);
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
        1,
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
        1,
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
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
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
}
