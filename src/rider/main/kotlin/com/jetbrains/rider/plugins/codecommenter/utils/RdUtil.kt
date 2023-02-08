package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rd.ide.model.RdRowType
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import javax.swing.tree.MutableTreeNode

fun RdRow.toTreeNode(): MutableTreeNode {
    val root = StatisticsData(
        type = this.type.toStatisticsDataType(),
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

fun RdRowType.toStatisticsDataType(): StatisticsData.Type {
    return when (this) {
        RdRowType.Module -> StatisticsData.Type.Module
        RdRowType.File -> StatisticsData.Type.File
        RdRowType.Method -> StatisticsData.Type.Method
        RdRowType.Root -> StatisticsData.Type.Root
    }
}
