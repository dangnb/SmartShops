using System.Reflection;

namespace Shop.Contract;
public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}
