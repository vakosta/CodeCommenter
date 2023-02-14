using System.Collections.Generic;
using System.Linq;
using JetBrains.Rider.Model;
using JetBrains.Util;
using ReSharperPlugin.CodeCommenter.Entities.Network;

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
            moduleDescriptor.Name,
            null,
            !files.IsEmpty() ? files.Average(file => file.Coverage) : 0,
            !files.IsEmpty()
                ? new RdQuality(files.Average(file => file.Quality.Value),
                    RdQualityStatus.RelativeToChildren)
                : new RdQuality(0, RdQualityStatus.Success),
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
            fileDescriptor.Name,
            null,
            !methods.IsEmpty() ? fileDescriptor.Methods.Average(method => method.Coverage) : 0,
            !methods.IsEmpty()
                ? new RdQuality(fileDescriptor.Methods.Average(method => method.Quality.Value),
                    RdQualityStatus.RelativeToChildren)
                : new RdQuality(0, RdQualityStatus.Success),
            methods);
        ;
    }

    public static RdRow ToRdRow(this MethodDescriptor methodDescriptor)
    {
        return new RdRow(
            RdRowType.Method,
            methodDescriptor.Identifier,
            methodDescriptor.Name,
            methodDescriptor.Docstring,
            methodDescriptor.Docstring.IsNotEmpty() ? 1 : 0,
            methodDescriptor.Quality.ToRdQuality(),
            new List<RdRow>());
    }

    public static RdQuality ToRdQuality(this Quality quality)
    {
        return new RdQuality(quality.Value, quality.Status.ToRdQualityStatus());
    }

    public static RdQualityStatus ToRdQualityStatus(this GenerationStatus generationStatus)
    {
        return generationStatus switch
        {
            GenerationStatus.Loading =>
                RdQualityStatus.Loading,
            GenerationStatus.Success =>
                RdQualityStatus.Success,
            GenerationStatus.Failed =>
                RdQualityStatus.Failed,
            GenerationStatus.Canceled =>
                RdQualityStatus.Canceled,
            _ =>
                RdQualityStatus.RelativeToChildren
        };
    }
}
