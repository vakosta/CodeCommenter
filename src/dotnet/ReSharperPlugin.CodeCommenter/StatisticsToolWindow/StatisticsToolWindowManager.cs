using JetBrains.Annotations;
using JetBrains.Core;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
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
    [NotNull] private readonly ICommentGenerationStrategy myCommentGenerationStrategy;

    public StatisticsToolWindowManager(
        Lifetime lifetime,
        StatisticsToolWindowModel statisticsToolWindowModel,
        DocstringPlacesFinder docstringPlacesFinder,
        HuggingFaceCommentGenerationStrategy commentGenerationStrategy)
    {
        myLifetime = lifetime;
        myStatisticsToolWindowModel = statisticsToolWindowModel;
        myDocstringPlacesFinder = docstringPlacesFinder;
        myCommentGenerationStrategy = commentGenerationStrategy;
        InitHandlers();
    }

    private void InitHandlers()
    {
        myStatisticsToolWindowModel.GetContent.Set((_, _) =>
        {
            var modules = myDocstringPlacesFinder.GetAllMethodsInProject();
            var rdRows = modules.ToRdRows();
            myStatisticsToolWindowModel.OnContentUpdated.Start(myLifetime, new RdToolWindowContent(rdRows));

            foreach (var module in modules)
            foreach (var file in module.Files)
            foreach (var method in file.Methods)
                UpdateRowQuality(method);

            return RdTask<Unit>.Successful(Unit.Instance);
        });
    }

    private void UpdateRowQuality(MethodDescriptor method)
    {
        method.Quality = CalculateQuality(method.Declaration);
        method.IsLoading = false;
        var rdMethod = method.ToRdRow();
        myStatisticsToolWindowModel.OnNodeChanged.Start(
            myLifetime,
            new RdChangeNodeContext(rdMethod));
    }

    private float CalculateQuality([NotNull] ITreeNode method)
    {
        var commentBlock = SharedImplUtil.GetDocCommentBlockNode(method)?.GetText();
        var methodCode = method.GetText();
        if (commentBlock != null)
            methodCode = methodCode.Replace(commentBlock, "");
        return CalculateQuality(commentBlock, methodCode);
    }

    private float CalculateQuality([NotNull] string commentBlock, [NotNull] string methodCode)
    {
        return (float)commentBlock
            .CalculateSimilarity(myCommentGenerationStrategy.Generate(methodCode, myLifetime).Result);
    }
}
