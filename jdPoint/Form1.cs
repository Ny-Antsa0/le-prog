namespace jdPoint;

public partial class Form1 : Form
{
    private int _columns = 10;
    private int _rows = 10;
    private readonly Dictionary<Point, bool> _points = new();
    private bool _usePrimaryColorNext = true;
    private bool _isGameOver;

    public Form1()
    {
        InitializeComponent();
        numColumns.ValueChanged += GridValueChanged;
        numRows.ValueChanged += GridValueChanged;
        pnlGrid.Resize += GridPanelResize;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
        UpdateGridDimensions();
    }

    private void GridValueChanged(object? sender, EventArgs e)
    {
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

    private void RestartGame()
    {
        _points.Clear();
        _usePrimaryColorNext = true;
        _isGameOver = false;
        pnlGrid.Invalidate();
    }

    private void pnlGrid_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || _columns <= 0 || _rows <= 0 || _isGameOver)
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

        pnlGrid.Invalidate();
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

        float usableWidth = pnlGrid.ClientSize.Width - 1;
        float usableHeight = pnlGrid.ClientSize.Height - 1;
        float cellWidth = usableWidth / _columns;
        float cellHeight = usableHeight / _rows;

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

        float pointDiameter = MathF.Max(4f, MathF.Min(cellWidth, cellHeight) * 0.45f);
        float pointRadius = pointDiameter / 2f;
        using var primaryBrush = new SolidBrush(Color.Firebrick);
        using var secondaryBrush = new SolidBrush(Color.RoyalBlue);

        foreach (KeyValuePair<Point, bool> entry in _points)
        {
            Point cell = entry.Key;
            float centerX = (cell.X + 0.5f) * cellWidth;
            float centerY = (cell.Y + 0.5f) * cellHeight;
            Brush brush = entry.Value ? primaryBrush : secondaryBrush;
            graphics.FillEllipse(brush, centerX - pointRadius, centerY - pointRadius, pointDiameter, pointDiameter);
        }
    }
}
