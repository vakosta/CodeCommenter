using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public static class PsiUtil
{
    public static string GetMethodCode(ITreeNode declaration, IDocCommentBlock oldCommentBlock)
    {
        return oldCommentBlock != null
            ? declaration.GetText().Replace(oldCommentBlock.GetText(), "")
            : declaration.GetText();
    }

    public static IDocCommentBlock CreateDocCommentBlock(ITreeNode declaration, string comment)
    {
        return CSharpElementFactory
            .GetInstance(declaration)
            .CreateDocCommentBlock(comment);
    }

    public static void ModifyCommentBlockInPsi(ITreeNode declaration, IDocCommentBlock oldCommentBlock,
        IDocCommentBlock newCommentBlock)
    {
        if (oldCommentBlock != null)
            ModificationUtil.ReplaceChild(oldCommentBlock, newCommentBlock);
        else
            ModificationUtil.AddChildBefore(declaration.FirstChild.NotNull(), newCommentBlock);
    }
}
