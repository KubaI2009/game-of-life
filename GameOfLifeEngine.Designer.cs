namespace GameOfLife;

partial class GameOfLifeEngine
{
    private static readonly string s_title = "Game of Life";
    private static readonly Color s_backgroundColor = Color.Black;

    public int FormWidth
    {
        get
        {
            return CellWidth * BoardWidth;
        }
    }

    public int FormHeight
    {
        get
        {
            return CellHeight * BoardHeight;
        }
    }

    public int CellWidth { get; set; }
    public int CellHeight { get; set; }
    
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    
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
    }

    private void SetEngineProperties()
    {
        CellWidth = 20;
        CellHeight = 20;
        BoardWidth = 25;
        BoardHeight = 25;
    }

    private void SetFormProperties()
    {
        this.ClientSize = new System.Drawing.Size(FormWidth, FormHeight);
        this.Text = s_title;
        this.BackColor = s_backgroundColor;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
    }
}