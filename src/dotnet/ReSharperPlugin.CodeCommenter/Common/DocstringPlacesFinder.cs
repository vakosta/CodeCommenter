using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.PsiGen.Util;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model;
using ReSharperPlugin.CodeCommenter.Entities.Network;
using ReSharperPlugin.CodeCommenter.Util;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class DocstringPlacesFinder
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly ISolution mySolution;
    [NotNull] private readonly IPsiSourceFileHelper myPsiSourceFileHelper;
    [NotNull] private readonly ITreeNodeHelper myTreeNodeHelper;

    public DocstringPlacesFinder(
        Lifetime lifetime,
        [NotNull] ISolution solution,
        [NotNull] IPsiSourceFileHelper psiSourceFileHelper,
        [NotNull] ITreeNodeHelper treeNodeHelper)
    {
        myLifetime = lifetime;
        mySolution = solution;
        myPsiSourceFileHelper = psiSourceFileHelper;
        myTreeNodeHelper = treeNodeHelper;
    }

    public IList<ModuleDescriptor> GetModuleDescriptors()
    {
        var modules = new List<ModuleDescriptor>();
        if (!myLifetime.IsAlive) return modules;

        using (ReadLockCookie.Create())
        {
            var moduleDescriptors = mySolution.GetAllProjects()
                .Where(project => project.ProjectFile != null)
                .Select(GetModuleDescriptor);
            modules.AddRange(moduleDescriptors);
        }

        return modules;
    }

    private ModuleDescriptor GetModuleDescriptor(IProject module)
    {
        var moduleDescriptor = new ModuleDescriptor
        {
            Identifier = module.GetHashCode().ToString(),
            Name = module.Name
        };
        if (!myLifetime.IsAlive) return moduleDescriptor;

        foreach (var projectItem in module.GetSubItems())
        {
            IFileSystemDescriptor projectItemDescriptor = GetProjectItemDescriptor(projectItem, moduleDescriptor);
            if (projectItemDescriptor != null)
                moduleDescriptor.Children.Add(projectItemDescriptor);
        }

        return moduleDescriptor;
    }

    private IFileSystemDescriptor GetProjectItemDescriptor(IProjectItem projectItem, IFileSystemDescriptor parent)
    {
        if (projectItem is IProjectFile { LanguageType: CSharpProjectFileType } file
            && !myPsiSourceFileHelper.IsHidden(file))
            return GetFileDescriptor(file, parent);
        if (projectItem is IProjectFolder folder)
            return GetFolderDescriptor(folder, parent);
        return null;
    }

    private FileDescriptor GetFileDescriptor(IProjectFile sourceFile, IFileSystemDescriptor parent)
    {
        var fileDescriptor = new FileDescriptor
        {
            Identifier = sourceFile.GetHashCode().ToString(),
            Name = sourceFile.Name,
            Parent = parent
        };
        if (!myLifetime.IsAlive) return fileDescriptor;

        fileDescriptor.Children.AddAll(GetAllMethodsInFile(
            myPsiSourceFileHelper.GetPsiFiles(sourceFile),
            fileDescriptor));
        return fileDescriptor;
    }

    private FolderDescriptor GetFolderDescriptor(IProjectFolder folder, IFileSystemDescriptor parent)
    {
        var folderDescriptor = new FolderDescriptor
        {
            Name = folder.Name,
            Parent = parent
        };
        if (!myLifetime.IsAlive) return folderDescriptor;

        foreach (var projectItem in folder.GetSubItems())
        {
            IFileSystemDescriptor projectItemDescriptor = GetProjectItemDescriptor(projectItem, folderDescriptor);
            if (projectItemDescriptor != null)
                folderDescriptor.Children.Add(projectItemDescriptor);
        }

        return folderDescriptor;
    }

    private IEnumerable<MethodDescriptor> GetAllMethodsInFile(ITreeNode treeNode, IFileSystemDescriptor parent)
    {
        var methods = new List<MethodDescriptor>();
        if (!myLifetime.IsAlive) return methods;

        foreach (var child in myTreeNodeHelper.Children(treeNode))
        {
            if (child is IMethodDeclaration declaration)
            {
                var commentBlock = SharedImplUtil.GetDocCommentBlockNode(child)?.GetText() ?? "";
                methods.Add(CreateMethodDescriptor(declaration, commentBlock, parent));
            }

            methods.AddAll(GetAllMethodsInFile(child, parent));
        }

        return methods;
    }

    private static MethodDescriptor CreateMethodDescriptor(
        IMethodDeclaration declaration,
        string commentBlock,
        IFileSystemDescriptor parent)
    {
        return new MethodDescriptor
        {
            Declaration = declaration,

            Identifier = declaration.ToString().Replace("IMethodDeclaration", declaration.GetPsiModule().Name),

            Name = declaration.GetContainingNamespaceDeclaration() == null
                ? declaration.DeclaredName
                : declaration.ToString().Replace( // TODO: Rewrite this.
                    $"IMethodDeclaration: {declaration.GetContainingNamespaceDeclaration()!.DeclaredName}.",
                    ""),

            Docstring = commentBlock,

            Quality = new Quality { Value = 0, Status = GenerationStatus.Loading },

            Parent = parent
        };
    }
}
