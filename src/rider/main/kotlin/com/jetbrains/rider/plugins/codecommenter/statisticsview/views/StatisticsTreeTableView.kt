package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.openapi.project.Project
import com.intellij.openapi.ui.SimpleToolWindowPanel
import com.intellij.ui.components.JBTreeTable
import com.jetbrains.rd.framework.IRdCall
import com.jetbrains.rider.plugins.codecommenter.statisticsview.StatisticsTreeTableModel

class StatisticsTreeTableView(
    model: StatisticsTreeTableModel,
    project: Project,
    getContentCall: IRdCall<Unit, Unit>,
) : SimpleToolWindowPanel(true, true) {
    private val treeTable = JBTreeTable(model)

    init {
        treeTable.columnProportion = 0.55F

        toolbar = StatisticsToolbar(project, getContentCall)
        setContent(treeTable)
    }
}
