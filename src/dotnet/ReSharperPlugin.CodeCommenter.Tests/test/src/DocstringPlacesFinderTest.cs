using System.Collections.Generic;
using System.Linq;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Modules;
using Moq;
using NUnit.Framework;
using ReSharperPlugin.CodeCommenter.Common;

namespace ReSharperPlugin.CodeCommenter.Tests.test.src;

[TestFixture]
public class DocstringPlacesFinderTest
{
    private ISolution mySolution;
    private DocstringPlacesFinder myDocstringPlacesFinder;

    [SetUp]
    public void SetUp()
    {
        var solution = new Mock<ISolution>();
        solution
            .Setup(s => s.GetAllProjects())
            .Returns(new List<IProject>());
        mySolution = solution.Object;

        myDocstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), mySolution);
    }

    [Test]
    public void GoodTest()
    {
        var methods = myDocstringPlacesFinder.GetAllMethodsInProject();
        Assert.AreEqual(0, methods.Count());
    }

    [Test]
    public void BadTest()
    {
        var methods = myDocstringPlacesFinder.GetAllMethodsInProject();
        Assert.AreEqual(1, methods.Count());
    }

    private IProject GetProjectMock(IList<IPsiModule> psiModules)
    {
        var project = new Mock<IProject>();
        project
            .Setup(p => p.ProjectFile)
            .Returns(new Mock<IProjectFile>().Object);
        project
            .Setup(p => p.GetPsiModules())
            .Returns(psiModules);
        return project.Object;
    }
}
