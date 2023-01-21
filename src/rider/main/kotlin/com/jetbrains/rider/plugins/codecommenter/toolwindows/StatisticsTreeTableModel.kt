package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.intellij.ui.treeStructure.treetable.TreeColumnInfo
import javax.swing.tree.DefaultMutableTreeNode

class StatisticsTreeTableModel(
    root: DefaultMutableTreeNode,
) : ListTreeTableModelOnColumns(root, columns.toTypedArray()) {
    override fun getValueAt(p0: Any?, p1: Int): Any {
        return (p0 as DataNode).name
    }

    companion object {
        val columns = listOf(
            TreeColumnInfo("Name"),
        )
    }
}
