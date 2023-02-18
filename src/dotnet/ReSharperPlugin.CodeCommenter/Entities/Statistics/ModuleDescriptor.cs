using System.Collections.Generic;

namespace JetBrains.Rider.Model;

public class ModuleDescriptor
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public List<FileDescriptor> Files { get; } = new();
}
