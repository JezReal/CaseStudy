public class Peripheral
{
    public long Code { get; set; }
    public string Name { get; set; }
    public PeripheralType Type { get; set; }
    public string? AssignedTo { get; set; }
    private static int ItemCount { get; set; } = 1;

    public Peripheral(string name, PeripheralType type, string? assignedTo)
    {
        var currentYear = DateTime.Now.Year;
        var minimumLength = 4;
        var itemCountLength = ItemCount.ToString().Length;

        var code = currentYear.ToString();

        if (itemCountLength < minimumLength)
        {
            for (int i = 0; i < minimumLength - itemCountLength; i++)
            {
                code += "0";
            }

            code += ItemCount++;
        }
        else
        {
            code += ItemCount++;
        }

        this.Code = Convert.ToInt64(code);
        this.Name = name;
        this.Type = type;
        this.AssignedTo = assignedTo;
    }

    public override string ToString()
    {
        var peripheralType = "";

        if (this.Type == PeripheralType.INPUT)
        {
            peripheralType = "Input device";
        }
        else if (this.Type == PeripheralType.OUTPUT)
        {
            peripheralType = "Output device";
        }
        else
        {
            peripheralType = "Input/Output device";
        }

        return $"Peripheral Info:\n\nCode: {this.Code} \nName: {this.Name} \nPeripheral Type: {peripheralType} \nAssignedTo: {this.AssignedTo}\n--------";
    }

}