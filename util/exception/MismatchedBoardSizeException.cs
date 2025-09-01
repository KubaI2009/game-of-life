namespace GameOfLife.util.exception;

public class MismatchedBoardSizeException(string message) : Exception(message)
{
    public MismatchedBoardSizeException() : this("Boards must have the same size") { }
}