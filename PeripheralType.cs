public enum PeripheralType
{
    INPUT,
    OUTPUT,
    BOTH
}

public static class PeripheralTypeExtensions
{
    public static PeripheralType IntToPeripheralType(this int number)
    {
        if (number == 0)
        {
            return PeripheralType.INPUT;
        }
        else if (number == 1)
        {
            return PeripheralType.OUTPUT;
        }
        else
        {
            return PeripheralType.BOTH;
        }
    }
}
