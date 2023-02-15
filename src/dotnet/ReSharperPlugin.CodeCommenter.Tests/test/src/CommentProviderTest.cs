using JetBrains.Lifetimes;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using Moq;
using NUnit.Framework;
using ReSharperPlugin.CodeCommenter.Common;
using ReSharperPlugin.CodeCommenter.Entities.Network;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.Tests.test.src;

[TestFixture]
public class CommentProviderTest
{
    [Test]
    public void SuccessGenerationStatusTest()
    {
        var commentGenerationStrategy = GetCommentGenerationStrategyMock("Test", GenerationStatus.Success);
        var psiHelper = GetPsiHelperMock();
        var commentProvider = new CommentProvider(new Lifetime(), commentGenerationStrategy, psiHelper);

        var commentBlocksContext = commentProvider.TryGenerateAndCreateCommentAsync(GetMethodDeclarationMock()).Result;
        Assert.AreEqual(GenerationStatus.Success, commentBlocksContext.GenerationStatus);
    }

    private ICommentGenerationStrategy GetCommentGenerationStrategyMock(string comment, GenerationStatus status)
    {
        var commentGenerationStrategy = new Mock<ICommentGenerationStrategy>();
        commentGenerationStrategy
            .Setup(cgs => cgs.Generate(It.IsAny<string>(), It.IsAny<Lifetime>()))
            .ReturnsAsync(new GenerationResult { Docstring = comment, Status = status });
        return commentGenerationStrategy.Object;
    }

    private IPsiHelper GetPsiHelperMock()
    {
        var psiHelper = new Mock<IPsiHelper>();
        psiHelper
            .Setup(ph => ph.CreateDocCommentBlock(It.IsAny<IMethodDeclaration>(), It.IsAny<string>()))
            .Returns(new Mock<IDocCommentBlock>().Object);
        return psiHelper.Object;
    }

    private IMethodDeclaration GetMethodDeclarationMock()
    {
        var methodDeclaration = new Mock<IMethodDeclaration>();
        methodDeclaration
            .Setup(md => md.GetPsiModule().Name)
            .Returns("");
        return methodDeclaration.Object;
    }
}
