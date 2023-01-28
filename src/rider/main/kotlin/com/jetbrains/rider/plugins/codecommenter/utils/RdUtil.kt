package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rider.plugins.codecommenter.toolwindows.DataNode
import org.jdesktop.swingx.treetable.MutableTreeTableNode

fun RdRow.toTreeNode(): MutableTreeTableNode {
    val root = DataNode(
        name = this.name,
        docstring = this.docstring ?: "",
    )
    for (child in this.children)
        root.add(child.toTreeNode())
    return root
}
