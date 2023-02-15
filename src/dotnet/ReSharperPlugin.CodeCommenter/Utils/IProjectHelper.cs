using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Modules;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface IProjectHelper
{
    public IList<IPsiModule> GetPsiModules(IProject project);
}
