using Ardalis.SmartEnum;

namespace Shop.Contract.Enumerations;
public class InvoiceType : SmartEnum<InvoiceType>
{
    public InvoiceType(string name, int value) : base(name, value)
    {
    }
    public static readonly InvoiceType Individual = new(nameof(Individual), 1);
    public static readonly InvoiceType Enterprise = new(nameof(Enterprise), 2);


    public static implicit operator InvoiceType(string name) => FromName(name);
    public static implicit operator InvoiceType(int value) => FromValue(value);

    public static implicit operator string(InvoiceType status) => status.Name;
    public static implicit operator int(InvoiceType status) => status.Value;
}
