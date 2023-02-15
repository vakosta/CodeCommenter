using JetBrains.ReSharper.Psi.Tree;
using ReSharperPlugin.CodeCommenter.Entities.Network;

namespace ReSharperPlugin.CodeCommenter.Entities.CommentProvider;

public class CommentBlocksContext
{
    public IDocCommentBlock OldDocCommentBlock { get; init; }
    public IDocCommentBlock NewDocCommentBlock { get; init; }
    public GenerationStatus GenerationStatus { get; init; }
}
