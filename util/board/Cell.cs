using GameOfLife.util.math;

namespace GameOfLife.util.board;

public class Cell(CellState startingState, CellPosition position, HashSet<int> birthNumsNeighborNumbers, HashSet<int> deathNumsNeighborNumbers)
{
    private HashSet<int> _birthNums = birthNumsNeighborNumbers;
    private HashSet<int> _deathNums = deathNumsNeighborNumbers;
    
    public CellState State { get; protected set; } = startingState;
    public CellPosition Position { get; protected set; } = position;

    public bool IsAlive
    {
        get => State.IsAlive;
    }

    public int X
    {
        get => Position.X;
    }

    public int Y
    {
        get => Position.Y;
    }
    
    public Cell(CellState startingState, CellPosition position) : 
        this(startingState, 
            position,
            new HashSet<int>() { 3 }, 
            new HashSet<int>() { 2, 3 }) { }
    
    public Cell(CellPosition position) : this(CellState.Dead, position) { }

    public void BeBorn()
    {
        State = CellState.Alive;
    }

    public void Kill()
    {
        State = CellState.Dead;
    }

    public void Update(CellState[] neighborStates)
    {
        int alive = CountLiving(neighborStates);

        if (IsAlive && _deathNums.Contains(alive))
        {
            Kill();
            return;
        }

        if (!IsAlive && _birthNums.Contains(alive))
        {
            BeBorn();
            return;
        }
    }

    public void SwitchState()
    {
        if (IsAlive)
        {
            Kill();
            return;
        }
        
        BeBorn();
    }

    public void OverwriteState(Cell other)
    {
        State = other.State;
    }
    
    public override string ToString() => "<GameOfLife.util.board.Cell>";

    private static int CountLiving(CellState[] states)
    {
        int alive = 0;

        foreach (CellState state in states)
        {
            alive += state.IsAlive ? 1 : 0;
        }
        
        return alive;
    }
}