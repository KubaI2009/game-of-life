using GameOfLife.util.exception;
using GameOfLife.util.math;

namespace GameOfLife.util.board;

public class Board
{
    private List<Cell> _cells;
    
    public int Width { get; protected init; }
    public int Height { get; protected init; }

    public int Size
    {
        get => Width * Height;
    }

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        
        _cells = new List<Cell>();

        for (int i = 0; i < Size; i++)
        {
            _cells.Add(new Cell(GetPositionOfIndex(i)));
        }
    }

    public (int x, int y) GetCoordinatesOfIndex(int i)
    {
        int x = i % Width;
        int y = i / Width;
        
        return (x, y);
    }

    public CellPosition GetPositionOfIndex(int i)
    {
        (int x, int y) = GetCoordinatesOfIndex(i);
        
        return new CellPosition(x, y);
    }

    public int GetIndexOfPosition(int x, int y)
    {
        return y * Width + x;
    }

    public int GetIndexOfPosition(CellPosition position)
    {
        return GetIndexOfPosition(position.X, position.Y);
    }

    /// <summary>
    /// Updates this board using the data from the other board
    /// </summary>
    /// <param name="other"></param>
    public void Update(Board other)
    {
        if (Size != other.Size)
        {
            throw new MismatchedBoardSizeException();
        }

        foreach (Cell cell in _cells)
        {
            Cell otherCell = other.CellAt(cell.Position);
            CellState[] neighborStates = GetStatesOfCells(GetNeighborsOfPosition(otherCell.Position));
            
            cell.Update(neighborStates);
        }
    }

    public Cell[] GetNeighborsOfPosition(CellPosition position)
    {
        CellPosition[] neighborPositions = position.MooreNeighborPositions;
        List<Cell> neighbors = new List<Cell>();

        foreach (CellPosition neighborPosition in neighborPositions)
        {
            try
            {
                Cell neighbor = CellAt(neighborPosition);
                
                neighbors.Add(neighbor);
            }
            catch (ArgumentOutOfRangeException)
            {
                continue;
            }
        }
        
        return neighbors.ToArray();
    }

    public Cell CellAt(int i)
    {
        return _cells[i];
    }

    public Cell CellAt(int x, int y)
    {
        return CellAt(GetIndexOfPosition(x, y));
    }

    public Cell CellAt(CellPosition position)
    {
        return CellAt(GetIndexOfPosition(position));
    }

    public Board Copy()
    {
        Board copy = new Board(Width, Height);
        
        copy.Overwrite(this);
        
        return copy;
    }

    public void Overwrite(Board other)
    {
        if (Size != other.Size)
        {
            throw new MismatchedBoardSizeException();
        }
        
        for (int i = 0; i < Size; i++)
        {
            //Console.WriteLine(i);
            CellAt(i).OverwriteState(other.CellAt(i));
        }
    }

    public override string ToString() => "<GameOfLife.util.board.Board>";

    private static CellState[] GetStatesOfCells(Cell[] cells)
    {
        List<CellState> states = new List<CellState>();
        
        foreach (Cell cell in cells)
        {
            states.Add(cell.State);
        }
        
        return states.ToArray();
    }
}