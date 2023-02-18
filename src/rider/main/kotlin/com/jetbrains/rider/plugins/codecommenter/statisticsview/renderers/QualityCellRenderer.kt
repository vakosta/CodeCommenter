package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.icons.AllIcons
import com.intellij.ui.components.JBLabel
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.utils.isErrorRecursive
import com.jetbrains.rider.plugins.codecommenter.utils.isLoadingRecursive
import java.awt.Component
import javax.swing.JTable
import javax.swing.SwingConstants
import javax.swing.table.TableCellRenderer

class QualityCellRenderer : JBLabel(
    "",
    SwingConstants.LEFT,
), TableCellRenderer {

    override fun getTableCellRendererComponent(
        table: JTable,
        value: Any,
        isSelected: Boolean,
        hasFocus: Boolean,
        row: Int,
        column: Int,
    ): Component {
        assert(value is Pair<*, *>)

        text = if ((value as Pair<StatisticsData, String>).first.isLoadingRecursive())
            "${value.second}*"
        else
            value.second

        icon = if (!value.first.isErrorRecursive())
            null
        else
            AllIcons.Nodes.WarningIntroduction

        return this
    }
}
