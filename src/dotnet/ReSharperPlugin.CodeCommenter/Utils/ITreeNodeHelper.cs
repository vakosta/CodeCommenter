using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public interface ITreeNodeHelper
{
    public IEnumerable<ITreeNode> Children(ITreeNode treeNode);
}
