using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

[SolutionComponent]
public class PsiSourceFileHelper : IPsiSourceFileHelper
{
    public bool IsHidden(IProjectFile projectFile)
    {
        return projectFile.Properties.IsHidden;
    }

    public IFile GetPsiFiles(IProjectFile projectFile)
    {
        return projectFile.GetPrimaryPsiFile();
    }
}
