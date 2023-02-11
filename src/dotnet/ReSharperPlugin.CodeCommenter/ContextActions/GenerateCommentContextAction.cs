using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.CSharp.Tree;
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
    [NotNull] private readonly CommentHandler myCommentHandler;

    public GenerateCommentContextAction(LanguageIndependentContextActionDataProvider dataProvider)
    {
        myDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
        myCommentHandler = dataProvider.Solution.GetComponent<CommentHandler>();
    }

    public override string Text => "Generate comment";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return myDeclaration != null && myDeclaration.IsValid();
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        myCommentHandler.GenerateComment(myDeclaration);
        return null;
    }
}
