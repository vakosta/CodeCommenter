using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace JetBrains.Rider.Model;

public class FileDescriptor
{
    public string Name { get; set; }
    public List<IMethodDeclaration> Methods { get; } = new();
}
