namespace jdPoint;

using System.Text.Json;
using Npgsql;

public partial class Form1 : Form
{
    private const string DefaultConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=point";
    private const string UiConfigFileName = "ui.config.json";
    private int _columns = 10;
    private int _rows = 10;
    private readonly Dictionary<Point, bool> _points = new();
    private bool _usePrimaryColorNext = true;
    private bool _isGameOver;
    private bool _isApplyingLoadedState;
    private int _missilesPrimaryRemaining;
    private int _missilesSecondaryRemaining;
    private UiConfig _uiConfig = UiConfig.CreateDefault();

    private Color _gridBackgroundColor = Color.White;
    private Color _gridLineColor = Color.DimGray;
    private Color _primaryPointColor = Color.Firebrick;
    private Color _secondaryPointColor = Color.RoyalBlue;
    private Color _missilePreviewColor = Color.OrangeRed;
    private Color _missileCrosshairColor = Color.Red;

    public Form1()
    {
        InitializeComponent();
        LoadAndApplyUiConfig();

        Resize += Form1_Resize;
        numColumns.ValueChanged += GridValueChanged;
        numRows.ValueChanged += GridValueChanged;
        pnlGrid.Resize += GridPanelResize;
        rdoActionPlace.CheckedChanged += ActionModeChanged;
        rdoActionMissile.CheckedChanged += ActionModeChanged;
        numMissilesPrimary.ValueChanged += MissileStockConfigChanged;
        numMissilesSecondary.ValueChanged += MissileStockConfigChanged;

        // Redraw preview when target changes
        numMissileTargetColumn.ValueChanged += MissileTargetChanged;
        numMissileTargetRow.ValueChanged += MissileTargetChanged;

        Shown += Form1_Shown;

        lblMissilePower.Visible = false;
        numMissilePower.Visible = false;

        RestartGame();
    }

    private void Form1_Resize(object? sender, EventArgs e)
    {
        ApplyRuntimeLayout();
    }

    private void LoadAndApplyUiConfig()
    {
        _uiConfig = LoadUiConfig();

        BackColor = ParseColor(_uiConfig.Colors.FormBackground, SystemColors.Control);
        pnlGrid.BackColor = ParseColor(_uiConfig.Colors.GridBackground, Color.White);

        _gridBackgroundColor = ParseColor(_uiConfig.Colors.GridBackground, Color.White);
        _gridLineColor = ParseColor(_uiConfig.Colors.GridLine, Color.DimGray);
        _primaryPointColor = ParseColor(_uiConfig.Colors.PrimaryPoint, Color.Firebrick);
        _secondaryPointColor = ParseColor(_uiConfig.Colors.SecondaryPoint, Color.RoyalBlue);
        _missilePreviewColor = ParseColor(_uiConfig.Colors.MissilePreview, Color.OrangeRed);
        _missileCrosshairColor = ParseColor(_uiConfig.Colors.MissileCrosshair, Color.Red);

        if (_uiConfig.Window.Width > 0 && _uiConfig.Window.Height > 0)
        {
            ClientSize = new Size(_uiConfig.Window.Width, _uiConfig.Window.Height);
        }

        if (_uiConfig.Window.MinimumWidth > 0 && _uiConfig.Window.MinimumHeight > 0)
        {
            MinimumSize = new Size(_uiConfig.Window.MinimumWidth, _uiConfig.Window.MinimumHeight);
        }

        if (!string.IsNullOrWhiteSpace(_uiConfig.Window.Title))
        {
            Text = _uiConfig.Window.Title;
        }

        ApplyRuntimeLayout();
    }

    private void ApplyRuntimeLayout()
    {
        string mode = _uiConfig.Layout.NavigationModel?.Trim().ToLowerInvariant() ?? "topbar";
        if (mode == "sidebar")
        {
            ApplySidebarLayout();
            return;
        }

        ApplyTopBarLayout();
    }

