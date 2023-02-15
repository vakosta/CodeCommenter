using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public class PsiSourceFileHelper
{
    public virtual bool IsHidden(IPsiSourceFile psiSourceFile)
    {
        return !psiSourceFile.ToProjectFile()!.Properties.IsHidden;
    }

    public virtual IReadOnlyList<IFile> GetPsiFiles(IPsiSourceFile sourceFile)
    {
        return sourceFile.GetPsiFiles<CSharpLanguage>();
    }
}
