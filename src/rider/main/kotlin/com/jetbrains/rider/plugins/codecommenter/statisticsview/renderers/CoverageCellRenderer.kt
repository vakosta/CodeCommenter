package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.ui.components.JBLabel
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
import java.awt.Component
import javax.swing.JTable
import javax.swing.table.TableCellRenderer

class CoverageCellRenderer : JBLabel(), TableCellRenderer {
    override fun getTableCellRendererComponent(
        table: JTable,
        value: Any,
        isSelected: Boolean,
        hasFocus: Boolean,
        row: Int,
        column: Int,
    ): Component {
        assert(value is Pair<*, *>)
        text = (value as Pair<StatisticsData, String>).second
        return this
    }
}
