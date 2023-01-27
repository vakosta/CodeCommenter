using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.PsiGen.Util;
using JetBrains.ReSharper.Resources.Shell;

namespace ReSharperPlugin.CodeCommenter.Common;

[SolutionComponent]
public class DocstringPlacesFinder
{
    [NotNull] private readonly ISolution mySolution;

    public DocstringPlacesFinder(ISolution solution)
    {
        mySolution = solution;
    }

    public List<IMethodDeclaration> GetAllMethodsInProject()
    {
        var methods = new List<IMethodDeclaration>();

        using (ReadLockCookie.Create())
        {
            foreach (var module in mySolution.GetPsiServices().Modules.GetModules())
            foreach (var sourceFile in module.SourceFiles)
            foreach (var file in sourceFile.GetPsiFiles<CSharpLanguage>())
            {
                methods.AddAll(GetAllMethodsInFile(file));
            }
        }

        return methods;
    }

    private static List<IMethodDeclaration> GetAllMethodsInFile(ITreeNode treeNode)
    {
        var methods = new List<IMethodDeclaration>();

        foreach (var child in treeNode.Children())
        {
            if (child is IMethodDeclaration declaration)
                methods.Add(declaration);
            methods.AddAll(GetAllMethodsInFile(child));
        }

        return methods;
    }
}
