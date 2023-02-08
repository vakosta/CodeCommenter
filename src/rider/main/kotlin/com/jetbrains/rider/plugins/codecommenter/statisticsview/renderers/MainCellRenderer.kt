package com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers

import com.intellij.icons.AllIcons
import com.intellij.ui.AnimatedIcon
import com.intellij.ui.components.JBLabel
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.models.isLoadingRecursive
import java.awt.Component
import javax.swing.JTree
import javax.swing.SwingConstants
import javax.swing.tree.TreeCellRenderer

class MainCellRenderer : JBLabel(
    "",
    AnimatedIcon.Default(),
    SwingConstants.LEFT
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
        horizontalAlignment = SwingConstants.LEFT

        if ((value as StatisticsData).name.isNotBlank())
            text = value.name
        if (value.isLoadingRecursive())
            text = "$text (loading...)"

        icon = when (value.type) {
            StatisticsData.Type.Module -> AllIcons.Actions.ModuleDirectory
            StatisticsData.Type.File -> AllIcons.Actions.InlayRenameInNoCodeFiles
            StatisticsData.Type.Method -> AllIcons.Nodes.Method
            StatisticsData.Type.Root -> AllIcons.Actions.InlayRenameInNoCodeFiles
        }

        return this
    }
}
