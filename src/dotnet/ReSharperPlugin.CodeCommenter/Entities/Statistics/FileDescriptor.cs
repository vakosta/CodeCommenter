using System.Collections.Generic;

namespace JetBrains.Rider.Model;

public class FileDescriptor : IFileSystemDescriptor
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public IFileSystemDescriptor Parent { get; init; }
    public List<IFileSystemDescriptor> Children { get; } = new();
}
