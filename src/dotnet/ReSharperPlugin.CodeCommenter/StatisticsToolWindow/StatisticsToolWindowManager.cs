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
            model.OnContentUpdated.Start(lifetime, new ToolWindowContent(new List<Row>()));
            return RdTask<Unit>.Successful(Unit.Instance);
        });
    }
}
