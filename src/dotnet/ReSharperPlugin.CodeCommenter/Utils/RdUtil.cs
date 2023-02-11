using System.Collections.Generic;
using System.Linq;
using JetBrains.Rider.Model;
using JetBrains.Util;

namespace ReSharperPlugin.CodeCommenter.Util;

public static class RdUtil
{
    public static List<RdRow> ToRdRows(this IEnumerable<ModuleDescriptor> descriptors)
    {
        return descriptors
            .Select(descriptor => descriptor.ToRdRow())
            .ToList();
    }

    public static RdRow ToRdRow(this ModuleDescriptor moduleDescriptor)
    {
        var files = moduleDescriptor.Files
            .Select(file => file.ToRdRow())
            .ToList();
        return new RdRow(
            RdRowType.Module,
            moduleDescriptor.Name,
            null,
            !files.IsEmpty() ? files.Average(file => file.Coverage) : 0,
            !files.IsEmpty() ? files.Average(file => file.Quality) : 0,
            RdLoadingState.RelativeToChildren,
            files);
    }

    public static RdRow ToRdRow(this FileDescriptor fileDescriptor)
    {
        var methods = fileDescriptor.Methods
            .Select(method => method.ToRdRow())
            .ToList();
        return new RdRow(
            RdRowType.File,
            fileDescriptor.Name,
            null,
            !methods.IsEmpty() ? fileDescriptor.Methods.Average(method => method.Coverage) : 0,
            !methods.IsEmpty() ? fileDescriptor.Methods.Average(method => method.Quality) : 0,
            RdLoadingState.RelativeToChildren,
            methods);
        ;
    }

    public static RdRow ToRdRow(this MethodDescriptor descriptor)
    {
        return new RdRow(
            RdRowType.Method,
            descriptor.Name,
            descriptor.Docstring,
            descriptor.Docstring.IsNotEmpty() ? 1 : 0,
            descriptor.Quality,
            descriptor.LoadingState.ToRdLoadingState(),
            new List<RdRow>());
    }

    public static RdLoadingState ToRdLoadingState(this LoadingState loadingState)
    {
        return loadingState switch
        {
            LoadingState.Loading =>
                RdLoadingState.Loading,
            LoadingState.Loaded =>
                RdLoadingState.Loaded,
            LoadingState.RelativeToChildren =>
                RdLoadingState.RelativeToChildren,
            _ =>
                RdLoadingState.Loaded
        };
    }
}
