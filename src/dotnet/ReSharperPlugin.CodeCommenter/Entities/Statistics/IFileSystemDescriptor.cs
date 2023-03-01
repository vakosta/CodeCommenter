using System.Collections.Generic;
using JetBrains.Annotations;

namespace JetBrains.Rider.Model;

public interface IFileSystemDescriptor
{
    [NotNull] public string Name { get; }
    [CanBeNull] public IFileSystemDescriptor Parent { get; }
    [NotNull] public List<IFileSystemDescriptor> Children { get; }
}
