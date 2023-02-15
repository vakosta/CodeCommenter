using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IPsiSourceFileHelper
{
    public bool IsHidden(IPsiSourceFile psiSourceFile);

    public IReadOnlyList<IFile> GetPsiFiles(IPsiSourceFile sourceFile);
}
