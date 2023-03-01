using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Rider.Model;
using JetBrains.Util;
using ReSharperPlugin.CodeCommenter.Entities.Network;

namespace ReSharperPlugin.CodeCommenter.Util;

public static class RdUtil
{
    [NotNull]
    public static List<RdRow> ToRdRows(this IEnumerable<ModuleDescriptor> descriptors)
    {
        return descriptors
            .Select(descriptor => descriptor.ToRdRow())
            .ToList();
    }

    [CanBeNull]
    public static RdRow ToRdRow(this IFileSystemDescriptor descriptor)
    {
        return descriptor switch
        {
            ModuleDescriptor moduleDescriptor =>
                moduleDescriptor.ToRdRow(),
            FolderDescriptor folderDescriptor =>
                folderDescriptor.ToRdRow(),
            FileDescriptor fileDescriptor =>
                fileDescriptor.ToRdRow(),
            MethodDescriptor methodDescriptor =>
                methodDescriptor.ToRdRow(),
            _ => null
        };
    }

    [NotNull]
    public static RdRow ToRdRow(this ModuleDescriptor moduleDescriptor)
    {
        var children = moduleDescriptor.Children
            .Select(file => file.ToRdRow())
            .ToList();
        return new RdRow(
            RdRowType.Module,
            moduleDescriptor.Identifier,
            moduleDescriptor.Name,
            null,
            !children.IsEmpty() ? children.Average(file => file.Coverage) : 0,
            !children.IsEmpty()
                ? new RdQuality(children.Average(file => file.Quality.Value),
                    RdQualityStatus.RelativeToChildren)
                : new RdQuality(0, RdQualityStatus.Success),
            children);
    }

    [NotNull]
    public static RdRow ToRdRow(this FolderDescriptor folderDescriptor)
    {
        var children = folderDescriptor.Children
            .Select(file => file.ToRdRow())
            .ToList();
        return new RdRow(
            RdRowType.Folder,
            folderDescriptor.Name,
            folderDescriptor.Name,
            null,
            !children.IsEmpty() ? children.Average(file => file.Coverage) : 0,
            !children.IsEmpty()
                ? new RdQuality(children.Average(file => file.Quality.Value),
                    RdQualityStatus.RelativeToChildren)
                : new RdQuality(0, RdQualityStatus.Success),
            children);
    }

    [NotNull]
    public static RdRow ToRdRow(this FileDescriptor fileDescriptor)
    {
        var methods = fileDescriptor.Children
            .Select(method => method.ToRdRow())
            .ToList();
        return new RdRow(
            RdRowType.File,
            fileDescriptor.Identifier,
            fileDescriptor.Name,
            null,
            !methods.IsEmpty() ? methods.Average(method => method.Coverage) : 0,
            !methods.IsEmpty()
                ? new RdQuality(methods.Average(method => method.Quality.Value), RdQualityStatus.RelativeToChildren)
                : new RdQuality(0, RdQualityStatus.Success),
            methods);
        ;
    }

    [NotNull]
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

    [NotNull]
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
