using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IPsiHelper
{
    [NotNull]
    public string GetMethodCode([NotNull] ITreeNode declaration, [CanBeNull] IDocCommentBlock oldCommentBlock);

    [NotNull]
    public IDocCommentBlock CreateDocCommentBlock([NotNull] ITreeNode declaration, [NotNull] string comment);

    public void ModifyCommentBlockInPsi(
        [NotNull] ITreeNode declaration,
        [CanBeNull] IDocCommentBlock oldCommentBlock,
        [NotNull] IDocCommentBlock newCommentBlock);
}
