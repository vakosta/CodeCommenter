using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

[SolutionComponent]
public class PsiSourceFileHelper : IPsiSourceFileHelper
{
    public bool IsHidden(IPsiSourceFile psiSourceFile)
    {
        return psiSourceFile.ToProjectFile()!.Properties.IsHidden;
    }

    public IReadOnlyList<IFile> GetPsiFiles(IPsiSourceFile sourceFile)
    {
        return sourceFile.GetPsiFiles<CSharpLanguage>();
    }
}
