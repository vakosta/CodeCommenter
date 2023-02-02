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

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class DocstringPlacesFinder
{
    [NotNull] private readonly ISolution mySolution;
    private readonly Lifetime myLifetime;
    [NotNull] private readonly ICommentGenerationStrategy myCommentGenerationStrategy;

    public DocstringPlacesFinder(
        ISolution solution,
        Lifetime lifetime,
        HuggingFaceCommentGenerationStrategy commentGenerationStrategy)
    {
        mySolution = solution;
        myLifetime = lifetime;
        myCommentGenerationStrategy = commentGenerationStrategy;
    }

    public IEnumerable<ModuleDescriptor> GetAllMethodsInProject()
    {
        var modules = new List<ModuleDescriptor>();
        if (!myLifetime.IsAlive) return modules;

        using (ReadLockCookie.Create())
        {
            modules.AddRange(mySolution
                .GetPsiServices()
                .Modules
                .GetModules()
                .Select(GetModuleDescriptor));
        }

        return modules;
    }

    private ModuleDescriptor GetModuleDescriptor(IPsiModule module)
    {
        var moduleDescriptor = new ModuleDescriptor { Name = module.Name };
        if (!myLifetime.IsAlive) return moduleDescriptor;

        foreach (var sourceFile in module.SourceFiles)
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
                var commentBlock = SharedImplUtil.GetDocCommentBlockNode(declaration)?.GetText() ?? "";
                var methodCode = declaration.GetText().Replace(commentBlock, "");
                methods.Add(new MethodDescriptor
                {
                    Declaration = declaration,
                    Name = declaration.DeclaredName,
                    Docstring = commentBlock,
                    Quality = (float)commentBlock.CalculateSimilarity(
                        myCommentGenerationStrategy.Generate(methodCode, myLifetime))
                });
            }

            methods.AddAll(GetAllMethodsInFile(child));
        }

        return methods;
    }
}
