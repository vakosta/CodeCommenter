using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

public class TreeNodeHelper
{
    public virtual IEnumerable<ITreeNode> Children(ITreeNode treeNode)
    {
        return treeNode.Children();
    }
}
