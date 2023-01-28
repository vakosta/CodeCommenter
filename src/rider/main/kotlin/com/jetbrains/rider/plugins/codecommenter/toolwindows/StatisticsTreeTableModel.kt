package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.jetbrains.rider.plugins.codecommenter.toolwindows.columninfos.DescriptionColumnInfo
import com.jetbrains.rider.plugins.codecommenter.toolwindows.columninfos.NameColumnInfo
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