    private void ApplyTopBarLayout()
    {
        int margin = Math.Max(6, _uiConfig.Layout.Margin);
        int topBarHeight = Math.Max(130, _uiConfig.Layout.TopBarHeight);

        pnlGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        int gridLeft = margin;
        int gridTop = topBarHeight + margin;
        int gridWidth = Math.Max(120, ClientSize.Width - (margin * 2));
        int gridHeight = Math.Max(120, ClientSize.Height - gridTop - margin);

        pnlGrid.Location = new Point(gridLeft, gridTop);
        pnlGrid.Size = new Size(gridWidth, gridHeight);
    }

    private void ApplySidebarLayout()
    {
        int margin = Math.Max(6, _uiConfig.Layout.Margin);
        int sidebarWidth = Math.Max(270, _uiConfig.Layout.SidebarWidth);

        int x = margin;
        int y = margin;
        int lineHeight = 26;
        int labelWidth = 120;
        int inputWidth = 92;
        int buttonWidth = 120;
        int rowGap = 8;

        lblColumns.Location = new Point(x, y + 5);
        lblColumns.Size = new Size(labelWidth, 15);
        numColumns.Location = new Point(x + labelWidth, y);
        numColumns.Size = new Size(inputWidth, lineHeight);
        y += lineHeight + rowGap;

        lblRows.Location = new Point(x, y + 5);
        lblRows.Size = new Size(labelWidth, 15);
        numRows.Location = new Point(x + labelWidth, y);
        numRows.Size = new Size(inputWidth, lineHeight);
        y += lineHeight + rowGap;

        btnApply.Location = new Point(x, y);
        btnApply.Size = new Size(buttonWidth, lineHeight);
        btnRestart.Location = new Point(x + buttonWidth + 10, y);
        btnRestart.Size = new Size(buttonWidth, lineHeight);
        y += lineHeight + rowGap + 2;

        lblSaveName.Location = new Point(x, y + 5);
        lblSaveName.Size = new Size(labelWidth, 15);
        y += lineHeight;
        txtSaveName.Location = new Point(x, y);
        txtSaveName.Size = new Size(sidebarWidth - (margin * 2), lineHeight);
        y += lineHeight + rowGap;

        btnSave.Location = new Point(x, y);
        btnSave.Size = new Size(buttonWidth, lineHeight);
        btnLoad.Location = new Point(x + buttonWidth + 10, y);
        btnLoad.Size = new Size(buttonWidth, lineHeight);
        y += lineHeight + rowGap + 2;

        lblCurrentPlayerValue.Location = new Point(x, y);
        lblMissilesPrimary.Location = new Point(x + 45, y + 1);
        lblMissilesPrimaryValue.Location = new Point(x + 83, y + 1);
        lblMissilesSecondary.Location = new Point(x + 115, y + 1);
        lblMissilesSecondaryValue.Location = new Point(x + 145, y + 1);
        y += lineHeight + rowGap;

        grpAction.Location = new Point(x, y);
        grpAction.Size = new Size(sidebarWidth - (margin * 2), 58);
        y += 58 + rowGap;

        lblMissilesConfig.Location = new Point(x, y + 5);
        lblMissilesConfig.Size = new Size(50, 15);
        numMissilesPrimary.Location = new Point(x + 55, y);
        numMissilesPrimary.Size = new Size(52, lineHeight);
        lblMissilesConfigSeparator.Location = new Point(x + 112, y + 5);
        numMissilesSecondary.Location = new Point(x + 127, y);
        numMissilesSecondary.Size = new Size(52, lineHeight);
        y += lineHeight + rowGap;

        lblMissileTargetColumn.Location = new Point(x, y + 5);
        numMissileTargetColumn.Location = new Point(x + 55, y);
        numMissileTargetColumn.Size = new Size(58, lineHeight);

        lblMissileTargetRow.Location = new Point(x + 120, y + 5);
        numMissileTargetRow.Location = new Point(x + 140, y);
        numMissileTargetRow.Size = new Size(48, lineHeight);
        y += lineHeight + rowGap;

        btnFireMissile.Location = new Point(x, y);
        btnFireMissile.Size = new Size(buttonWidth, lineHeight);
        lblActionHint.Location = new Point(x, y + lineHeight + 4);
        lblActionHint.Size = new Size(sidebarWidth - (margin * 2), 32);

        lblMissilePower.Visible = false;
        numMissilePower.Visible = false;
        lblCurrentPlayer.Visible = false;

        pnlGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        int gridLeft = sidebarWidth + margin;
        int gridTop = margin;
        int gridWidth = Math.Max(120, ClientSize.Width - gridLeft - margin);
        int gridHeight = Math.Max(120, ClientSize.Height - (margin * 2));
        pnlGrid.Location = new Point(gridLeft, gridTop);
        pnlGrid.Size = new Size(gridWidth, gridHeight);
    }

