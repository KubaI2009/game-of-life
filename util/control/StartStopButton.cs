using System.ComponentModel;
using GameOfLife.util.collection;

namespace GameOfLife.util.control;

public class StartStopButton : Button, IControlWithMaster
{
    private static readonly string s_startText = "Start";
    private static readonly string s_stopText = "Stop";
    
    private static readonly TwoStateSwitch<string> s_texts = new TwoStateSwitch<string>(s_startText, s_stopText);
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Form Master { get; protected set; }
    
    public StartStopButton(Form master, string name, Point location, Size size) : base()
    {
        this.Master = master;
        this.Name = name;
        this.Location = location;
        this.Size = size;
        
        this.BackColor = Color.Transparent;
        this.Text = s_texts.FirstValue;
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
    }
}