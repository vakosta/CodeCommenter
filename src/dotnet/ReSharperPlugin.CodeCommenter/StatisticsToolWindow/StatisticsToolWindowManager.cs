using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Core;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
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
                myLifetime.StartMainReadAsync(() => UpdateRowQuality(method));

            return RdTask<Unit>.Successful(Unit.Instance);
        });
    }

    private async Task UpdateRowQuality(MethodDescriptor method)
    {
        method.Quality = await CalculateQuality(method.Declaration);
        method.LoadingState = LoadingState.Loaded;
        var rdMethod = method.ToRdRow();
        myStatisticsToolWindowModel.OnNodeChanged.Start(myLifetime, new RdChangeNodeContext(rdMethod));
    }

    private async Task<float> CalculateQuality([NotNull] ITreeNode method)
    {
        var commentBlock = SharedImplUtil.GetDocCommentBlockNode(method)?.GetText();
        var methodCode = method.GetText();
        if (commentBlock != null)
            methodCode = methodCode.Replace(commentBlock, "");
        return await CalculateQuality(commentBlock, methodCode);
    }

    private async Task<float> CalculateQuality([NotNull] string commentBlock, [NotNull] string methodCode)
    {
        var generate = await myCommentGenerationStrategy.Generate(methodCode, myLifetime);
        return generate != null
            ? (float)commentBlock.CalculateSimilarity(generate)
            : -1;
    }
}
