using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace JetBrains.Rider.Model;

public class FileDescriptor
{
    public string Name { get; init; }
    public List<MethodDescriptor> Methods { get; } = new();
}
