using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperPlugin.CodeCommenter.Entities.Network;

namespace JetBrains.Rider.Model;

public class MethodDescriptor : IFileSystemDescriptor
{
    public IMethodDeclaration Declaration { get; init; }
    public string Identifier { get; init; }
    public string Name { get; init; }
    public string Docstring { get; init; }
    public double Coverage { get; init; }
    public Quality Quality { get; set; }
    public IFileSystemDescriptor Parent { get; init; }
    public List<IFileSystemDescriptor> Children { get; } = new();
}
