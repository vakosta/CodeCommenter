using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

[SolutionComponent]
public class PsiSourceFileHelper : IPsiSourceFileHelper
{
    public bool IsHidden(ProjectFileImpl psiSourceFile)
    {
        return psiSourceFile.Properties.IsHidden;
    }

    public IFile GetPsiFiles(ProjectFileImpl sourceFile)
    {
        return sourceFile.GetPrimaryPsiFile();
    }
}
