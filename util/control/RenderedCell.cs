using System.ComponentModel;
using GameOfLife.util.board;

namespace GameOfLife.util.control;

public class RenderedCell : Button, IControlWithMaster
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected Cell Cell;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected bool IsActive;
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form Master { get; protected set; }
    
    public RenderedCell(Form master, string name, Point location, Size size, Cell cell) : base()
    {
        Master = master;
        Name = name;
        Location = location;
        Size = size;
        Cell = cell;
        
        BackColor = Color.Black;
        IsActive = true;
        Click += ClickEvent;
    }

    public void Render()
    {
        BackColor = Cell.State.Color;
    }

    public void Enable()
    {
        IsActive = true;
    }

    public void Disable()
    {
        IsActive = false;
    }

    private void ClickEvent(object? sender, EventArgs e)
    {
        if (!IsActive)
        {
            return;
        }
        
        Cell.SwitchState();
        Render();
    }
}