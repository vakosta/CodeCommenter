using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Core;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.Rd.Tasks;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model;
using ReSharperPlugin.CodeCommenter.Common;
using ReSharperPlugin.CodeCommenter.Entities.Network;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.StatisticsToolWindow;

[SolutionComponent]
public class StatisticsToolWindowManager
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly StatisticsToolWindowModel myStatisticsToolWindowModel;
    [NotNull] private readonly DocstringPlacesFinder myDocstringPlacesFinder;
    [NotNull] private readonly CommentProvider myCommentProvider;

    public StatisticsToolWindowManager(
        Lifetime lifetime,
        StatisticsToolWindowModel statisticsToolWindowModel,
        DocstringPlacesFinder docstringPlacesFinder,
        CommentProvider commentProvider)
    {
        myLifetime = lifetime;
        myStatisticsToolWindowModel = statisticsToolWindowModel;
        myDocstringPlacesFinder = docstringPlacesFinder;
        myCommentProvider = commentProvider;
        InitHandlers();
    }

    private void InitHandlers()
    {
        myStatisticsToolWindowModel.GetContent.Set((_, _) =>
        {
            var modules = myDocstringPlacesFinder.GetModuleDescriptors();
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
        var declaration = method.Declaration;
        method.Quality = await CalculateQuality(declaration,
            SharedImplUtil.GetDocCommentBlockNode(declaration)?.GetText());
        var rdMethod = method.ToRdRow();
        myStatisticsToolWindowModel.OnNodeChanged.Start(myLifetime, new RdChangeNodeContext(rdMethod));
    }

    private async Task<Quality> CalculateQuality([NotNull] IMethodDeclaration declaration,
        [NotNull] string commentBlock)
    {
        var generate = await myCommentProvider.TryGenerateAndCreateCommentAsync(declaration);
        return new Quality
        {
            Value = generate.GenerationStatus == GenerationStatus.Success
                ? commentBlock.CalculateSimilarity(generate.NewDocCommentBlock.GetText())
                : 0,
            Status = generate.GenerationStatus
        };
    }
}
