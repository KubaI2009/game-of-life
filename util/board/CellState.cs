namespace GameOfLife.util.board;

public record struct CellState(bool IsAlive, Color Color)
{
    public static readonly CellState Alive = new(true, Color.WhiteSmoke);
    public static readonly CellState Dead = new(false, Color.Black);
}