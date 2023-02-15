using System.Threading.Tasks;
using JetBrains.Annotations;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperPlugin.CodeCommenter.Entities.CommentProvider;
using ReSharperPlugin.CodeCommenter.Entities.Network;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class CommentProvider
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly ICommentGenerationStrategy myCommentGenerationStrategy;
    [NotNull] private readonly IPsiHelper myPsiHelper;

    public CommentProvider(
        Lifetime lifetime,
        [NotNull] ICommentGenerationStrategy commentGenerationStrategy,
        [NotNull] IPsiHelper psiHelper)
    {
        myLifetime = lifetime;
        myCommentGenerationStrategy = commentGenerationStrategy;
        myPsiHelper = psiHelper;
    }

    public async Task<CommentBlocksContext> TryGenerateAndCreateCommentAsync([NotNull] ITreeNode declaration)
    {
        IDocCommentBlock oldCommentBlock = SharedImplUtil.GetDocCommentBlockNode(declaration);
        string methodCode = myPsiHelper.GetMethodCode(declaration, oldCommentBlock);
        GenerationResult comment = await myCommentGenerationStrategy.Generate(methodCode, myLifetime);
        return new CommentBlocksContext
        {
            OldDocCommentBlock = oldCommentBlock,
            NewDocCommentBlock = myPsiHelper.CreateDocCommentBlock(declaration, comment.Docstring),
            GenerationStatus = comment.Status
        };
    }
}
