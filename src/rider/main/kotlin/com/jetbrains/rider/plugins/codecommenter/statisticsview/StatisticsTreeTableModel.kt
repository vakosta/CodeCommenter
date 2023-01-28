package com.jetbrains.rider.plugins.codecommenter.statisticsview

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.DescriptionColumnInfo
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.NameColumnInfo
import org.jdesktop.swingx.treetable.TreeTableNode

class StatisticsTreeTableModel(
    var actualRoot: TreeTableNode,
) : ListTreeTableModelOnColumns(
    actualRoot,
    columns.toTypedArray(),
) {

    companion object {
        val columns = listOf(
            NameColumnInfo(),
            DescriptionColumnInfo(),
        )
    }
}
