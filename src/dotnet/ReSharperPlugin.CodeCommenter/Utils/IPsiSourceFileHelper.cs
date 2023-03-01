using JetBrains.Annotations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IPsiSourceFileHelper
{
    public bool IsHidden([NotNull] IProjectFile projectFile);

    [CanBeNull]
    public IFile GetPsiFiles([NotNull] IProjectFile projectFile);
}
