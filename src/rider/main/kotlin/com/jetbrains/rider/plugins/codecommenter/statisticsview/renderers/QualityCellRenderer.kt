package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.ui.AnimatedIcon
import com.intellij.ui.components.JBLabel
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.models.isLoadingRecursive
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

        text = (value as Pair<StatisticsData, String>).second
        if (value.first.isLoadingRecursive() && icon == null)
            icon = AnimatedIcon.Default()
        else if (!value.first.isLoadingRecursive() && icon != null)
            icon = null

        return this
    }
}
