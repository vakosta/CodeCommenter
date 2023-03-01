using System.Collections.Generic;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
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
        var psiSourceFileHelper = new Mock<PsiSourceFileHelper>().Object;
        var treeNodeHelper = new Mock<TreeNodeHelper>().Object;
        var docstringPlacesFinder = new DocstringPlacesFinder(
            new Lifetime(),
            solution,
            psiSourceFileHelper,
            treeNodeHelper);

        var methods = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(0, methods.Count);
    }

    [Test]
    public void OneMethodTest()
    {
        var projectHelper = new Mock<IProjectHelper>();
        var psiSourceFileHelper = new Mock<IPsiSourceFileHelper>();
        var treeNodeHelper = new Mock<ITreeNodeHelper>();
        var docstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), GetSolutionMock(
            new List<IProject>
            {
                GetProjectMock(
                    projectHelper,
                    "Module123",
                    new List<IProjectItem>
                    {
                        GetPsiProjectFile(
                            CSharpProjectFileType.Instance,
                            "File1",
                            false,
                            psiSourceFileHelper,
                            GetFile(
                                treeNodeHelper,
                                new List<ITreeNode>
                                {
                                    GetMethodDeclaration("Method1")
                                }))
                    })
            }), psiSourceFileHelper.Object, treeNodeHelper.Object);

        IList<ModuleDescriptor> modules = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(1, modules.Count);
        Assert.AreEqual("Module123", modules[0].Name);

        Assert.AreEqual(1, modules[0].Children.Count);
        Assert.AreEqual("File1", modules[0].Children[0].Name);

        Assert.AreEqual(1, modules[0].Children[0].Children.Count);

        Assert.AreEqual("Method1", modules[0].Children[0].Children[0].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[0]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[0]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[0].Children[0].Children[0]).Quality.Status);
    }

    [Test]
    public void TwoMethodsTest()
    {
        var projectHelper = new Mock<IProjectHelper>();
        var psiSourceFileHelper = new Mock<IPsiSourceFileHelper>();
        var treeNodeHelper = new Mock<ITreeNodeHelper>();
        var docstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), GetSolutionMock(
            new List<IProject>
            {
                GetProjectMock(
                    projectHelper,
                    "Module123",
                    new List<IProjectItem>
                    {
                        GetPsiProjectFile(
                            CSharpProjectFileType.Instance,
                            "File1",
                            false,
                            psiSourceFileHelper,
                            GetFile(
                                treeNodeHelper,
                                new List<ITreeNode>
                                {
                                    GetMethodDeclaration("Method1"),
                                    GetMethodDeclaration("Method2")
                                }))
                    })
            }), psiSourceFileHelper.Object, treeNodeHelper.Object);

        IList<ModuleDescriptor> modules = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(1, modules.Count);
        Assert.AreEqual("Module123", modules[0].Name);

        Assert.AreEqual(1, modules[0].Children.Count);
        Assert.AreEqual("File1", modules[0].Children[0].Name);

        Assert.AreEqual(2, modules[0].Children[0].Children.Count);

        Assert.AreEqual("Method1", modules[0].Children[0].Children[0].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[0]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[0]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[0].Children[0].Children[0]).Quality.Status);

        Assert.AreEqual("Method2", modules[0].Children[0].Children[1].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[1]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[1]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[0].Children[0].Children[1]).Quality.Status);
    }

    [Test]
    public void FiveMethodsInTwoChildrenInTwoModulesTest()
    {
        var projectHelper = new Mock<IProjectHelper>();
        var psiSourceFileHelper = new Mock<IPsiSourceFileHelper>();
        var treeNodeHelper = new Mock<ITreeNodeHelper>();
        var docstringPlacesFinder = new DocstringPlacesFinder(new Lifetime(), GetSolutionMock(
            new List<IProject>
            {
                GetProjectMock(
                    projectHelper,
                    "Module1",
                    new List<IProjectItem>
                    {
                        GetPsiProjectFile(
                            CSharpProjectFileType.Instance,
                            "File1",
                            false,
                            psiSourceFileHelper,
                            GetFile(
                                treeNodeHelper,
                                new List<ITreeNode>
                                {
                                    GetMethodDeclaration("Method1"),
                                    GetMethodDeclaration("Method2")
                                }))
                    }),
                GetProjectMock(
                    projectHelper,
                    "Module2",
                    new List<IProjectItem>
                    {
                        GetPsiProjectFile(
                            CSharpProjectFileType.Instance,
                            "File2",
                            false,
                            psiSourceFileHelper,
                            GetFile(
                                treeNodeHelper,
                                new List<ITreeNode>
                                {
                                    GetMethodDeclaration("Method3"),
                                    GetMethodDeclaration("Method4"),
                                    GetMethodDeclaration("Method5")
                                })),
                        GetPsiProjectFile(
                            CSharpProjectFileType.Instance,
                            "File3",
                            false,
                            psiSourceFileHelper,
                            GetFile(
                                treeNodeHelper,
                                new List<ITreeNode>
                                {
                                    GetMethodDeclaration("Method6"),
                                    GetMethodDeclaration("Method7")
                                }))
                    })
            }), psiSourceFileHelper.Object, treeNodeHelper.Object);

        IList<ModuleDescriptor> modules = docstringPlacesFinder.GetModuleDescriptors();
        Assert.AreEqual(2, modules.Count);
        Assert.AreEqual("Module1", modules[0].Name);
        Assert.AreEqual("Module2", modules[1].Name);

        Assert.AreEqual(1, modules[0].Children.Count);
        Assert.AreEqual(2, modules[1].Children.Count);
        Assert.AreEqual("File1", modules[0].Children[0].Name);
        Assert.AreEqual("File2", modules[1].Children[0].Name);

        Assert.AreEqual(2, modules[0].Children[0].Children.Count);

        Assert.AreEqual("Method1", modules[0].Children[0].Children[0].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[0]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[0]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[0].Children[0].Children[0]).Quality.Status);

        Assert.AreEqual("Method2", modules[0].Children[0].Children[1].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[1]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[0].Children[0].Children[1]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[0].Children[0].Children[1]).Quality.Status);

        Assert.AreEqual(3, modules[1].Children[0].Children.Count);

        Assert.AreEqual("Method3", modules[1].Children[0].Children[0].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[0].Children[0]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[0].Children[0]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[1].Children[0].Children[0]).Quality.Status);

        Assert.AreEqual("Method4", modules[1].Children[0].Children[1].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[0].Children[1]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[0].Children[1]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[1].Children[0].Children[1]).Quality.Status);

        Assert.AreEqual("Method5", modules[1].Children[0].Children[2].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[0].Children[2]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[0].Children[2]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[1].Children[0].Children[2]).Quality.Status);

        Assert.AreEqual(2, modules[1].Children[1].Children.Count);

        Assert.AreEqual("Method6", modules[1].Children[1].Children[0].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[1].Children[0]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[1].Children[0]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[1].Children[1].Children[0]).Quality.Status);

        Assert.AreEqual("Method7", modules[1].Children[1].Children[1].Name);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[1].Children[1]).Coverage);
        Assert.AreEqual(0, ((MethodDescriptor)modules[1].Children[1].Children[1]).Quality.Value);
        Assert.AreEqual(GenerationStatus.Loading,
            ((MethodDescriptor)modules[1].Children[1].Children[1]).Quality.Status);
    }

    private ISolution GetSolutionMock(List<IProject> projects)
    {
        var solution = new Mock<ISolution>();
        solution
            .Setup(s => s.GetAllProjects())
            .Returns(projects);
        return solution.Object;
    }

    private IProject GetProjectMock(Mock<IProjectHelper> projectHelper, string displayName,
        IList<IProjectItem> psiSourceChildren)
    {
        var project = new Mock<IProject>();
        project
            .Setup(p => p.ProjectFile)
            .Returns(new Mock<IProjectFile>().Object);
        project
            .Setup(pm => pm.Name)
            .Returns(displayName);
        project
            .Setup(pm => pm.GetSubItems())
            .Returns(psiSourceChildren);
        return project.Object;
    }

    private IProjectFile GetPsiProjectFile(ProjectFileType projectFileType, string name, bool isHidden,
        Mock<IPsiSourceFileHelper> psiSourceFileHelper, IFile file)
    {
        var psiSourceFile = new Mock<IProjectFile>();
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
            .Returns(file);

        return psiSourceFileObject;
    }

    private IFile GetFile(Mock<ITreeNodeHelper> treeNodeHelper, IList<ITreeNode> treeNodes)
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
