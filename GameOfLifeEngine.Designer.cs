using GameOfLife.util.board;
using GameOfLife.util.control;
using GameOfLife.util.math;

namespace GameOfLife;

partial class GameOfLifeEngine
{
    private static readonly string s_title = "Game of Life";
    private static readonly Color s_backgroundColor = Color.Gray;

    private System.Windows.Forms.Timer _timer;
    private long _ticks;
    
    private Board _board;
    private Board _referenceBoard;
    
    private List<RenderedCell> _renderedBoard;
    private StartStopButton _startStopButton;

    public int FormWidth
    {
        get
        {
            return CellWidth * BoardWidth + WidthMargin;
        }
    }

    public int FormHeight
    {
        get
        {
            return CellHeight * BoardHeight + HeightMargin;
        }
    }

    public int CellWidth { get; set; }
    public int CellHeight { get; set; }
    
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    
    public int WidthMargin { get; set; }
    public int HeightMargin { get; set; }
    
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOfLifeEngine));
        SuspendLayout();
        // 
        // GameOfLifeEngine
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
        ResumeLayout(false);
    }

    #endregion

    private void CustomizeComponent()
    {
        SetEngineProperties();
        SetFormProperties();
        CreateControls();
        RenderControls();
    }

    private void SetEngineProperties()
    {
        CellWidth = 20;
        CellHeight = 20;
        BoardWidth = 25;
        BoardHeight = 25;
        WidthMargin = 0;
        HeightMargin = CellHeight * 2;
    }

    private void SetFormProperties()
    {
        this.ClientSize = new System.Drawing.Size(FormWidth, FormHeight);
        this.Text = s_title;
        this.BackColor = s_backgroundColor;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
    }

    private void CreateControls()
    {
        CreateBoard();
        CreateStartStopButton();
    }

    private void CreateBoard()
    {
        _board = new Board(BoardWidth, BoardHeight);
        _referenceBoard = _board.Copy();
        _renderedBoard = new List<RenderedCell>();

        for (int i = 0; i < _board.Size; i++)
        {
            Cell cell = _board.CellAt(i);
            
            _renderedBoard.Add(new RenderedCell(this, $"cl{cell.X}_{cell.Y}", LocationOf(cell.Position), new Size(CellWidth, CellHeight), cell));
        }
    }

    private void CreateStartStopButton()
    {
        _startStopButton = new StartStopButton(this, "btnStartStop", new Point(0, FormHeight - HeightMargin), new Size(FormWidth - WidthMargin, HeightMargin), StartEvent, StopEvent);
    }

    private void RenderControls()
    {
        RenderBoard();
        RenderStartStopButton();
    }

    private void RenderBoard()
    {
        foreach (RenderedCell cell in _renderedBoard)
        {
            Controls.Add(cell);
        }
    }

    private void RenderStartStopButton()
    {
        Controls.Add(_startStopButton);
    }

    private void UpdateBoard()
    {
        _board.Update(_referenceBoard);
        _referenceBoard = _board.Copy();
    }

    private void UpdateRenderedBoard()
    {
        foreach (RenderedCell cell in  _renderedBoard)
        {
            cell.Render();
        }
    }

    private void DisableCellClicking()
    {
        foreach (RenderedCell cell in _renderedBoard)
        {
            cell.Disable();
        }
    }

    private void InitTimer()
    {
        _timer = new System.Windows.Forms.Timer();
        _ticks = 0;
        
        _timer.Interval = 500;
        _timer.Tick += UpdateEvent;
        
        _timer.Start();
    }

    private void KillTimer()
    {
        _timer.Stop();
    }

    private Point LocationOf(CellPosition position)
    {
        return LocationOf(position.X, position.Y);
    }

    private Point LocationOf(int x, int y)
    {
        int rX = x * CellWidth;
        int rY = y * CellHeight;
        
        return new Point(rX, rY);
    }
    
    private void StartEvent(object? sender, EventArgs? e)
    {
        DisableCellClicking();
        InitTimer();

        if (sender is StartStopButton button)
        {
            button.SwapText();
            button.SetToStop();
        }
    }

    private void UpdateEvent(object? sender, EventArgs? e)
    {
        _ticks++;
        Console.WriteLine(_ticks);
        
        UpdateBoard();
        UpdateRenderedBoard();
    }

    private void StopEvent(object? sender, EventArgs? e)
    {
        KillTimer();
    }
}