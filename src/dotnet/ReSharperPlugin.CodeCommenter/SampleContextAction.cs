using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSharperPlugin.CodeCommenter;

[ContextAction(
    Name = "ToLowerCase",
    Description = "Convert the text to lowercase",
    Group = "C#",
    Disabled = false,
    Priority = 1
)]
public class SampleContextAction : ContextActionBase
{
    private readonly ICSharpDeclaration myDeclaration;

    public SampleContextAction([NotNull] LanguageIndependentContextActionDataProvider dataProvider)
    {
        myDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
    }

    public override string Text => "123";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return myDeclaration != null && myDeclaration.IsValid();
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        var commentUpdater = solution.GetComponent<CommentUpdater>();
        commentUpdater.UpdateComment(myDeclaration);
        return null;
    }
}