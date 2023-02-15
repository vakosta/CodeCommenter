using JetBrains.Annotations;
using JetBrains.Application.Notifications;
using JetBrains.Application.UI.Controls;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Resources.Shell;
using ReSharperPlugin.CodeCommenter.Entities.CommentProvider;
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
    [NotNull] private readonly CommentProvider myCommentProvider;
    [NotNull] private readonly IPsiHelper myPsiHelper;

    public CommentHandler(
        Lifetime lifetime,
        [NotNull] ISolution solution,
        [NotNull] IBackgroundProgressIndicatorManager backgroundProgressIndicatorManager,
        [NotNull] UserNotifications userNotifications,
        [NotNull] CommentProvider commentProvider,
        [NotNull] IPsiHelper psiHelper)
    {
        myLifetime = lifetime;
        mySolution = solution;
        myBackgroundProgressIndicatorManager = backgroundProgressIndicatorManager;
        myUserNotifications = userNotifications;
        myCommentProvider = commentProvider;
        myPsiHelper = psiHelper;
    }

    public void GenerateComment(IMethodDeclaration declaration)
    {
        myLifetime.StartMainWriteAsync(async () =>
        {
            var indicatorLifetime = myLifetime.CreateNested();
            myBackgroundProgressIndicatorManager.CreateBackgroundProgress(indicatorLifetime,
                $"Docstring for {declaration.DeclaredName}");

            var commentBlocksContext = await myCommentProvider.TryGenerateAndCreateCommentAsync(declaration);
            if (commentBlocksContext.GenerationStatus == GenerationStatus.Success)
                ExecuteModifyTransaction(declaration, commentBlocksContext);
            else
                ErrorNotification();

            indicatorLifetime.Terminate();
        });
    }

    private void ExecuteModifyTransaction(IMethodDeclaration declaration, CommentBlocksContext commentBlocksContext)
    {
        mySolution.GetPsiServices().Transactions.Execute("Add docstring",
            () => myPsiHelper.ModifyCommentBlockInPsi(
                declaration,
                commentBlocksContext.OldDocCommentBlock,
                commentBlocksContext.NewDocCommentBlock));
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
