using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.Rider.Model;

namespace ReSharperPlugin.CodeCommenter.Util;

public static class RdUtil
{
    public static RdRow ToRdRow(this IMethodDeclaration declaration)
    {
        var docstring = SharedImplUtil.GetDocCommentBlockNode(declaration)?.GetText();
        return new RdRow(declaration.DeclaredName, docstring, (float)0.1, (float)0.1, new List<RdRow>());
    }

    public static List<RdRow> ToRdRows(this IEnumerable<IMethodDeclaration> declarations)
    {
        return declarations
            .Select(declaration => declaration.ToRdRow())
            .ToList();
    }
}
