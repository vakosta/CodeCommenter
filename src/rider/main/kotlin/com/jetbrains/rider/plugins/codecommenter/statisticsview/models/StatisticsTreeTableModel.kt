package com.jetbrains.rider.plugins.codecommenter.statisticsview.models

import com.intellij.ui.treeStructure.treetable.ListTreeTableModelOnColumns
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
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
        // TODO: Optimize recursion.
        super.nodeChanged(node)
        if (node?.parent != null && node.parent is StatisticsData) {
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

    fun findNodeByIdentifier(identifier: String, node: TreeNode = root): StatisticsData? {
        return if (node is StatisticsData && node.identifier == identifier)
            return node
        else {
            for (child in node.children()) {
                val calculatedChild = findNodeByIdentifier(identifier, child)
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
