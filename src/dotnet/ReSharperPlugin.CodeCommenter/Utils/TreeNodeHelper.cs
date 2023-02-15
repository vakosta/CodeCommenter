using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.CodeCommenter.Util;

[SolutionComponent]
public class TreeNodeHelper : ITreeNodeHelper
{
    public IEnumerable<ITreeNode> Children(ITreeNode treeNode)
    {
        return treeNode.Children();
    }
}
