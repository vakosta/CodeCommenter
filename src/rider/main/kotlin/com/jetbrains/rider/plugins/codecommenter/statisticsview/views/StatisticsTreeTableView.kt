package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.ui.components.JBTreeTable
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.StatisticsCellRenderer
import com.jetbrains.rider.plugins.codecommenter.statisticsview.StatisticsTreeTableModel

class StatisticsTreeTableView(
    model: StatisticsTreeTableModel,
) : JBTreeTable(model) {

    init {
        tree.cellRenderer = StatisticsCellRenderer()
        tree.isRootVisible = true
    }
}
