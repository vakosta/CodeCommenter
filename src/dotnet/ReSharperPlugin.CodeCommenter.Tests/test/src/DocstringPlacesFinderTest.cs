using System.Collections.Generic;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Rider.Model;
using Moq;
using NUnit.Framework;
using ReSharperPlugin.CodeCommenter.Common;
using ReSharperPlugin.CodeCommenter.Entities.Network;
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

        var methods = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(0, methods.Count);
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
                    projectHelper,
                    new List<IPsiModule>
                    {
                        GetPsiModuleMock(
                            "Module123",
                            new List<IPsiSourceFile>
                            {
                                GetPsiProjectFile(
                                    CSharpProjectFileType.Instance,
                                    "File1",
                                    false,
                                    psiSourceFileHelper,
                                    new List<IFile>
                                    {
                                        GetFile(
                                            treeNodeHelper,
                                            new List<ITreeNode>
                                            {
                                                GetMethodDeclaration("Method1")
                                            })
                                    })
                            })
                    })
            }), projectHelper.Object, psiSourceFileHelper.Object, treeNodeHelper.Object);

        IList<ModuleDescriptor> modules = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(1, modules.Count);
        Assert.AreEqual("Module123", modules[0].Name);

        Assert.AreEqual(1, modules[0].Files.Count);
        Assert.AreEqual("File1", modules[0].Files[0].Name);

        Assert.AreEqual(1, modules[0].Files[0].Methods.Count);

        Assert.AreEqual("Method1", modules[0].Files[0].Methods[0].Name);
        Assert.AreEqual(0, modules[0].Files[0].Methods[0].Coverage);
        Assert.AreEqual(0, modules[0].Files[0].Methods[0].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[0].Files[0].Methods[0].Quality.Status);
    }

    [Test]
    public void TwoMethodsTest()
    {
        var projectHelper = new Mock<ProjectHelper>();
        var psiSourceFileHelper = new Mock<PsiSourceFileHelper>();
        var treeNodeHelper = new Mock<TreeNodeHelper>();
        var docstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), GetSolutionMock(
            new List<IProject>
            {
                GetProjectMock(
                    projectHelper,
                    new List<IPsiModule>
                    {
                        GetPsiModuleMock(
                            "Module123",
                            new List<IPsiSourceFile>
                            {
                                GetPsiProjectFile(
                                    CSharpProjectFileType.Instance,
                                    "File1",
                                    false,
                                    psiSourceFileHelper,
                                    new List<IFile>
                                    {
                                        GetFile(
                                            treeNodeHelper,
                                            new List<ITreeNode>
                                            {
                                                GetMethodDeclaration("Method1"),
                                                GetMethodDeclaration("Method2")
                                            })
                                    })
                            })
                    })
            }), projectHelper.Object, psiSourceFileHelper.Object, treeNodeHelper.Object);

        IList<ModuleDescriptor> modules = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(1, modules.Count);
        Assert.AreEqual("Module123", modules[0].Name);

        Assert.AreEqual(1, modules[0].Files.Count);
        Assert.AreEqual("File1", modules[0].Files[0].Name);

        Assert.AreEqual(2, modules[0].Files[0].Methods.Count);

        Assert.AreEqual("Method1", modules[0].Files[0].Methods[0].Name);
        Assert.AreEqual(0, modules[0].Files[0].Methods[0].Coverage);
        Assert.AreEqual(0, modules[0].Files[0].Methods[0].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[0].Files[0].Methods[0].Quality.Status);

        Assert.AreEqual("Method2", modules[0].Files[0].Methods[1].Name);
        Assert.AreEqual(0, modules[0].Files[0].Methods[1].Coverage);
        Assert.AreEqual(0, modules[0].Files[0].Methods[1].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[0].Files[0].Methods[1].Quality.Status);
    }

    [Test]
    public void FiveMethodsInTwoFilesInTwoModulesTest()
    {
        var projectHelper = new Mock<ProjectHelper>();
        var psiSourceFileHelper = new Mock<PsiSourceFileHelper>();
        var treeNodeHelper = new Mock<TreeNodeHelper>();
        var docstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), GetSolutionMock(
            new List<IProject>
            {
                GetProjectMock(
                    projectHelper,
                    new List<IPsiModule>
                    {
                        GetPsiModuleMock(
                            "Module1",
                            new List<IPsiSourceFile>
                            {
                                GetPsiProjectFile(
                                    CSharpProjectFileType.Instance,
                                    "File1",
                                    false,
                                    psiSourceFileHelper,
                                    new List<IFile>
                                    {
                                        GetFile(
                                            treeNodeHelper,
                                            new List<ITreeNode>
                                            {
                                                GetMethodDeclaration("Method1"),
                                                GetMethodDeclaration("Method2")
                                            })
                                    })
                            }),
                        GetPsiModuleMock(
                            "Module2",
                            new List<IPsiSourceFile>
                            {
                                GetPsiProjectFile(
                                    CSharpProjectFileType.Instance,
                                    "File2",
                                    false,
                                    psiSourceFileHelper,
                                    new List<IFile>
                                    {
                                        GetFile(
                                            treeNodeHelper,
                                            new List<ITreeNode>
                                            {
                                                GetMethodDeclaration("Method3"),
                                                GetMethodDeclaration("Method4"),
                                                GetMethodDeclaration("Method5")
                                            })
                                    }),
                                GetPsiProjectFile(
                                    CSharpProjectFileType.Instance,
                                    "File3",
                                    false,
                                    psiSourceFileHelper,
                                    new List<IFile>
                                    {
                                        GetFile(
                                            treeNodeHelper,
                                            new List<ITreeNode>
                                            {
                                                GetMethodDeclaration("Method6"),
                                                GetMethodDeclaration("Method7")
                                            })
                                    })
                            })
                    })
            }), projectHelper.Object, psiSourceFileHelper.Object, treeNodeHelper.Object);

        IList<ModuleDescriptor> modules = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(2, modules.Count);
        Assert.AreEqual("Module1", modules[0].Name);
        Assert.AreEqual("Module2", modules[1].Name);

        Assert.AreEqual(1, modules[0].Files.Count);
        Assert.AreEqual(2, modules[1].Files.Count);
        Assert.AreEqual("File1", modules[0].Files[0].Name);
        Assert.AreEqual("File2", modules[1].Files[0].Name);

        Assert.AreEqual(2, modules[0].Files[0].Methods.Count);

        Assert.AreEqual("Method1", modules[0].Files[0].Methods[0].Name);
        Assert.AreEqual(0, modules[0].Files[0].Methods[0].Coverage);
        Assert.AreEqual(0, modules[0].Files[0].Methods[0].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[0].Files[0].Methods[0].Quality.Status);

        Assert.AreEqual("Method2", modules[0].Files[0].Methods[1].Name);
        Assert.AreEqual(0, modules[0].Files[0].Methods[1].Coverage);
        Assert.AreEqual(0, modules[0].Files[0].Methods[1].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[0].Files[0].Methods[1].Quality.Status);

        Assert.AreEqual(3, modules[1].Files[0].Methods.Count);

        Assert.AreEqual("Method3", modules[1].Files[0].Methods[0].Name);
        Assert.AreEqual(0, modules[1].Files[0].Methods[0].Coverage);
        Assert.AreEqual(0, modules[1].Files[0].Methods[0].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[1].Files[0].Methods[0].Quality.Status);

        Assert.AreEqual("Method4", modules[1].Files[0].Methods[1].Name);
        Assert.AreEqual(0, modules[1].Files[0].Methods[1].Coverage);
        Assert.AreEqual(0, modules[1].Files[0].Methods[1].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[1].Files[0].Methods[1].Quality.Status);

        Assert.AreEqual("Method5", modules[1].Files[0].Methods[2].Name);
        Assert.AreEqual(0, modules[1].Files[0].Methods[2].Coverage);
        Assert.AreEqual(0, modules[1].Files[0].Methods[2].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[1].Files[0].Methods[2].Quality.Status);

        Assert.AreEqual(2, modules[1].Files[1].Methods.Count);

        Assert.AreEqual("Method6", modules[1].Files[1].Methods[0].Name);
        Assert.AreEqual(0, modules[1].Files[1].Methods[0].Coverage);
        Assert.AreEqual(0, modules[1].Files[1].Methods[0].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[1].Files[1].Methods[0].Quality.Status);

        Assert.AreEqual("Method7", modules[1].Files[1].Methods[1].Name);
        Assert.AreEqual(0, modules[1].Files[1].Methods[1].Coverage);
        Assert.AreEqual(0, modules[1].Files[1].Methods[1].Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading, modules[1].Files[1].Methods[1].Quality.Status);
    }

    private ISolution GetSolutionMock(List<IProject> projects)
    {
        var solution = new Mock<ISolution>();
        solution
            .Setup(s => s.GetAllProjects())
            .Returns(projects);
        return solution.Object;
    }

    private IProject GetProjectMock(Mock<ProjectHelper> projectHelper, IList<IPsiModule> psiModules)
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

    private IPsiProjectFile GetPsiProjectFile(ProjectFileType projectFileType, string name, bool isHidden,
        Mock<PsiSourceFileHelper> psiSourceFileHelper, IReadOnlyList<IFile> files)
    {
        var psiSourceFile = new Mock<IPsiProjectFile>();
        psiSourceFile
            .Setup(psf => psf.Name)
            .Returns(name);
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

    private IMethodDeclaration GetMethodDeclaration(string declaredName)
    {
        var methodDeclaration = new Mock<IMethodDeclaration>();
        methodDeclaration
            .Setup(md => md.GetPsiModule().Name)
            .Returns("");
        methodDeclaration
            .Setup(md => md.DeclaredName)
            .Returns(declaredName);
        return methodDeclaration.Object;
    }
}
