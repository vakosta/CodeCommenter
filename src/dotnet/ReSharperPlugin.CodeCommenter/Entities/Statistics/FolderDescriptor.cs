using System.Collections.Generic;

namespace JetBrains.Rider.Model;

public class FolderDescriptor : IFileSystemDescriptor
{
    public string Name { get; init; }
    public IFileSystemDescriptor Parent { get; init; }
    public List<IFileSystemDescriptor> Children { get; } = new();
}
