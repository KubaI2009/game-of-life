namespace GameOfLife.util.collection;

public class TwoStateSwitch<T>(T firstValue, T secondValue)
{
    public T FirstValue { get; private set; } = firstValue;
    public T SecondValue { get; private set; } = secondValue;

    public (T firstValue, T secondValue) Values
    {
        get => (FirstValue, SecondValue);
    }

    public void SwapValues()
    {
        (FirstValue, SecondValue) = (SecondValue, FirstValue);
    }

    public (T firstValue, T secondValue) SwapAndGetValues()
    {
        SwapValues();
        return Values;
    }
}