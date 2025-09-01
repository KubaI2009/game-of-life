using System.Diagnostics.CodeAnalysis;

namespace GameOfLife.util.math;

public class CellPosition(int x, int y)
{
    public static CellPosition Zero
    {
        get => new CellPosition(0, 0);
    }
    
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    //###
    //# #
    //###
    public CellPosition[] MooreNeighborPositions
    {
        get
        {
            CellPosition[] neighbors = GetMooreNeighborSummands();

            foreach (CellPosition neighbor in neighbors)
            {
                neighbor.Add(this);
            }
            
            return neighbors;
        }
    }

    public void Add(CellPosition other)
    {
        Add(other.X, other.Y);
    }

    public void Add(int x, int y)
    {
        X += x;
        Y += y;
    }
    
    public CellPosition Clone() => new CellPosition(X, Y);

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is CellPosition other)
        {
            return other.X == X && other.Y == Y;
        }
        
        return false;
    }

    public override string ToString() => "<GameOfLife.util.Vector2Int>";

    private static CellPosition[] GetMooreNeighborSummands()
    {
        return new CellPosition[]
        {
            new CellPosition(-1, -1),
            new CellPosition(0, -1),
            new CellPosition(1, -1),
            new CellPosition(-1, 0),
            new CellPosition(1, 0),
            new CellPosition(-1, 1),
            new CellPosition(0, 1),
            new CellPosition(1, 1)
        };
    }
}