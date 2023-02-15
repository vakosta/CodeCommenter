using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Modules;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.PsiGen.Util;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Rider.Model;
using ReSharperPlugin.CodeCommenter.Entities.Network;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class DocstringPlacesFinder
{
    private readonly Lifetime myLifetime;
    [NotNull] private readonly ISolution mySolution;

    public DocstringPlacesFinder(
        Lifetime lifetime,
        [NotNull] ISolution solution)
    {
        myLifetime = lifetime;
        mySolution = solution;
    }

    public IEnumerable<ModuleDescriptor> GetAllMethodsInProject()
    {
        var modules = new List<ModuleDescriptor>();
        if (!myLifetime.IsAlive) return modules;

        using (ReadLockCookie.Create())
        {
            var moduleDescriptors = mySolution.GetAllProjects()
                .Where(project => project.ProjectFile != null)
                .SelectMany(project => project.GetPsiModules())
                .Select(GetModuleDescriptor);
            modules.AddRange(moduleDescriptors);
        }

        return modules;
    }

    private ModuleDescriptor GetModuleDescriptor(IPsiModule module)
    {
        var moduleDescriptor = new ModuleDescriptor { Name = module.DisplayName };
        if (!myLifetime.IsAlive) return moduleDescriptor;

        foreach (var sourceFile in module.SourceFiles)
            if (sourceFile.LanguageType is CSharpProjectFileType && !sourceFile.ToProjectFile()!.Properties.IsHidden)
                moduleDescriptor.Files.Add(GetFileDescriptor(sourceFile));
        return moduleDescriptor;
    }

    private FileDescriptor GetFileDescriptor(IPsiSourceFile sourceFile)
    {
        var fileDescriptor = new FileDescriptor { Name = sourceFile.Name };
        if (!myLifetime.IsAlive) return fileDescriptor;

        foreach (var file in sourceFile.GetPsiFiles<CSharpLanguage>())
            fileDescriptor.Methods.AddAll(GetAllMethodsInFile(file));
        return fileDescriptor;
    }

    private IEnumerable<MethodDescriptor> GetAllMethodsInFile(ITreeNode treeNode)
    {
        var methods = new List<MethodDescriptor>();
        if (!myLifetime.IsAlive) return methods;

        foreach (var child in treeNode.Children())
        {
            if (child is IMethodDeclaration declaration)
            {
                var commentBlock = SharedImplUtil.GetDocCommentBlockNode(child)?.GetText() ?? "";
                methods.Add(CreateMethodDescriptor(declaration, commentBlock));
            }

            methods.AddAll(GetAllMethodsInFile(child));
        }

        return methods;
    }

    private static MethodDescriptor CreateMethodDescriptor(IMethodDeclaration declaration, string commentBlock)
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

            Quality = new Quality { Value = 0, Status = GenerationStatus.Loading }
        };
    }
}
