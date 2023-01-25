package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.intellij.ui.treeStructure.treetable.TreeColumnInfo
import javax.swing.tree.DefaultMutableTreeNode

class StatisticsTreeTableModel(
    root: DefaultMutableTreeNode,
) : ListTreeTableModelOnColumns(root, columns.toTypedArray()) {
    override fun getValueAt(value: Any?, column: Int): Any {
        // TODO: Replace with renderers (TableCellRenderer)
        return (value as DataNode).docstring
    }

    companion object {
        val columns = listOf(
            TreeColumnInfo("Name"),
            TreeColumnInfo("Description"),
        )
    }
}
