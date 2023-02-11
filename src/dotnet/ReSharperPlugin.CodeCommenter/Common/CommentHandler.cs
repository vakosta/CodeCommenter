using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.Application.UI.Controls;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class CommentHandler
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly ISolution mySolution;
    [NotNull] private readonly IBackgroundProgressIndicatorManager myBackgroundProgressIndicatorManager;
    [NotNull] private readonly ICommentGenerationStrategy myCommentGenerationStrategy;

    public CommentHandler(
        Lifetime lifetime,
        [NotNull] ISolution solution,
        [NotNull] IBackgroundProgressIndicatorManager backgroundProgressIndicatorManager,
        [NotNull] HuggingFaceCommentGenerationStrategy commentGenerationStrategy)
    {
        myLifetime = lifetime;
        mySolution = solution;
        myBackgroundProgressIndicatorManager = backgroundProgressIndicatorManager;
        myCommentGenerationStrategy = commentGenerationStrategy;
    }

    public void GenerateComment(IMethodDeclaration declaration)
    {
        myLifetime.StartMainWriteAsync(async () =>
        {
            var indicatorLifetime = myLifetime.CreateNested();
            var progress = myBackgroundProgressIndicatorManager.CreateBackgroundProgress(indicatorLifetime,
                $"Docstring for {declaration.DeclaredName}");

            await GenerateCommentAsync(declaration, progress);

            indicatorLifetime.Terminate();
        });
    }

    private async Task GenerateCommentAsync(ITreeNode declaration, IProgressIndicator progress)
    {
        var oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(declaration);
        var methodCode = PsiUtil.GetMethodCode(declaration, oldCommentBlock);
        var comment = await myCommentGenerationStrategy.Generate(methodCode, myLifetime);
        var newCommentBlock = PsiUtil.CreateDocCommentBlock(declaration, comment);

        if (progress.IsCanceled)
            return;

        mySolution.GetPsiServices().Transactions.Execute("Add docstring",
            () => PsiUtil.ModifyCommentBlockInPsi(declaration, oldCommentBlock, newCommentBlock));
    }
}
