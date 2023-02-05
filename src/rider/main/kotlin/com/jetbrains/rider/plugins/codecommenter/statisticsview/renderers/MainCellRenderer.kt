package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.ui.AnimatedIcon
import com.intellij.ui.components.JBLabel
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import java.awt.Component
import javax.swing.JTree
import javax.swing.SwingConstants
import javax.swing.tree.TreeCellRenderer

class MainCellRenderer : JBLabel(
    "Loading...",
    AnimatedIcon.Default(),
    SwingConstants.LEFT,
), TreeCellRenderer {

    override fun getTreeCellRendererComponent(
        tree: JTree,
        value: Any,
        selected: Boolean,
        expanded: Boolean,
        leaf: Boolean,
        row: Int,
        hasFocus: Boolean,
    ): Component {
        assert(value is StatisticsData)
        if ((value as StatisticsData).name.isNotBlank())
            text = value.name
        if (!value.isLoading)
            icon = null
        return this
    }
}
