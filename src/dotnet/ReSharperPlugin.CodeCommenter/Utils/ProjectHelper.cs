using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Modules;

namespace ReSharperPlugin.CodeCommenter.Util;

[SolutionComponent]
public class ProjectHelper : IProjectHelper
{
    public IList<IPsiModule> GetPsiModules(IProject project)
    {
        return project.GetPsiModules();
    }
}
