using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IPsiHelper
{
    public string GetMethodCode(ITreeNode declaration, IDocCommentBlock oldCommentBlock);

    public IDocCommentBlock CreateDocCommentBlock(ITreeNode declaration, string comment);

    public void ModifyCommentBlockInPsi(ITreeNode declaration, IDocCommentBlock oldCommentBlock,
        IDocCommentBlock newCommentBlock);
}
