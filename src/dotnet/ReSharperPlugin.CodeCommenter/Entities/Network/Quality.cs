using JetBrains.Rider.Model;

namespace ReSharperPlugin.CodeCommenter.Entities.Network;

public class Quality
{
    public double Value { get; init; }
    public GenerationStatus Status { get; init; }
}
