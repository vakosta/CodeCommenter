using JetBrains.Annotations;
using JetBrains.Core;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.Rider.Model;
using ReSharperPlugin.CodeCommenter.Common;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.StatisticsToolWindow;

[SolutionComponent]
public class StatisticsToolWindowManager
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly StatisticsToolWindowModel myStatisticsToolWindowModel;
    [NotNull] private readonly DocstringPlacesFinder myDocstringPlacesFinder;

    public StatisticsToolWindowManager(
        Lifetime lifetime,
        StatisticsToolWindowModel statisticsToolWindowModel,
        DocstringPlacesFinder docstringPlacesFinder)
    {
        myLifetime = lifetime;
        myStatisticsToolWindowModel = statisticsToolWindowModel;
        myDocstringPlacesFinder = docstringPlacesFinder;
        initHandlers();
    }

    private void initHandlers()
    {
        myStatisticsToolWindowModel.GetContent.Set((_, _) =>
        {
            var methods = myDocstringPlacesFinder.GetAllMethodsInProject();
            myStatisticsToolWindowModel.OnContentUpdated.Start(myLifetime, new RdToolWindowContent(methods.ToRdRows()));
            return RdTask<Unit>.Successful(Unit.Instance);
        });
    }
}
