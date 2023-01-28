package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.ui.ColorUtil
import com.intellij.ui.DarculaColors
import com.intellij.ui.components.JBLabel
import java.awt.Component
import java.awt.Graphics
import javax.swing.JTable
import javax.swing.table.TableCellRenderer

class StatisticsCellRenderer : JBLabel(), TableCellRenderer {
    override fun getTableCellRendererComponent(
        table: JTable,
        value: Any,
        isSelected: Boolean,
        hasFocus: Boolean,
        row: Int,
        column: Int,
    ): Component {
        return this
    }

    override fun paintComponents(g: Graphics) {
        g.color = ColorUtil.withAlpha(DarculaColors.RED, 0.5)
        super.paintComponents(g)
    }
}
