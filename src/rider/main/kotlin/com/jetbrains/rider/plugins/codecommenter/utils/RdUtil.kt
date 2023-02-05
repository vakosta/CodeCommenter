package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import javax.swing.tree.MutableTreeNode

fun RdRow.toTreeNode(): MutableTreeNode {
    val root = StatisticsData(
        name = this.name,
        docstring = this.docstring ?: "",
        coverage = this.coverage,
        quality = this.quality,
        isLoading = this.isLoading,
    )

    for (child in this.children)
        root.add(child.toTreeNode())
    return root
}
