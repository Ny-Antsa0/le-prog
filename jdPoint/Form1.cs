namespace jdPoint;

using System.Text.Json;
using Npgsql;

public partial class Form1 : Form
{
    private const string DefaultConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=8889;Database=point";
    private int _columns = 10;
    private int _rows = 10;
    private readonly Dictionary<Point, bool> _points = new();
    private bool _usePrimaryColorNext = true;
    private bool _isGameOver;
    private bool _isApplyingLoadedState;
    private int _missilesPrimaryRemaining;
    private int _missilesSecondaryRemaining;

    public Form1()
    {
        InitializeComponent();
        KeyPreview = true;
        KeyDown += Form1_KeyDown;
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

    // -----------------------------------------------------------------------
    // Calcule la colonne réelle à partir de X saisi entre 1 et 9.
    // 1 = première colonne (gauche), 9 = dernière colonne (droite),
    // et les valeurs du milieu sont réparties linéairement.
    // -----------------------------------------------------------------------
    private int ComputeTargetColumn()
    {
        // On vise uniquement les intersections internes (bords exclus).
        if (_columns <= 2)
        {
            return -1;
        }

        double xInput = (double)numMissileTargetColumn.Value; // 1..9
        double normalized = (xInput - 1.0) / 8.0; // 0..1
        double scaled = 1.0 + normalized * (_columns - 2); // 1..(_columns-1)

        // Apres calcul, on retire la partie decimale pour obtenir l'entier de placement.
        int col = (int)scaled;
        return Math.Clamp(col, 1, _columns - 1);
    }

    private int ComputeTargetRow()
    {
        // On vise uniquement les intersections internes (bords exclus).
        if (_rows <= 2)
        {
            return -1;
        }

        double yInput = (double)numMissileTargetRow.Value; // 1..9
        double normalized = (yInput - 1.0) / 8.0; // 0..1
        double scaled = 1.0 + normalized * (_rows - 2); // 1..(_rows-1)

        int row = (int)scaled;
        return Math.Clamp(row, 1, _rows - 1);
    }

    private void MissileTargetChanged(object? sender, EventArgs e)
    {
        pnlGrid.Invalidate();
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!rdoActionMissile.Checked || _isGameOver)
        {
            return;
        }

        // Ne pas intercepter la saisie libre dans le champ de nom de sauvegarde.
        if (ActiveControl is TextBoxBase)
        {
            return;
        }

        decimal? mappedValue = e.KeyCode switch
        {
            Keys.A => 1m,
            Keys.Z => 2m,
            Keys.E => 3m,
            Keys.R => 4m,
            Keys.T => 5m,
            Keys.Y => 6m,
            Keys.U => 7m,
            Keys.I => 8m,
            Keys.O => 9m,
            _ => null
        };

        if (!mappedValue.HasValue)
        {
            return;
        }

        numMissileTargetColumn.Value = mappedValue.Value;
        e.Handled = true;
        e.SuppressKeyPress = true;
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
                // Les pions sont autorises uniquement aux intersections internes.
                if (point.X <= 0 || point.X >= _columns || point.Y <= 0 || point.Y >= _rows)
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

        // Calcul de la position reelle depuis les valeurs 1-9
        int targetColumn = ComputeTargetColumn();
        int targetRow = ComputeTargetRow();

        if (targetColumn <= 0 || targetColumn >= _columns || targetRow <= 0 || targetRow >= _rows)
        {
            MessageBox.Show("La cible est hors des intersections internes.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            $"{shooter} tire en X={numMissileTargetColumn.Value:0.0} → colonne {targetColumn}, Y={numMissileTargetRow.Value:0.0} → ligne {targetRow}. Pions retires: {(removed ? 1 : 0)}.",
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

        // On projette le clic sur l'intersection la plus proche.
        int intersectionX = (int)Math.Round(e.X / cellWidth, MidpointRounding.AwayFromZero);
        int intersectionY = (int)Math.Round(e.Y / cellHeight, MidpointRounding.AwayFromZero);

        // Intersections valides uniquement a l'interieur (bords exclus).
        if (intersectionX <= 0 || intersectionX >= _columns || intersectionY <= 0 || intersectionY >= _rows)
        {
            return;
        }

        var clickedIntersection = new Point(intersectionX, intersectionY);
        if (_points.ContainsKey(clickedIntersection))
        {
            return;
        }

        bool currentPlayerIsPrimary = _usePrimaryColorNext;
        _points[clickedIntersection] = currentPlayerIsPrimary;

        if (HasFiveInRow(clickedIntersection, currentPlayerIsPrimary))
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

        // Y : repere utilisateur fixe de 1 a 9 (converti ensuite en intersection interne)
        numMissileTargetRow.Minimum = 1;
        numMissileTargetRow.Maximum = 9;
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
            int previewRow = ComputeTargetRow();
            // lblActionHint.Text = $"Choisis X (1,0-9,0) et Y puis Tirer.  → colonne {previewCol}";
            if (previewCol > 0 && previewCol < _columns && previewRow > 0 && previewRow < _rows)
            {
                lblActionHint.Text = $"Cible missile: X={previewCol}, Y={previewRow} (intersections internes).";
            }
            return;
        }

        lblActionHint.Text = "Clique sur une intersection interne pour poser un pion.";
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
        graphics.Clear(Color.White);

        float usableWidth  = pnlGrid.ClientSize.Width  - 1;
        float usableHeight = pnlGrid.ClientSize.Height - 1;
        float cellWidth    = usableWidth  / _columns;
        float cellHeight   = usableHeight / _rows;

        // --- Previsualisation du missile (grand cercle sur intersection) ---
        if (rdoActionMissile.Checked && !_isGameOver)
        {
            int previewCol = ComputeTargetColumn();
            int previewRow = ComputeTargetRow();

            if (previewCol > 0 && previewCol < _columns && previewRow > 0 && previewRow < _rows)
            {
                float cx = previewCol * cellWidth;
                float cy = previewRow * cellHeight;
                float previewDiameter = MathF.Max(12f, MathF.Min(cellWidth, cellHeight) * 0.9f);
                float previewRadius = previewDiameter / 2f;

                using var previewFill = new SolidBrush(Color.FromArgb(90, Color.OrangeRed));
                using var previewBorder = new Pen(Color.OrangeRed, 2.5f);

                graphics.FillEllipse(previewFill, cx - previewRadius, cy - previewRadius, previewDiameter, previewDiameter);
                graphics.DrawEllipse(previewBorder, cx - previewRadius, cy - previewRadius, previewDiameter, previewDiameter);

                using var crossPen = new Pen(Color.Red, 2f);
                float arm = previewRadius * 0.65f;
                graphics.DrawLine(crossPen, cx - arm, cy, cx + arm, cy);
                graphics.DrawLine(crossPen, cx, cy - arm, cx, cy + arm);
            }
        }

        // --- Grille ---
        using var pen = new Pen(Color.DimGray, 1f);

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
        float pointRadius   = pointDiameter / 2f;

        using var primaryBrush   = new SolidBrush(Color.Firebrick);
        using var secondaryBrush = new SolidBrush(Color.RoyalBlue);

        foreach (KeyValuePair<Point, bool> entry in _points)
        {
            Point intersection = entry.Key;
            float centerX = intersection.X * cellWidth;
            float centerY = intersection.Y * cellHeight;
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

    private sealed record PointState(int X, int Y, bool IsPrimary);
}