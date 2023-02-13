using JetBrains.Annotations;

namespace ReSharperPlugin.CodeCommenter.Entities.Network;

public class GenerationResult
{
    [CanBeNull] public string Docstring { get; init; }
    public GenerationStatus Status { get; init; }
}
