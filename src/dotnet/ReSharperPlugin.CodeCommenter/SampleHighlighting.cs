using JetBrains.Diagnostics;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace ReSharperPlugin.CodeCommenter;

[RegisterConfigurableSeverity(
    "121",
    CompoundItemName: null,
    Group: HighlightingGroupIds.CodeSmell,
    Title: "123",
    Description: "123",
    DefaultSeverity: Severity.WARNING)]
[ConfigurableSeverityHighlighting(
    "121",
    CSharpLanguage.Name,
    OverlapResolve = OverlapResolveKind.ERROR,
    OverloadResolvePriority = 0,
    ToolTipFormatString = "123")]
public class SampleHighlighting : IHighlighting
{
    public ICSharpDeclaration Declaration { get; }

    public SampleHighlighting(ICSharpDeclaration declaration)
    {
        Declaration = declaration;
    }

    public bool IsValid()
    {
        return Declaration.IsValid();
    }

    public DocumentRange CalculateRange()
    {
        return Declaration.NameIdentifier.NotNull().GetHighlightingRange();
    }

    public string ToolTip => $"ReSharper SDK: {nameof(SampleHighlighting)}";
    public string ErrorStripeToolTip => $"ReSharper SDK: {nameof(SampleHighlighting)}.{nameof(ErrorStripeToolTip)}";
}