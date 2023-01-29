package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rider.plugins.codecommenter.model.StatisticsData
import org.jdesktop.swingx.treetable.MutableTreeTableNode

fun RdRow.toTreeNode(): MutableTreeTableNode {
    val root = StatisticsData(
        name = this.name,
        docstring = this.docstring ?: "",
        coverage = this.coverage,
        quality = this.quality,
    )
    for (child in this.children)
        root.add(child.toTreeNode())
    return root
}
