using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.Diagnostics;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;
using JetBrains.Util;
using ReSharperPlugin.CodeCommenter.Common;

namespace ReSharperPlugin.CodeCommenter;

[ContextAction(
    Name = "GenerateComment",
    Description = "Convert the text to lowercase",
    Group = "C#",
    Disabled = false,
    Priority = 1
)]
public class GenerateCommentContextAction : ContextActionBase
{
    [NotNull] private readonly IMethodDeclaration myDeclaration;
    [NotNull] private readonly HuggingFaceCommentGenerationStrategy myCommentGenerationStrategy;

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

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        using (WriteLockCookie.Create())
        {
            var oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(myDeclaration);
            var methodCode = oldCommentBlock != null
                ? myDeclaration.GetText().Replace(oldCommentBlock.GetText(), "")
                : myDeclaration.GetText();
            var comment = myCommentGenerationStrategy.Generate(methodCode);

            var newCommentBlock = CSharpElementFactory
                .GetInstance(myDeclaration)
                .CreateDocCommentBlock(comment);

            if (oldCommentBlock != null)
                ModificationUtil.ReplaceChild(oldCommentBlock, newCommentBlock);
            else
                ModificationUtil.AddChildBefore(myDeclaration.FirstChild.NotNull(), newCommentBlock);
        }

        return null;
    }
}
