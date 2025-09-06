using System.ComponentModel;

namespace GameOfLife.util.control;

public class StartStopButton : Button, IControlWithMaster
{
    private static readonly string s_startText = "Start";
    private static readonly string s_stopText = "Stop";

    private EventHandler _start;
    private EventHandler _stop;
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form Master { get; protected set; }
    
    public StartStopButton(Form master, string name, Point location, Size size, EventHandler startEvent, EventHandler stopEvent) : base()
    {
        Master = master;
        Name = name;
        Location = location;
        Size = size;
        
        _start = startEvent;
        _stop = stopEvent;
        
        Click += _start;
        
        BackColor = Color.Transparent;
        Text = s_startText;
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
    }

    public void SwapText()
    {
        Text = Text == s_startText ? s_stopText : s_startText;
    }

    public void SetToStart()
    {
        Click -= _stop;
        Click += _start;
    }

    public void SetToStop()
    {
        Click -= _start;
        Click += _stop;
    }
}