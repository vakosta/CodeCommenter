using System.Collections.Generic;
using System.Linq;
using JetBrains.Rider.Model;
using JetBrains.Util;
using NuGet;

namespace ReSharperPlugin.CodeCommenter.Util;

public static class RdUtil
{
    public static List<RdRow> ToRdRows(this IEnumerable<ModuleDescriptor> descriptors)
    {
        return descriptors
            .Where(descriptor => !EnumerableExtensions.IsEmpty(descriptor.Files))
            .Select(descriptor => descriptor.ToRdRow())
            .ToList();
    }

    public static RdRow ToRdRow(this ModuleDescriptor moduleDescriptor)
    {
        var files = moduleDescriptor.Files
            .Where(file => !EnumerableExtensions.IsEmpty(file.Methods))
            .Select(file => file.ToRdRow())
            .ToList();
        return new RdRow(
            moduleDescriptor.Name,
            null,
            files.Average(file => file.Coverage),
            files.Average(file => file.Quality),
            true,
            files);
    }

    public static RdRow ToRdRow(this FileDescriptor fileDescriptor)
    {
        var methods = fileDescriptor.Methods
            .Select(method => method.ToRdRow())
            .ToList();
        return new RdRow(
            fileDescriptor.Name,
            null,
            fileDescriptor.Methods.Average(method => method.Coverage),
            fileDescriptor.Methods.Average(method => method.Quality),
            true,
            methods);
        ;
    }

    public static RdRow ToRdRow(this MethodDescriptor descriptor)
    {
        return new RdRow(
            descriptor.Name,
            descriptor.Docstring,
            descriptor.Docstring.IsNotEmpty() ? 1 : 0,
            descriptor.Quality,
            descriptor.IsLoading,
            new List<RdRow>());
    }
}
