package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.ui.components.JBTreeTable

class StatisticsTreeTableView(
    model: StatisticsTreeTableModel,
) : JBTreeTable(model) {

    init {
        tree.cellRenderer = StatisticsCellRenderer()
        tree.isRootVisible = true
    }
}
