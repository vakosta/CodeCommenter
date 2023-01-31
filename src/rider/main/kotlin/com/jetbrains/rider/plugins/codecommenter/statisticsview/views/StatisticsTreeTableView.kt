package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.openapi.ui.SimpleToolWindowPanel
import com.intellij.ui.components.JBTreeTable
import com.jetbrains.rider.plugins.codecommenter.statisticsview.StatisticsTreeTableModel

class StatisticsTreeTableView(
    model: StatisticsTreeTableModel,
) : SimpleToolWindowPanel(true, true) {
    private val treeTable = JBTreeTable(model)

    init {
        treeTable.columnProportion = 0.55F

        toolbar = StatisticsToolbar()
        setContent(treeTable)
    }
}
