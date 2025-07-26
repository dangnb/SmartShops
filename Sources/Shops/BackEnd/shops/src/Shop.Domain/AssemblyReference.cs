using System.Reflection;

namespace Shop.Domain;
public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}
