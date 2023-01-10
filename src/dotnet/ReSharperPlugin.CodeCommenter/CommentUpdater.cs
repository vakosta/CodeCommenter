using JetBrains.Annotations;
using JetBrains.Diagnostics;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Resources.Shell;

namespace ReSharperPlugin.CodeCommenter;

[SolutionComponent]
public class CommentUpdater
{
    [NotNull] private readonly ISolution mySolution;

    public CommentUpdater([NotNull] ISolution solution)
    {
        mySolution = solution;
    }

    public void UpdateComment([NotNull] ICSharpDeclaration declaration)
    {
        using (WriteLockCookie.Create())
        {
            var oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(declaration);
            var newCommentBlock = CSharpElementFactory
                .GetInstance(declaration)
                .CreateDocCommentBlock("Hello\nWorld\n!");

            if (oldCommentBlock != null)
                ModificationUtil.ReplaceChild(oldCommentBlock, newCommentBlock);
            else
                ModificationUtil.AddChildBefore(declaration.FirstChild.NotNull(), newCommentBlock);
        }
    }
}