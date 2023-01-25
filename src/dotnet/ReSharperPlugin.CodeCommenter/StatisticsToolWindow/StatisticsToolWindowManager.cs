using System.Collections.Generic;
using JetBrains.Core;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.Rider.Model;

namespace ReSharperPlugin.CodeCommenter.StatisticsToolWindow;

[SolutionComponent]
public class StatisticsToolWindowManager
{
    public StatisticsToolWindowManager(
        Lifetime lifetime,
        StatisticsToolWindowModel model)
    {
#if RESHARPER
#endif
        model.GetContent.Set((_, _) =>
        {
            model.OnContentUpdated.Start(lifetime, new RdToolWindowContent(getRows()));
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
