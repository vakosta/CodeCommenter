using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Application.Notifications;
using JetBrains.Application.Progress;
using JetBrains.Application.UI.Controls;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
using ReSharperPlugin.CodeCommenter.Entities.Network;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class CommentHandler
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly ISolution mySolution;
    [NotNull] private readonly IBackgroundProgressIndicatorManager myBackgroundProgressIndicatorManager;
    [NotNull] private readonly UserNotifications myUserNotifications;
    [NotNull] private readonly ICommentGenerationStrategy myCommentGenerationStrategy;

    public CommentHandler(
        Lifetime lifetime,
        [NotNull] ISolution solution,
        [NotNull] IBackgroundProgressIndicatorManager backgroundProgressIndicatorManager,
        [NotNull] UserNotifications userNotifications,
        [NotNull] HuggingFaceCommentGenerationStrategy commentGenerationStrategy)
    {
        myLifetime = lifetime;
        mySolution = solution;
        myBackgroundProgressIndicatorManager = backgroundProgressIndicatorManager;
        myUserNotifications = userNotifications;
        myCommentGenerationStrategy = commentGenerationStrategy;
    }

    public void GenerateComment(IMethodDeclaration declaration)
    {
        myLifetime.StartMainWriteAsync(async () =>
        {
            var indicatorLifetime = myLifetime.CreateNested();
            var progress = myBackgroundProgressIndicatorManager.CreateBackgroundProgress(indicatorLifetime,
                $"Docstring for {declaration.DeclaredName}");

            await TryGenerateAndCreateCommentAsync(declaration, progress);

            indicatorLifetime.Terminate();
        });
    }

    private async Task TryGenerateAndCreateCommentAsync(ITreeNode declaration, IProgressIndicator progress)
    {
        var oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(declaration);
        var methodCode = PsiUtil.GetMethodCode(declaration, oldCommentBlock);
        var comment = await myCommentGenerationStrategy.Generate(methodCode, myLifetime);

        if (comment.Status == GenerationStatus.Success)
            CreateCommentBlock(declaration, progress, comment, oldCommentBlock);
        else
            ErrorNotification();
    }

    private void CreateCommentBlock(ITreeNode declaration, IProgressIndicator progress, GenerationResult comment,
        IDocCommentBlock oldCommentBlock)
    {
        if (progress.IsCanceled)
            return;

        var newCommentBlock = PsiUtil.CreateDocCommentBlock(declaration, comment.Docstring);
        mySolution.GetPsiServices().Transactions.Execute("Add docstring",
            () => PsiUtil.ModifyCommentBlockInPsi(declaration, oldCommentBlock, newCommentBlock));
    }

    private void ErrorNotification()
    {
        myUserNotifications.CreateNotification(
            myLifetime,
            NotificationSeverity.WARNING,
            title: "Cannot create docstring",
            body: "Cannot create docstring for this method.",
            closeAfterExecution: true);
    }
}
