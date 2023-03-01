using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Modules;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IProjectHelper
{
    [NotNull]
    public IList<IPsiModule> GetPsiModules([NotNull] IProject project);
}
