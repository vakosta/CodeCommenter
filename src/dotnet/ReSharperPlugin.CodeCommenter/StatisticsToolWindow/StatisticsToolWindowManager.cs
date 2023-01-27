using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.Core;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.Rider.Model;
using ReSharperPlugin.CodeCommenter.Common;

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
            myDocstringPlacesFinder.GetAllMethodsInProject();
            myStatisticsToolWindowModel.OnContentUpdated.Start(myLifetime, new RdToolWindowContent(getRows()));
            return RdTask<Unit>.Successful(Unit.Instance);
        });
    }

    private List<RdRow> getRows()
    {
        return new List<RdRow>
        {
            new RdRow(
                "Name 1",
                "Some Docstring 1",
                new List<RdRow>
                {
                    new RdRow(
                        "Name 2",
                        "Some Docstring 3",
                        new List<RdRow>()
                    ),
                    new RdRow(
                        "Name 3",
                        "Some Docstring 3",
                        new List<RdRow>()
                    )
                }
            ),
            new RdRow(
                "Name 4",
                "Some Docstring 4",
                new List<RdRow>()
            )
        };
    }
}
