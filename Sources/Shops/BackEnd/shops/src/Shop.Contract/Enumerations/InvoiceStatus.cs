using Ardalis.SmartEnum;

namespace Shop.Contract.Enumerations;
public class InvoiceStatus : SmartEnum<InvoiceStatus>
{
    public InvoiceStatus(string name, int value) : base(name, value)
    {
    }
    public static readonly InvoiceStatus Pay = new(nameof(Pay), 1);
    public static readonly InvoiceStatus Cancel = new(nameof(Cancel), 2);


    public static implicit operator InvoiceStatus(string name) => FromName(name);
    public static implicit operator InvoiceStatus(int value) => FromValue(value);

    public static implicit operator string(InvoiceStatus status) => status.Name;
    public static implicit operator int(InvoiceStatus status) => status.Value;
}
