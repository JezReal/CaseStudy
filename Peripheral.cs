public class Peripheral
{
    public long Code { get; set; }
    public string Name { get; set; }
    public PeripheralType Type { get; set; }
    public string? AssignedTo { get; set; }

    public Peripheral(long code, string name, PeripheralType type, string? assignedTo)
    {
        this.Code = code;
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