package com.jetbrains.rider.plugins.codecommenter.statisticsview

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.QualityColumnInfo
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.CoverageColumnInfo
import org.jdesktop.swingx.treetable.TreeTableNode

class StatisticsTreeTableModel(
    root: TreeTableNode,
) : ListTreeTableModelOnColumns(
    root,
    columns.toTypedArray(),
) {

    companion object {
        val columns = listOf(
            CoverageColumnInfo(),
            QualityColumnInfo(),
        )
    }
}
