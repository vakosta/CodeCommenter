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
        [NotNull] StatisticsToolWindowModel statisticsToolWindowModel,
        [NotNull] DocstringPlacesFinder docstringPlacesFinder,
        [NotNull] CommentProvider commentProvider)
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
                UpdateRowQuality(module);

            return RdTask<Unit>.Successful(Unit.Instance);
        });
    }

    private void UpdateRowQuality(IFileSystemDescriptor descriptor)
    {
        if (descriptor is MethodDescriptor methodDescriptor)
            myLifetime.StartMainReadAsync(() => UpdateMethodQuality(methodDescriptor));
        else
            foreach (var child in descriptor.Children)
                UpdateRowQuality(child);
    }

    private async Task UpdateMethodQuality(MethodDescriptor methodDescriptor)
    {
        var declaration = methodDescriptor.Declaration;
        methodDescriptor.Quality = await CalculateQuality(declaration,
            SharedImplUtil.GetDocCommentBlockNode(declaration)?.GetText() ?? "");
        SendUpdatedRow(methodDescriptor);
    }

    private void SendUpdatedRow(IFileSystemDescriptor descriptor)
    {
        if (descriptor == null) return;
        var rdMethod = descriptor.ToRdRow();
        myStatisticsToolWindowModel.OnNodeChanged.Start(myLifetime, new RdChangeNodeContext(rdMethod));
        SendUpdatedRow(descriptor.Parent);
    }

    private async Task<Quality> CalculateQuality(IMethodDeclaration declaration, string commentBlock)
    {
        var generate = await myCommentProvider.TryGenerateAndCreateCommentAsync(declaration);
        return new Quality
        {
            Value = generate.GenerationStatus == GenerationStatus.Success
                ? Fastenshtein.Levenshtein.Distance(commentBlock, generate.NewDocCommentBlock.GetText())
                : 0,
            Status = generate.GenerationStatus
        };
    }
}
