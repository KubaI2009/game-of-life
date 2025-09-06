namespace GameOfLife.util.board;

public record struct CellState(bool IsAlive, Color Color, char Symbol)
{
    public static readonly CellState Alive = new(true, Color.WhiteSmoke, 'x');
    public static readonly CellState Dead = new(false, Color.Black, '=');
}