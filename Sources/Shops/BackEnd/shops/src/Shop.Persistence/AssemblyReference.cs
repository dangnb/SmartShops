using System.Reflection;

namespace Shop.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}
