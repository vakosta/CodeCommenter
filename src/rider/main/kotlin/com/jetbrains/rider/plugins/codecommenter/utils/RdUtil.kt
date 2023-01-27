package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rider.plugins.codecommenter.toolwindows.DataNode
import javax.swing.tree.DefaultMutableTreeNode

fun RdRow.toTreeNode(): DefaultMutableTreeNode {
    val root = DataNode(
        name = this.name,
        docstring = this.docstring ?: ""
    )
    for (child in this.children)
        root.add(child.toTreeNode())
    return root
}
