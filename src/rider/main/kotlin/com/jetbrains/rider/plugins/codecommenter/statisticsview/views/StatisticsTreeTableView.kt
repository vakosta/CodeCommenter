package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.openapi.project.Project
import com.intellij.openapi.ui.SimpleToolWindowPanel
import com.intellij.ui.components.JBTreeTable
import com.jetbrains.rd.framework.IRdCall
import com.jetbrains.rider.plugins.codecommenter.statisticsview.models.StatisticsTreeTableModel
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.CoverageColumnInfo
import com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos.QualityColumnInfo
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.CoverageCellRenderer
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.MainCellRenderer
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.QualityCellRenderer

class StatisticsTreeTableView(
    model: StatisticsTreeTableModel,
    project: Project,
    getContentCall: IRdCall<Unit, Unit>,
) : SimpleToolWindowPanel(true, true) {
    private val treeTable = JBTreeTable(model)

    init {
        treeTable.columnProportion = 0.55F

        initCellRenderers()
        toolbar = StatisticsToolbar(project, getContentCall)
        setContent(treeTable)
    }

    private fun initCellRenderers() {
        treeTable.tree
            .cellRenderer = MainCellRenderer()

        treeTable.table.getColumn(CoverageColumnInfo.IDENTIFIER)
            .cellRenderer = CoverageCellRenderer()

        treeTable.table.getColumn(QualityColumnInfo.IDENTIFIER)
            .cellRenderer = QualityCellRenderer()
    }
}
