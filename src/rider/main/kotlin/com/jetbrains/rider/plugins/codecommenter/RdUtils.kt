package com.jetbrains.rider.plugins.codecommenter

import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rider.plugins.codecommenter.toolwindows.DataNode
import javax.swing.tree.DefaultMutableTreeNode

fun RdRow.toTreeNode(): DefaultMutableTreeNode {
    val root = DataNode(this.name, this.docstring)
    for (child in this.children)
        root.add(child.toTreeNode())
    return root
}
