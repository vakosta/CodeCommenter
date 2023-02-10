using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.Diagnostics;
using JetBrains.DocumentManagers.Transactions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperPlugin.CodeCommenter.Common;

namespace ReSharperPlugin.CodeCommenter;

[ContextAction(
    Name = "GenerateComment",
    Description = "Generate comment by method code",
    Group = "C#",
    Disabled = false,
    Priority = 1,
    AllowedInNonUserFiles = false
)]
public class GenerateCommentContextAction : ContextActionBase
{
    private readonly IMethodDeclaration myDeclaration;
    [NotNull] private readonly HuggingFaceCommentGenerationStrategy myCommentGenerationStrategy;

    private IDocCommentBlock oldCommentBlock;
    private string comment;

    public GenerateCommentContextAction(LanguageIndependentContextActionDataProvider dataProvider)
    {
        myDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
        myCommentGenerationStrategy = dataProvider.Solution.GetComponent<HuggingFaceCommentGenerationStrategy>();
    }

    public override string Text => "Generate comment";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return myDeclaration != null && myDeclaration.IsValid();
    }

    protected override void ExecuteBeforePsiTransaction(ISolution solution, IProjectModelTransactionCookie cookie,
        IProgressIndicator progress)
    {
        oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(myDeclaration);
        var methodCode = oldCommentBlock != null
            ? myDeclaration.GetText().Replace(oldCommentBlock.GetText(), "")
            : myDeclaration.GetText();

        // TODO: Probably need to use ContextAction's lifetime.
        comment = myCommentGenerationStrategy.Generate(methodCode, solution.GetLifetime()).Result;
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        var newCommentBlock = CSharpElementFactory
            .GetInstance(myDeclaration)
            .CreateDocCommentBlock(comment);

        using (WriteLockCookie.Create())
        {
            if (oldCommentBlock != null)
                ModificationUtil.ReplaceChild(oldCommentBlock, newCommentBlock);
            else
                ModificationUtil.AddChildBefore(myDeclaration.FirstChild.NotNull(), newCommentBlock);
        }

        return null;
    }
}
