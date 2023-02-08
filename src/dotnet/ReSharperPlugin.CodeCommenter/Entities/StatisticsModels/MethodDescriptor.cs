using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace JetBrains.Rider.Model;

public class MethodDescriptor
{
    public IMethodDeclaration Declaration { get; init; }
    public string Name { get; init; }
    public string Docstring { get; init; }
    public float Coverage { get; init; }
    public float Quality { get; set; }
    public bool IsLoading { get; set; } = true;
}
