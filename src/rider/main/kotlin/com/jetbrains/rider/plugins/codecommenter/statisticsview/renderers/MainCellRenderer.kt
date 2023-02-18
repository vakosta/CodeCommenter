package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.icons.AllIcons
import com.intellij.ui.components.JBLabel
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsDataType
import java.awt.Component
import javax.swing.JTree
import javax.swing.SwingConstants
import javax.swing.tree.TreeCellRenderer

class MainCellRenderer : JBLabel(), TreeCellRenderer {
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
        horizontalAlignment = SwingConstants.LEFT

        if ((value as StatisticsData).name.isNotBlank())
            text = value.name

        icon = when (value.type) {
            StatisticsDataType.Module -> AllIcons.Actions.ModuleDirectory
            StatisticsDataType.File -> AllIcons.Actions.InlayRenameInNoCodeFiles
            StatisticsDataType.Method -> AllIcons.Nodes.Method
            else -> null
        }

        return this
    }
}
