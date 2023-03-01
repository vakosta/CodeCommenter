using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IPsiSourceFileHelper
{
    public bool IsHidden(IProjectFile projectFile);

    public IFile GetPsiFiles(IProjectFile projectFile);
}
