using GameOfLife.util.exception;
using GameOfLife.util.math;

namespace GameOfLife.util.board;

public class Board
{
    private Cell[] _cells;
    
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
        
        List<Cell> cells = new List<Cell>(Size);

        for (int i = 0; i < cells.Count; i++)
        {
            cells[0] = new Cell(GetPositionOfIndex(i));
        }
    }

    public CellPosition GetPositionOfIndex(int i)
    {
        int x = i % Width;
        int y = i / Width;
        
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
            catch (IndexOutOfRangeException)
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