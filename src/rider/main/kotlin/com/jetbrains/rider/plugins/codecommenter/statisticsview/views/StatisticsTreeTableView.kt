package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.ui.components.JBTreeTable
import com.intellij.ui.treeStructure.treetable.TreeTableModel
import com.jetbrains.rider.plugins.codecommenter.statisticsview.StatisticsTreeTableModel
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.StatisticsCellRenderer

class StatisticsTreeTableView(
    model: StatisticsTreeTableModel,
) : JBTreeTable(model) {

    init {
        setDefaultRenderer(TreeTableModel::class.java, StatisticsCellRenderer())
        columnProportion = 0.9F
        tree.isRootVisible = true
        updateUI()
    }
}
