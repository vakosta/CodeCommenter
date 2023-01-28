package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.ui.ColoredTreeCellRenderer
import javax.swing.JTree

class StatisticsCellRenderer : ColoredTreeCellRenderer() {
    override fun customizeCellRenderer(
        p0: JTree,
        p1: Any?,
        p2: Boolean,
        p3: Boolean,
        p4: Boolean,
        p5: Int,
        p6: Boolean
    ) {
        append("Kek")
    }
}
