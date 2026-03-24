namespace jdPoint;

using System.Text.Json;
using Npgsql;
using System.Threading;
using System.Threading.Tasks;

public partial class Form1 : Form
{
    private const string DefaultConnectionString = "Host=localhost;Port=5433;Username=postgres;Password=etu003146;Database=point";
    private int _columns = 10;
    private int _rows = 10;
    private readonly Dictionary<Point, bool> _points = new();
    private readonly HashSet<Point> _invulnerablePoints = new();
    private bool _usePrimaryColorNext = true;
    private bool _isGameOver;
    private bool _isApplyingLoadedState;
    private bool _isAIMode = false;

    public Form1()
    {
        InitializeComponent();
        numColumns.ValueChanged += GridValueChanged;
        numRows.ValueChanged += GridValueChanged;
        pnlGrid.Resize += GridPanelResize;
        Shown += Form1_Shown;
        
        // Initialisation des gestionnaires d'événements pour les nouveaux contrôles
        rbPlacePoint.CheckedChanged += ActionSelectionChanged;
        rbFireMissile.CheckedChanged += ActionSelectionChanged;
        numPower.ValueChanged += MissileParametersChanged;
        rbPvP.CheckedChanged += GameModeChanged;
        rbPvAI.CheckedChanged += GameModeChanged;
        
        UpdateCurrentPlayerDisplay();
        UpdateMissileControls();
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
            
            List<Point> serializedInvulnerablePoints = _invulnerablePoints.ToList();

            string pointsJson = JsonSerializer.Serialize(serializedPoints);
            string invulnerableJson = JsonSerializer.Serialize(serializedInvulnerablePoints);

            using var connection = new NpgsqlConnection(GetConnectionString());
            connection.Open();

            using var command = new NpgsqlCommand(@"
INSERT INTO game_saves(save_name, columns_count, rows_count, points_json, invulnerable_json, use_primary_color_next, is_game_over, updated_at)
VALUES (@save_name, @columns_count, @rows_count, @points_json::jsonb, @invulnerable_json::jsonb, @use_primary_color_next, @is_game_over, NOW())
ON CONFLICT (save_name) DO UPDATE
SET columns_count = EXCLUDED.columns_count,
    rows_count = EXCLUDED.rows_count,
    points_json = EXCLUDED.points_json,
    invulnerable_json = EXCLUDED.invulnerable_json,
    use_primary_color_next = EXCLUDED.use_primary_color_next,
    is_game_over = EXCLUDED.is_game_over,
    updated_at = NOW();", connection);

            command.Parameters.AddWithValue("save_name", saveName);
            command.Parameters.AddWithValue("columns_count", _columns);
            command.Parameters.AddWithValue("rows_count", _rows);
            command.Parameters.AddWithValue("points_json", pointsJson);
            command.Parameters.AddWithValue("invulnerable_json", invulnerableJson);
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
SELECT columns_count, rows_count, points_json::text, invulnerable_json::text, use_primary_color_next, is_game_over
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
            string invulnerableJson = reader.IsDBNull(3) ? "[]" : reader.GetString(3);
            bool loadedUsePrimaryColorNext = reader.GetBoolean(4);
            bool loadedIsGameOver = reader.GetBoolean(5);

            List<PointState>? loadedPoints = JsonSerializer.Deserialize<List<PointState>>(pointsJson);
            if (loadedPoints is null)
            {
                loadedPoints = new List<PointState>();
            }
            
            List<Point>? loadedInvulnerablePoints = JsonSerializer.Deserialize<List<Point>>(invulnerableJson);
            if (loadedInvulnerablePoints is null)
            {
                loadedInvulnerablePoints = new List<Point>();
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
            _invulnerablePoints.Clear();

            foreach (PointState point in loadedPoints)
            {
                if (point.X < 0 || point.X >= _columns || point.Y < 0 || point.Y >= _rows)
                {
                    continue;
                }

                _points[new Point(point.X, point.Y)] = point.IsPrimary;
            }

            foreach (Point point in loadedInvulnerablePoints)
            {
                if (point.X >= 0 && point.X < _columns && point.Y >= 0 && point.Y < _rows)
                {
                    _invulnerablePoints.Add(point);
                }
            }

            _usePrimaryColorNext = loadedUsePrimaryColorNext;
            _isGameOver = loadedIsGameOver;

            UpdateCurrentPlayerDisplay();
            UpdateMissileControls();
            pnlGrid.Invalidate();
            MessageBox.Show("Partie chargee.", "Chargement", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Echec du chargement.\n\n{ex.Message}", "Chargement", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RestartGame()
    {
        _points.Clear();
        _invulnerablePoints.Clear();
        _usePrimaryColorNext = true;
        _isGameOver = false;
        UpdateCurrentPlayerDisplay();
        UpdateMissileControls();
        pnlGrid.Invalidate();
    }

    private void ActionSelectionChanged(object? sender, EventArgs e)
    {
        UpdateMissileControls();
        UpdateCurrentPlayerDisplay();
    }

    private void MissileParametersChanged(object? sender, EventArgs e)
    {
        UpdateMissileControls();
    }

    private void UpdateCurrentPlayerDisplay()
    {
        if (_isAIMode)
        {
            string currentPlayer = _usePrimaryColorNext ? "🤖 IA (Rouge)" : "👤 Joueur (Bleu)";
            string action = rbPlacePoint.Checked ? "Poser" : "Missile";
            lblCurrentPlayer.Text = $"{currentPlayer} ({action})";
            lblCurrentPlayer.ForeColor = _usePrimaryColorNext ? Color.Firebrick : Color.RoyalBlue;
        }
        else
        {
            string playerName = _usePrimaryColorNext ? "Rouge" : "Bleu";
            string action = rbPlacePoint.Checked ? "Poser" : "Missile";
            lblCurrentPlayer.Text = $"Joueur {playerName} ({action})";
            lblCurrentPlayer.ForeColor = _usePrimaryColorNext ? Color.Firebrick : Color.RoyalBlue;
        }
    }

    private void UpdateMissileControls()
    {
        bool missileMode = rbFireMissile.Checked;
        // En mode IA, seul le joueur (Bleu) peut utiliser les missiles, pas l'IA
        bool canUseMissiles = missileMode && !_isGameOver && (!_isAIMode || !_usePrimaryColorNext);
        grpMissile.Enabled = canUseMissiles;
        
        if (missileMode)
        {
            // Calculer automatiquement la ligne cible et la colonne cible
            int power = (int)numPower.Value;
            int targetRow = CalculateTargetRow(power, _rows);
            int targetColumn = CalculateTargetColumn(power, _columns);
            
            // Afficher les informations calculées
            lblTargetRow.Text = $"Ligne: {targetRow + 1}, Colonne: {targetColumn + 1}\n(puissance: {power})";
        }
    }

    private void GameModeChanged(object? sender, EventArgs e)
    {
        _isAIMode = rbPvAI.Checked;
        UpdateCurrentPlayerDisplay();
        UpdateMissileControls();
        
        if (_isAIMode)
        {
            // Choisir aléatoirement qui commence en mode IA
            Random rand = new Random();
            bool aiStarts = rand.Next(2) == 0;
            
            if (aiStarts)
            {
                _usePrimaryColorNext = true; // IA joue en Rouge
                MessageBox.Show("Mode Joueur vs IA activé!\nL'IA commence!", "Mode de jeu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task.Run(() => MakeAIMove()); // L'IA joue directement
            }
            else
            {
                _usePrimaryColorNext = false; // Joueur joue en Bleu
                MessageBox.Show("Mode Joueur vs IA activé!\nVous commencez!", "Mode de jeu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private int CalculateTargetRow(int power, int gridSize)
    {
        // Calculer la ligne cible selon la même formule que la colonne
        // Division entière automatique (partie entière des décimales)
        return (gridSize * power) / 9;
    }

    private int CalculateTargetColumn(int power, int gridSize)
    {
        // Division entière automatique - on prend toujours la partie entière
        // Ex: 2.95 devient 2, 2.54 devient 2, etc.
        return (gridSize * power) / 9;
    }

    private void btnFireMissile_Click(object? sender, EventArgs e)
    {
        if (_isGameOver)
            return;

        int power = (int)numPower.Value;
        int targetRow = CalculateTargetRow(power, _rows);
        int targetColumn = CalculateTargetColumn(power, _columns);

        // Vérifier que la cible est dans les limites
        if (targetRow < 0 || targetRow >= _rows || targetColumn < 0 || targetColumn >= _columns)
        {
            MessageBox.Show("La cible du missile est en dehors de la grille!", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var targetPoint = new Point(targetColumn, targetRow);
        
        // Vérifier si le point est invulnérable
        if (_invulnerablePoints.Contains(targetPoint))
        {
            MessageBox.Show("Ce point est protégé par un alignement de 5 points!", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SwitchPlayer();
            return;
        }

        // Retirer le point s'il existe
        bool pointRemoved = _points.Remove(targetPoint);
        
        if (pointRemoved)
        {
            MessageBox.Show($"Missile tiré avec puissance {power}!\nPoint éliminé en ligne {targetRow + 1}, colonne {targetColumn + 1}.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show($"Missile tiré avec puissance {power}!\nAucun point trouvé en ligne {targetRow + 1}, colonne {targetColumn + 1}.", "Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        SwitchPlayer();
        pnlGrid.Invalidate();
    }

    private void SwitchPlayer()
    {
        if (_isAIMode)
        {
            // En mode IA, alterner entre joueur et IA
            _usePrimaryColorNext = !_usePrimaryColorNext;
            UpdateCurrentPlayerDisplay();
            
            // Si c'est le tour de l'IA (Rouge = true)
            if (_usePrimaryColorNext)
            {
                Task.Run(() => MakeAIMove());
            }
        }
        else
        {
            _usePrimaryColorNext = !_usePrimaryColorNext;
            UpdateCurrentPlayerDisplay();
            UpdateMissileControls();
        }
    }

    private void MakeAIMove()
    {
        // Attendre un peu pour que l'interface se mette à jour
        Thread.Sleep(500);
        
        this.Invoke(() =>
        {
            if (_isGameOver || !_isAIMode) return;

            // Stratégie simple de l'IA
            var availableMoves = GetAvailableMoves();
            if (availableMoves.Count == 0) return;

            // L'IA choisit aléatoirement entre poser un point ou tirer un missile
            Random rand = new Random();
            bool useMissile = rand.Next(2) == 0; // Utiliser missile si 50% de chance

            if (useMissile)
            {
                // L'IA tire un missile
                int power = rand.Next(1, 10); // Puissance aléatoire 1-9
                int targetRow = CalculateTargetRow(power, _rows);
                int targetColumn = CalculateTargetColumn(power, _columns);
                var targetPoint = new Point(targetColumn, targetRow);

                // Retirer le point s'il existe et n'est pas invulnérable
                if (_points.ContainsKey(targetPoint) && !_invulnerablePoints.Contains(targetPoint))
                {
                    _points.Remove(targetPoint);
                    MessageBox.Show($"L'IA tire un missile!\nPoint éliminé en ligne {targetRow + 1}, colonne {targetColumn + 1}.", "IA Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"L'IA tire un missile!\nAucun point trouvé en ligne {targetRow + 1}, colonne {targetColumn + 1}.", "IA Missile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // L'IA pose un point
                // Priorité: 1. Bloquer les alignements adverses 2. Créer des alignements 3. Au hasard
                var bestMove = FindBestMove(availableMoves);
                
                if (bestMove.HasValue)
                {
                    ExecuteAIMove(bestMove.Value);
                    return;
                }
            }

            // Changer de joueur pour le prochain tour
            SwitchPlayer();
            pnlGrid.Invalidate();
        });
    }

    private List<Point> GetAvailableMoves()
    {
        var moves = new List<Point>();
        for (int x = 0; x < _columns; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                var point = new Point(x, y);
                if (!_points.ContainsKey(point))
                {
                    moves.Add(point);
                }
            }
        }
        return moves;
    }

    private Point? FindBestMove(List<Point> availableMoves)
    {
        Random rand = new Random();
        
        // 1. Vérifier si l'IA peut bloquer un alignement adverse
        foreach (var move in availableMoves)
        {
            _points[move] = false; // Simuler point adverse (Bleu = false)
            if (HasFiveInRow(move, false))
            {
                _points.Remove(move); // Annuler la simulation
                return move; // Bloquer l'adversaire est prioritaire
            }
            _points.Remove(move); // Annuler la simulation
        }

        // 2. Vérifier si l'IA peut créer un alignement
        foreach (var move in availableMoves)
        {
            _points[move] = true; // Simuler point IA (Rouge = true)
            if (HasFiveInRow(move, true))
            {
                _points.Remove(move); // Annuler la simulation
                return move; // Créer un alignement est la deuxième priorité
            }
            _points.Remove(move); // Annuler la simulation
        }

        // 3. Sinon, jouer au hasard
        return availableMoves[rand.Next(availableMoves.Count)];
    }

    private void ExecuteAIMove(Point move)
    {
        bool currentPlayerIsPrimary = _usePrimaryColorNext;
        _points[move] = currentPlayerIsPrimary;

        if (HasFiveInRow(move, currentPlayerIsPrimary))
        {
            MarkAlignedPointsAsInvulnerable(move, currentPlayerIsPrimary);
            MessageBox.Show("L'IA a aligné 5 points !\nCes points sont maintenant invulnérables.", "IA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        // Changer de joueur pour le prochain tour
        SwitchPlayer();
        pnlGrid.Invalidate();
    }

    private void pnlGrid_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || _columns <= 1 || _rows <= 1 || _isGameOver)
        {
            return;
        }

        // Ne permettre de poser des points qu'en mode "poser un point"
        if (!rbPlacePoint.Checked)
        {
            return;
        }

        // En mode IA, ne permettre que les coups du joueur (Bleu = false)
        if (_isAIMode && _usePrimaryColorNext)
        {
            return; // C'est le tour de l'IA, le joueur ne peut pas jouer
        }

        float usableWidth = pnlGrid.ClientSize.Width - 1;
        float usableHeight = pnlGrid.ClientSize.Height - 1;

        if (usableWidth <= 0 || usableHeight <= 0)
        {
            return;
        }

        float cellWidth = usableWidth / (_columns - 1);
        float cellHeight = usableHeight / (_rows - 1);

        // Calculer l'intersection la plus proche
        int cellX = (int)Math.Round(e.X / cellWidth);
        int cellY = (int)Math.Round(e.Y / cellHeight);

        // Limiter aux bornes de la grille
        cellX = Math.Clamp(cellX, 0, _columns - 1);
        cellY = Math.Clamp(cellY, 0, _rows - 1);

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
            // Marquer les points alignés comme invulnérables mais continuer le jeu
            MarkAlignedPointsAsInvulnerable(clickedCell, currentPlayerIsPrimary);
            string winnerName = currentPlayerIsPrimary ? "Rouge" : "Bleu";
            MessageBox.Show($"{winnerName} a aligné 5 points !\nCes points sont maintenant invulnérables aux missiles.", "Alignement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SwitchPlayer();
        }
        else
        {
            SwitchPlayer();
        }

        pnlGrid.Invalidate();
    }

    private bool HasFiveInRow(Point origin, bool playerColor)
    {
        return CountAligned(origin, 1, 0, playerColor) >= 5
            || CountAligned(origin, 0, 1, playerColor) >= 5
            || CountAligned(origin, 1, 1, playerColor) >= 5
            || CountAligned(origin, 1, -1, playerColor) >= 5;
    }

    private void MarkAlignedPointsAsInvulnerable(Point origin, bool playerColor)
    {
        if (CountAligned(origin, 1, 0, playerColor) >= 5)
            MarkDirectionAsInvulnerable(origin, 1, 0, playerColor);
        if (CountAligned(origin, 0, 1, playerColor) >= 5)
            MarkDirectionAsInvulnerable(origin, 0, 1, playerColor);
        if (CountAligned(origin, 1, 1, playerColor) >= 5)
            MarkDirectionAsInvulnerable(origin, 1, 1, playerColor);
        if (CountAligned(origin, 1, -1, playerColor) >= 5)
            MarkDirectionAsInvulnerable(origin, 1, -1, playerColor);
    }

    private void MarkDirectionAsInvulnerable(Point origin, int dx, int dy, bool playerColor)
    {
        // Marquer le point d'origine
        _invulnerablePoints.Add(origin);
        
        // Marquer dans la direction positive
        int x = origin.X + dx;
        int y = origin.Y + dy;
        while (x >= 0 && x < _columns && y >= 0 && y < _rows)
        {
            var current = new Point(x, y);
            if (!_points.TryGetValue(current, out bool currentColor) || currentColor != playerColor)
            {
                break;
            }
            _invulnerablePoints.Add(current);
            x += dx;
            y += dy;
        }
        
        // Marquer dans la direction négative
        x = origin.X - dx;
        y = origin.Y - dy;
        while (x >= 0 && x < _columns && y >= 0 && y < _rows)
        {
            var current = new Point(x, y);
            if (!_points.TryGetValue(current, out bool currentColor) || currentColor != playerColor)
            {
                break;
            }
            _invulnerablePoints.Add(current);
            x -= dx;
            y -= dy;
        }
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
        if (_columns <= 1 || _rows <= 1)
        {
            return;
        }

        var graphics = e.Graphics;
        graphics.Clear(Color.White);

        float usableWidth = pnlGrid.ClientSize.Width - 1;
        float usableHeight = pnlGrid.ClientSize.Height - 1;
        float cellWidth = usableWidth / (_columns - 1);
        float cellHeight = usableHeight / (_rows - 1);

        using var pen = new Pen(Color.DimGray, 1f);

        // Dessiner les lignes verticales
        for (int x = 0; x < _columns; x++)
        {
            float xPos = x * cellWidth;
            graphics.DrawLine(pen, xPos, 0, xPos, usableHeight);
        }

        // Dessiner les lignes horizontales
        for (int y = 0; y < _rows; y++)
        {
            float yPos = y * cellHeight;
            graphics.DrawLine(pen, 0, yPos, usableWidth, yPos);
        }

        float pointDiameter = MathF.Max(4f, MathF.Min(cellWidth, cellHeight) * 0.3f);
        float pointRadius = pointDiameter / 2f;
        using var primaryBrush = new SolidBrush(Color.Firebrick);
        using var secondaryBrush = new SolidBrush(Color.RoyalBlue);
        using var invulnerablePen = new Pen(Color.Gold, 3f);

        foreach (KeyValuePair<Point, bool> entry in _points)
        {
            Point cell = entry.Key;
            float centerX = cell.X * cellWidth;
            float centerY = cell.Y * cellHeight;
            Brush brush = entry.Value ? primaryBrush : secondaryBrush;
            
            graphics.FillEllipse(brush, centerX - pointRadius, centerY - pointRadius, pointDiameter, pointDiameter);
            
            // Dessiner un cercle d'or autour des points invulnérables
            if (_invulnerablePoints.Contains(cell))
            {
                graphics.DrawEllipse(invulnerablePen, centerX - pointRadius - 2, centerY - pointRadius - 2, pointDiameter + 4, pointDiameter + 4);
            }
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

        // Ajouter la colonne invulnerable_json si elle n'existe pas
        using var alterCommand = new NpgsqlCommand(@"
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns 
        WHERE table_name = 'game_saves' AND column_name = 'invulnerable_json'
    ) THEN
        ALTER TABLE game_saves ADD COLUMN invulnerable_json JSONB;
    END IF;
END $$;", connection);
        alterCommand.ExecuteNonQuery();

        using var command = new NpgsqlCommand(@"
CREATE TABLE IF NOT EXISTS game_saves
(
    save_name TEXT PRIMARY KEY,
    columns_count INTEGER NOT NULL,
    rows_count INTEGER NOT NULL,
    points_json JSONB NOT NULL,
    invulnerable_json JSONB,
    use_primary_color_next BOOLEAN NOT NULL,
    is_game_over BOOLEAN NOT NULL,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);", connection);

        command.ExecuteNonQuery();
    }

    private sealed record PointState(int X, int Y, bool IsPrimary);
}
