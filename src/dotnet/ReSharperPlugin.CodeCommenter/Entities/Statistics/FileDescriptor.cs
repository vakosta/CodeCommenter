using System.Collections.Generic;

namespace JetBrains.Rider.Model;

public class FileDescriptor
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public List<MethodDescriptor> Methods { get; } = new();
}
