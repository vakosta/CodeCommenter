using System.Collections.Generic;

namespace JetBrains.Rider.Model;

public class ModuleDescriptor
{
    public string Name { get; set; }
    public List<FileDescriptor> Files { get; } = new();
}