    private UiConfig LoadUiConfig()
    {
        UiConfig fallback = UiConfig.CreateDefault();

        string[] candidates =
        {
            Path.Combine(AppContext.BaseDirectory, UiConfigFileName),
            Path.Combine(Application.StartupPath, UiConfigFileName),
            Path.Combine(Environment.CurrentDirectory, UiConfigFileName)
        };

        string? configPath = candidates.FirstOrDefault(File.Exists);
        if (configPath is null)
        {
            return fallback;
        }

        try
        {
            string json = File.ReadAllText(configPath);
            UiConfig? parsed = JsonSerializer.Deserialize<UiConfig>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return parsed ?? fallback;
        }
        catch
        {
            return fallback;
        }
    }

    private static Color ParseColor(string? value, Color fallback)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return fallback;
        }

        try
        {
            return ColorTranslator.FromHtml(value);
        }
        catch
        {
            return fallback;
        }
    }

    // -----------------------------------------------------------------------
    // Calcule la colonne réelle à partir de X saisi entre 1 et 9.
    // 1 = première colonne (gauche), 9 = dernière colonne (droite),
    // et les valeurs du milieu sont réparties linéairement.
    // -----------------------------------------------------------------------
    private int ComputeTargetColumn()
    {
        if (_columns <= 1)
        {
            return 0;
        }

        double xInput = (double)numMissileTargetColumn.Value; // 1..9
        double normalized = (xInput - 1.0) / 8.0; // 0..1
        int col = (int)Math.Round(normalized * (_columns - 1), MidpointRounding.AwayFromZero);
        return Math.Clamp(col, 0, _columns - 1);
    }

    private void MissileTargetChanged(object? sender, EventArgs e)
    {
        pnlGrid.Invalidate();
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        try
        {
            EnsureDatabaseObjects();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Connexion PostgreSQL impossible. Verifie la chaine de connexion.\n\n{ex.Message}", "Base de donnees", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
        UpdateGridDimensions();
    }

    private void GridValueChanged(object? sender, EventArgs e)
    {
        if (_isApplyingLoadedState)
        {
            return;
        }

        UpdateGridDimensions();
    }

    private void GridPanelResize(object? sender, EventArgs e)
    {
        pnlGrid.Invalidate();
    }

    private void ActionModeChanged(object? sender, EventArgs e)
    {
        UpdateStatusIndicators();
        pnlGrid.Invalidate();
    }

    private void MissileStockConfigChanged(object? sender, EventArgs e)
    {
        if (_points.Count > 0 && !_isGameOver)
        {
            return;
        }

        InitializeMissileStocksFromConfig();
        UpdateStatusIndicators();
    }

    private void UpdateGridDimensions()
    {
        _columns = (int)numColumns.Value;
        _rows = (int)numRows.Value;
        RestartGame();
    }

    private void btnRestart_Click(object sender, EventArgs e)
    {
        RestartGame();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        string saveName = txtSaveName.Text.Trim();
        if (string.IsNullOrWhiteSpace(saveName))
        {
            MessageBox.Show("Donne un nom de sauvegarde.", "Sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            EnsureDatabaseObjects();

            List<PointState> serializedPoints = _points
                .Select(entry => new PointState(entry.Key.X, entry.Key.Y, entry.Value))
                .ToList();

            string pointsJson = JsonSerializer.Serialize(serializedPoints);

            using var connection = new NpgsqlConnection(GetConnectionString());
            connection.Open();

            using var command = new NpgsqlCommand(@"
INSERT INTO game_saves(save_name, columns_count, rows_count, points_json, use_primary_color_next, is_game_over, updated_at)
VALUES (@save_name, @columns_count, @rows_count, @points_json::jsonb, @use_primary_color_next, @is_game_over, NOW())
ON CONFLICT (save_name) DO UPDATE
SET columns_count = EXCLUDED.columns_count,
    rows_count = EXCLUDED.rows_count,
    points_json = EXCLUDED.points_json,
    use_primary_color_next = EXCLUDED.use_primary_color_next,
    is_game_over = EXCLUDED.is_game_over,
    updated_at = NOW();", connection);

            command.Parameters.AddWithValue("save_name", saveName);
            command.Parameters.AddWithValue("columns_count", _columns);
            command.Parameters.AddWithValue("rows_count", _rows);
            command.Parameters.AddWithValue("points_json", pointsJson);
            command.Parameters.AddWithValue("use_primary_color_next", _usePrimaryColorNext);
            command.Parameters.AddWithValue("is_game_over", _isGameOver);

            command.ExecuteNonQuery();
            MessageBox.Show("Partie sauvegardee.", "Sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Echec de la sauvegarde.\n\n{ex.Message}", "Sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        string saveName = txtSaveName.Text.Trim();
        if (string.IsNullOrWhiteSpace(saveName))
        {
            MessageBox.Show("Donne un nom de sauvegarde.", "Chargement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            EnsureDatabaseObjects();

            using var connection = new NpgsqlConnection(GetConnectionString());
            connection.Open();

            using var command = new NpgsqlCommand(@"
SELECT columns_count, rows_count, points_json::text, use_primary_color_next, is_game_over
FROM game_saves
WHERE save_name = @save_name;", connection);
            command.Parameters.AddWithValue("save_name", saveName);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("Aucune sauvegarde trouvee avec ce nom.", "Chargement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int loadedColumns = reader.GetInt32(0);
            int loadedRows = reader.GetInt32(1);
            string pointsJson = reader.GetString(2);
            bool loadedUsePrimaryColorNext = reader.GetBoolean(3);
            bool loadedIsGameOver = reader.GetBoolean(4);

            List<PointState>? loadedPoints = JsonSerializer.Deserialize<List<PointState>>(pointsJson);
            if (loadedPoints is null)
            {
                loadedPoints = new List<PointState>();
            }

            _isApplyingLoadedState = true;
            try
            {
                numColumns.Value = Math.Clamp(loadedColumns, (int)numColumns.Minimum, (int)numColumns.Maximum);
                numRows.Value = Math.Clamp(loadedRows, (int)numRows.Minimum, (int)numRows.Maximum);
            }
            finally
            {
                _isApplyingLoadedState = false;
            }

            _columns = (int)numColumns.Value;
            _rows = (int)numRows.Value;
            _points.Clear();

            foreach (PointState point in loadedPoints)
            {
                if (point.X < 0 || point.X >= _columns || point.Y < 0 || point.Y >= _rows)
                {
                    continue;
                }

                _points[new Point(point.X, point.Y)] = point.IsPrimary;
            }

            _usePrimaryColorNext = loadedUsePrimaryColorNext;
            _isGameOver = loadedIsGameOver;

            InitializeMissileStocksFromConfig();
            SyncMissileTargetBounds();
            UpdateStatusIndicators();

            pnlGrid.Invalidate();
            MessageBox.Show("Partie chargee.", "Chargement", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Echec du chargement.\n\n{ex.Message}", "Chargement", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnFireMissile_Click(object? sender, EventArgs e)
    {
        if (_isGameOver)
        {
            MessageBox.Show("La partie est terminee.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!rdoActionMissile.Checked)
        {
            MessageBox.Show("Selectionne d'abord l'action 'Lancer un missile'.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int availableMissiles = _usePrimaryColorNext ? _missilesPrimaryRemaining : _missilesSecondaryRemaining;
        if (availableMissiles <= 0)
        {
            string colorName = _usePrimaryColorNext ? "Rouge" : "Bleu";
            MessageBox.Show($"{colorName} n'a plus de missiles.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Calcul de la colonne réelle depuis la valeur décimale 1-9
        int targetColumn = ComputeTargetColumn();
        int targetRowFromBottom = (int)numMissileTargetRow.Value;
        int targetRow = (_rows - 1) - targetRowFromBottom;

        if (targetColumn < 0 || targetColumn >= _columns || targetRow < 0 || targetRow >= _rows)
        {
            MessageBox.Show("La case cible est hors de la grille.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        bool removed = _points.Remove(new Point(targetColumn, targetRow));

        if (_usePrimaryColorNext)
        {
            _missilesPrimaryRemaining--;
        }
        else
        {
            _missilesSecondaryRemaining--;
        }

        string shooter = _usePrimaryColorNext ? "Rouge" : "Bleu";
        _usePrimaryColorNext = !_usePrimaryColorNext;

        UpdateStatusIndicators();
        pnlGrid.Invalidate();

        MessageBox.Show(
            $"{shooter} tire en X={numMissileTargetColumn.Value:0.0} → colonne {targetColumn}, Y={targetRowFromBottom}. Pions retires: {(removed ? 1 : 0)}.",
            "Missile",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void RestartGame()
    {
        _points.Clear();
        _usePrimaryColorNext = true;
        _isGameOver = false;
        InitializeMissileStocksFromConfig();
        SyncMissileTargetBounds();
        UpdateStatusIndicators();
        pnlGrid.Invalidate();
    }

    private void pnlGrid_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || _columns <= 0 || _rows <= 0 || _isGameOver)
        {
            return;
        }

        if (!rdoActionPlace.Checked)
        {
            return;
        }

        float usableWidth = pnlGrid.ClientSize.Width - 1;
        float usableHeight = pnlGrid.ClientSize.Height - 1;

        if (usableWidth <= 0 || usableHeight <= 0)
        {
            return;
        }

        float cellWidth = usableWidth / _columns;
        float cellHeight = usableHeight / _rows;

        int cellX = Math.Min((int)(e.X / cellWidth), _columns - 1);
        int cellY = Math.Min((int)(e.Y / cellHeight), _rows - 1);

        if (cellX < 0 || cellY < 0)
        {
            return;
        }

        var clickedCell = new Point(cellX, cellY);
        if (_points.ContainsKey(clickedCell))
        {
            return;
        }

        bool currentPlayerIsPrimary = _usePrimaryColorNext;
        _points[clickedCell] = currentPlayerIsPrimary;

        if (HasFiveInRow(clickedCell, currentPlayerIsPrimary))
        {
            _isGameOver = true;
            string winnerName = currentPlayerIsPrimary ? "Rouge" : "Bleu";
            MessageBox.Show($"{winnerName} gagne avec 5 points alignes !", "Partie terminee", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        _usePrimaryColorNext = !_usePrimaryColorNext;

        UpdateStatusIndicators();
        pnlGrid.Invalidate();
    }

    private void InitializeMissileStocksFromConfig()
    {
        _missilesPrimaryRemaining = (int)numMissilesPrimary.Value;
        _missilesSecondaryRemaining = (int)numMissilesSecondary.Value;
    }

    private void SyncMissileTargetBounds()
    {
        // X : repère utilisateur fixe de 1 à 9
        numMissileTargetColumn.Minimum = 1;
        numMissileTargetColumn.Maximum = 9;
        if (numMissileTargetColumn.Value < numMissileTargetColumn.Minimum)
            numMissileTargetColumn.Value = numMissileTargetColumn.Minimum;
        if (numMissileTargetColumn.Value > numMissileTargetColumn.Maximum)
            numMissileTargetColumn.Value = numMissileTargetColumn.Maximum;

        // Y : ligne depuis le bas, 0 à rows-1
        decimal maxRow = Math.Max(0, _rows - 1);
        numMissileTargetRow.Minimum = 0;
        numMissileTargetRow.Maximum = maxRow;
        if (numMissileTargetRow.Value < numMissileTargetRow.Minimum || numMissileTargetRow.Value > numMissileTargetRow.Maximum)
        {
            numMissileTargetRow.Value = numMissileTargetRow.Minimum;
        }
    }

    private void UpdateStatusIndicators()
    {
        lblCurrentPlayerValue.Text = _usePrimaryColorNext ? "R" : "B";
        lblMissilesPrimaryValue.Text = _missilesPrimaryRemaining.ToString();
        lblMissilesSecondaryValue.Text = _missilesSecondaryRemaining.ToString();

        bool missileMode = rdoActionMissile.Checked && !_isGameOver;
        numMissileTargetColumn.Enabled = missileMode;
        numMissileTargetRow.Enabled = missileMode;
        btnFireMissile.Enabled = missileMode;

        if (_isGameOver)
        {
            lblActionHint.Text = "Partie terminee. Clique sur Recommencer pour rejouer.";
            return;
        }

        if (rdoActionMissile.Checked)
        {
            int previewCol = ComputeTargetColumn();
            // lblActionHint.Text = $"Choisis X (1,0-9,0) et Y puis Tirer.  → colonne {previewCol}";
            return;
        }

        lblActionHint.Text = "Clique sur la grille pour poser un pion.";
    }

    private bool HasFiveInRow(Point origin, bool playerColor)
    {
        return CountAligned(origin, 1, 0, playerColor) >= 5
            || CountAligned(origin, 0, 1, playerColor) >= 5
            || CountAligned(origin, 1, 1, playerColor) >= 5
            || CountAligned(origin, 1, -1, playerColor) >= 5;
    }

    private int CountAligned(Point origin, int dx, int dy, bool playerColor)
    {
        int count = 1;
        count += CountDirection(origin, dx, dy, playerColor);
        count += CountDirection(origin, -dx, -dy, playerColor);
        return count;
    }

    private int CountDirection(Point origin, int dx, int dy, bool playerColor)
    {
        int count = 0;
        int x = origin.X + dx;
        int y = origin.Y + dy;

        while (x >= 0 && x < _columns && y >= 0 && y < _rows)
        {
            var current = new Point(x, y);
            if (!_points.TryGetValue(current, out bool currentColor) || currentColor != playerColor)
            {
                break;
            }

            count++;
            x += dx;
            y += dy;
        }

        return count;
    }

    private void pnlGrid_Paint(object sender, PaintEventArgs e)
    {
        if (_columns <= 0 || _rows <= 0)
        {
            return;
        }

        var graphics = e.Graphics;
        graphics.Clear(_gridBackgroundColor);

        float usableWidth = pnlGrid.ClientSize.Width - 1;
        float usableHeight = pnlGrid.ClientSize.Height - 1;
        float cellWidth = usableWidth / _columns;
        float cellHeight = usableHeight / _rows;

        // --- Prévisualisation du missile (fond rouge translucide) ---
        if (rdoActionMissile.Checked && !_isGameOver)
        {
            int previewCol = ComputeTargetColumn();
            int previewRowBottom = (int)numMissileTargetRow.Value;
            int previewRow = (_rows - 1) - previewRowBottom;

            if (previewCol >= 0 && previewCol < _columns && previewRow >= 0 && previewRow < _rows)
            {
                float px = previewCol * cellWidth;
                float py = previewRow * cellHeight;

                using var previewFill = new SolidBrush(Color.FromArgb(80, _missilePreviewColor));
                using var previewBorder = new Pen(_missilePreviewColor, 2f);

                graphics.FillRectangle(previewFill, px, py, cellWidth, cellHeight);
                graphics.DrawRectangle(previewBorder, px, py, cellWidth, cellHeight);

                // Croix de visée
                float cx = px + cellWidth / 2f;
                float cy = py + cellHeight / 2f;
                float arm = MathF.Min(cellWidth, cellHeight) * 0.3f;

                using var crossPen = new Pen(_missileCrosshairColor, 2f);
                graphics.DrawLine(crossPen, cx - arm, cy, cx + arm, cy);
                graphics.DrawLine(crossPen, cx, cy - arm, cx, cy + arm);
            }
        }

        // --- Grille ---
        using var pen = new Pen(_gridLineColor, 1f);

        for (int x = 0; x <= _columns; x++)
        {
            float xPos = x * cellWidth;
            graphics.DrawLine(pen, xPos, 0, xPos, usableHeight);
        }

        for (int y = 0; y <= _rows; y++)
        {
            float yPos = y * cellHeight;
            graphics.DrawLine(pen, 0, yPos, usableWidth, yPos);
        }

        // --- Pions ---
        float pointDiameter = MathF.Max(4f, MathF.Min(cellWidth, cellHeight) * 0.45f);
        float pointRadius = pointDiameter / 2f;

        using var primaryBrush = new SolidBrush(_primaryPointColor);
        using var secondaryBrush = new SolidBrush(_secondaryPointColor);

        foreach (KeyValuePair<Point, bool> entry in _points)
        {
            Point cell = entry.Key;
            float centerX = (cell.X + 0.5f) * cellWidth;
            float centerY = (cell.Y + 0.5f) * cellHeight;
            Brush brush = entry.Value ? primaryBrush : secondaryBrush;
            graphics.FillEllipse(brush, centerX - pointRadius, centerY - pointRadius, pointDiameter, pointDiameter);
        }
    }

    private string GetConnectionString()
    {
        return Environment.GetEnvironmentVariable("JDPOINT_CONNECTION_STRING") ?? DefaultConnectionString;
    }

    private void EnsureDatabaseObjects()
    {
        using var connection = new NpgsqlConnection(GetConnectionString());
        connection.Open();

        using var command = new NpgsqlCommand(@"
CREATE TABLE IF NOT EXISTS game_saves
(
    save_name TEXT PRIMARY KEY,
    columns_count INTEGER NOT NULL,
    rows_count INTEGER NOT NULL,
    points_json JSONB NOT NULL,
    use_primary_color_next BOOLEAN NOT NULL,
    is_game_over BOOLEAN NOT NULL,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);", connection);

        command.ExecuteNonQuery();
    }

    private sealed class UiConfig
    {
        public UiWindowConfig Window { get; init; } = new();
        public UiLayoutConfig Layout { get; init; } = new();
        public UiColorsConfig Colors { get; init; } = new();

        public static UiConfig CreateDefault()
        {
            return new UiConfig
            {
                Window = new UiWindowConfig
                {
                    Title = "Grille personnalisable",
                    Width = 980,
                    Height = 450,
                    MinimumWidth = 600,
                    MinimumHeight = 350
                },
                Layout = new UiLayoutConfig
                {
                    NavigationModel = "TopBar",
                    Margin = 12,
                    TopBarHeight = 150,
                    SidebarWidth = 310
                },
                Colors = new UiColorsConfig
                {
                    FormBackground = "#F3F5F7",
                    GridBackground = "#FFFFFF",
                    GridLine = "#696969",
                    PrimaryPoint = "#B22222",
                    SecondaryPoint = "#4169E1",
                    MissilePreview = "#FF4500",
                    MissileCrosshair = "#C00000"
                }
            };
        }
    }

    private sealed class UiWindowConfig
    {
        public string Title { get; init; } = "Grille personnalisable";
        public int Width { get; init; } = 980;
        public int Height { get; init; } = 450;
        public int MinimumWidth { get; init; } = 600;
        public int MinimumHeight { get; init; } = 350;
    }

    private sealed class UiLayoutConfig
    {
        public string NavigationModel { get; init; } = "TopBar";
        public int Margin { get; init; } = 12;
        public int TopBarHeight { get; init; } = 150;
        public int SidebarWidth { get; init; } = 310;
    }

    private sealed class UiColorsConfig
    {
        public string FormBackground { get; init; } = "#F3F5F7";
        public string GridBackground { get; init; } = "#FFFFFF";
        public string GridLine { get; init; } = "#696969";
        public string PrimaryPoint { get; init; } = "#B22222";
        public string SecondaryPoint { get; init; } = "#4169E1";
        public string MissilePreview { get; init; } = "#FF4500";
        public string MissileCrosshair { get; init; } = "#C00000";
    }

    private sealed record PointState(int X, int Y, bool IsPrimary);
}