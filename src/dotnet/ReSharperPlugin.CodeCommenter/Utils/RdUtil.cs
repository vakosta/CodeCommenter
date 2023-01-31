using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.Rider.Model;
using NuGet;

namespace ReSharperPlugin.CodeCommenter.Util;

public static class RdUtil
{
    public static RdRow ToRdRow(this IMethodDeclaration declaration)
    {
        var docstring = SharedImplUtil.GetDocCommentBlockNode(declaration)?.GetText();
        return new RdRow(
            declaration.DeclaredName,
            docstring,
            SharedImplUtil.GetDocCommentBlockNode(declaration) != null
                ? (float)1
                : 0,
            (float)0.1,
            new List<RdRow>());
    }

    public static RdRow ToRdRow(this FileDescriptor fileDescriptor)
    {
        var methods = fileDescriptor.Methods
            .Select(method => method.ToRdRow())
            .ToList();
        return new RdRow(
            fileDescriptor.Name,
            null,
            methods.Average(method => method.Coverage),
            methods.Average(method => method.Quality),
            methods);
    }

    public static RdRow ToRdRow(this ModuleDescriptor moduleDescriptor)
    {
        var files = moduleDescriptor.Files
            .Where(file => !file.Methods.IsEmpty())
            .Select(file => file.ToRdRow())
            .ToList();
        return new RdRow(
            moduleDescriptor.Name,
            null,
            files.Average(method => method.Coverage),
            files.Average(method => method.Coverage),
            files);
    }

    public static List<RdRow> ToRdRows(this IEnumerable<ModuleDescriptor> descriptors)
    {
        return descriptors
            .Where(descriptor => !descriptor.Files.IsEmpty())
            .Select(descriptor => descriptor.ToRdRow())
            .ToList();
    }
}
