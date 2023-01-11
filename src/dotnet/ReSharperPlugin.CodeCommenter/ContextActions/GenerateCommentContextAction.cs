using System;
using System.IO;
using System.Net;
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
    private const string URL = "https://google.com/";

    private readonly ICSharpDeclaration myDeclaration;

    public GenerateCommentContextAction([NotNull] LanguageIndependentContextActionDataProvider dataProvider)
    {
        myDeclaration = dataProvider.GetSelectedElement<IMethodDeclaration>();
    }

    public override string Text => "Generate comment";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return myDeclaration != null && myDeclaration.IsValid();
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        var response = Get(URL);

        using (WriteLockCookie.Create())
        {
            var oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(myDeclaration);
            var newCommentBlock = CSharpElementFactory
                .GetInstance(myDeclaration)
                .CreateDocCommentBlock("Hello\nWorld\n!");

            if (oldCommentBlock != null)
                ModificationUtil.ReplaceChild(oldCommentBlock, newCommentBlock);
            else
                ModificationUtil.AddChildBefore(myDeclaration.FirstChild.NotNull(), newCommentBlock);
        }

        return null;
    }

    private static string Get(string uri)
    {
        var request = (HttpWebRequest)WebRequest.Create(uri);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using var response = (HttpWebResponse)request.GetResponse();
        using var stream = response.GetResponseStream();
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}