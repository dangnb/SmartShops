using System.Reflection;

namespace Shop.AssemblyReference.Dapper;
public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}

