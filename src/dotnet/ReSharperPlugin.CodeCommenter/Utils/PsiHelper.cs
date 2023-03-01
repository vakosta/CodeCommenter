using JetBrains.Diagnostics;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

[SolutionComponent]
public class PsiHelper : IPsiHelper
{
    public string GetMethodCode(ITreeNode declaration, IDocCommentBlock oldCommentBlock)
    {
        return oldCommentBlock != null
            ? declaration.GetText().Replace(oldCommentBlock.GetText(), "")
            : declaration.GetText();
    }

    public IDocCommentBlock CreateDocCommentBlock(ITreeNode declaration, string comment)
    {
        return CSharpElementFactory
            .GetInstance(declaration)
            .CreateDocCommentBlock(comment);
    }

    public void ModifyCommentBlockInPsi(
        ITreeNode declaration,
        IDocCommentBlock oldCommentBlock,
        IDocCommentBlock newCommentBlock)
    {
        if (oldCommentBlock != null)
            ModificationUtil.ReplaceChild(oldCommentBlock, newCommentBlock);
        else
            ModificationUtil.AddChildBefore(declaration.FirstChild.NotNull(), newCommentBlock);
    }
}
