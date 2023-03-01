using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface ITreeNodeHelper
{
    [NotNull]
    public IEnumerable<ITreeNode> Children([NotNull] ITreeNode treeNode);
}
