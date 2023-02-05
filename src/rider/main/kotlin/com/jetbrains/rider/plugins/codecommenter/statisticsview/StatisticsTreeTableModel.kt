package com.jetbrains.rider.plugins.codecommenter.statisticsview

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.CoverageColumnInfo
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.QualityColumnInfo
import javax.swing.tree.TreeNode

class StatisticsTreeTableModel(
    root: StatisticsData = StatisticsData.getRoot(),
) : ListTreeTableModelOnColumns(
    root,
    columns.toTypedArray(),
) {
    override fun nodeChanged(node: TreeNode?) {
        super.nodeChanged(node)
        if (node?.parent != null && node.parent is StatisticsData) {
            // TODO: Add synchronization to make thread-safe.
            // TODO: Optimize recursion.
            (node.parent as StatisticsData).coverage = node.parent.children().asSequence()
                .filter { it is StatisticsData }
                .map { (it as StatisticsData).coverage }
                .toList().average().toFloat()
            (node.parent as StatisticsData).quality = node.parent.children().asSequence()
                .filter { it is StatisticsData }
                .map { (it as StatisticsData).quality }
                .toList().average().toFloat()
            nodeChanged(node.parent)
        }
    }

    fun findNodeByName(name: String, node: TreeNode = root): StatisticsData? {
        return if (node is StatisticsData && node.name == name)
            return node
        else {
            for (child in node.children()) {
                val calculatedChild = findNodeByName(name, child)
                if (calculatedChild != null)
                    return calculatedChild
            }
            null
        }
    }

    companion object {
        val columns = listOf(
            CoverageColumnInfo(),
            QualityColumnInfo(),
        )
    }
}
