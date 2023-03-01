using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IPsiSourceFileHelper
{
    public bool IsHidden(ProjectFileImpl psiSourceFile);

    public IFile GetPsiFiles(ProjectFileImpl sourceFile);
}
