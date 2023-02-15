using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Modules;

namespace ReSharperPlugin.CodeCommenter.Util;

public class ProjectHelper
{
    public virtual IList<IPsiModule> GetPsiModules(IProject project)
    {
        return project.GetPsiModules();
    }
}
