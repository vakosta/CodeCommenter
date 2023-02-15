using System.Collections.Generic;
using System.Linq;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Tree;
using Moq;
using NUnit.Framework;
using ReSharperPlugin.CodeCommenter.Common;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.Tests.test.src;

[TestFixture]
public class DocstringPlacesFinderTest
{
    [Test]
    public void ZeroMethodsTest()
    {
        var solution = GetSolutionMock(new List<IProject>());
        var projectHelper = new Mock<ProjectHelper>().Object;
        var psiSourceFileHelper = new Mock<PsiSourceFileHelper>().Object;
        var treeNodeHelper = new Mock<TreeNodeHelper>().Object;
        var docstringPlacesFinder = new DocstringPlacesFinder(
            new Lifetime(),
            solution,
            projectHelper,
            psiSourceFileHelper,
            treeNodeHelper);

        var methods = docstringPlacesFinder.GetAllMethodsInProject();
        Assert.AreEqual(0, methods.Count());
    }

    [Test]
    public void OneMethodTest()
    {
        var projectHelper = new Mock<ProjectHelper>();
        var psiSourceFileHelper = new Mock<PsiSourceFileHelper>();
        var treeNodeHelper = new Mock<TreeNodeHelper>();
        var docstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), GetSolutionMock(
            new List<IProject>
            {
                GetProjectMock(
                    new List<IPsiModule>
                    {
                        GetPsiModuleMock(
                            "Module123",
                            new List<IPsiSourceFile>
                            {
                                GetPsiProjectFile(
                                    CSharpProjectFileType.Instance,
                                    false,
                                    psiSourceFileHelper,
                                    new List<IFile>
                                    {
                                        GetFile(
                                            treeNodeHelper,
                                            new List<ITreeNode>
                                            {
                                                GetMethodDeclaration()
                                            })
                                    })
                            })
                    },
                    projectHelper)
            }), projectHelper.Object, psiSourceFileHelper.Object, treeNodeHelper.Object);

        var methods = docstringPlacesFinder.GetAllMethodsInProject();
        Assert.AreEqual(1, methods.Count());
    }

    private ISolution GetSolutionMock(List<IProject> projects)
    {
        var solution = new Mock<ISolution>();
        solution
            .Setup(s => s.GetAllProjects())
            .Returns(projects);
        return solution.Object;
    }

    private IProject GetProjectMock(IList<IPsiModule> psiModules, Mock<ProjectHelper> projectHelper)
    {
        var project = new Mock<IProject>();
        project
            .Setup(p => p.ProjectFile)
            .Returns(new Mock<IProjectFile>().Object);
        var projectObject = project.Object;

        projectHelper
            .Setup(ph => ph.GetPsiModules(projectObject))
            .Returns(psiModules);

        return projectObject;
    }

    private IPsiModule GetPsiModuleMock(string displayName, IList<IPsiSourceFile> psiSourceFiles)
    {
        var psiModule = new Mock<IPsiModule>();
        psiModule
            .Setup(pm => pm.DisplayName)
            .Returns(displayName);
        psiModule
            .Setup(pm => pm.SourceFiles)
            .Returns(psiSourceFiles);
        return psiModule.Object;
    }

    private IPsiProjectFile GetPsiProjectFile(ProjectFileType projectFileType, bool isHidden,
        Mock<PsiSourceFileHelper> psiSourceFileHelper, IReadOnlyList<IFile> files)
    {
        var psiSourceFile = new Mock<IPsiProjectFile>();
        psiSourceFile
            .Setup(psf => psf.LanguageType)
            .Returns(projectFileType);
        var psiSourceFileObject = psiSourceFile.Object;

        psiSourceFileHelper
            .Setup(psfh => psfh.IsHidden(psiSourceFileObject))
            .Returns(isHidden);
        psiSourceFileHelper
            .Setup(psfh => psfh.GetPsiFiles(psiSourceFileObject))
            .Returns(files);

        return psiSourceFileObject;
    }

    private IFile GetFile(Mock<TreeNodeHelper> treeNodeHelper, IList<ITreeNode> treeNodes)
    {
        var file = new Mock<IFile>();
        var fileObject = file.Object;

        treeNodeHelper
            .Setup(tnh => tnh.Children(fileObject))
            .Returns(treeNodes);

        return fileObject;
    }

    private IMethodDeclaration GetMethodDeclaration()
    {
        var methodDeclaration = new Mock<IMethodDeclaration>();
        return methodDeclaration.Object;
    }
}
