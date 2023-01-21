package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rd.ui.bindable.views.utils.BeControlHost
import javax.swing.tree.DefaultMutableTreeNode

class StatisticsToolWindowFactory : ToolWindowFactory {
    override fun createToolWindowContent(project: Project, toolWindow: ToolWindow) {
        val component = BeControlHost(project).apply {
            bind(
                project.lifetime,
                StatisticsToolWindowModelHost.getInstance(project).interactionModel.toolWindowContent
            )
        }

        val root = DefaultMutableTreeNode()
        val dataNode1 = DataNode("13123")
        val dataNode2 = DataNode("131234")
        val dataNode3 = DataNode("131234")
        root.add(dataNode1)
        dataNode1.add(dataNode2)
        root.add(dataNode3)

        val myToolWindow = StatisticsTreeTableView(StatisticsTreeTableModel(root))

        val contentFactory = ContentFactory.getInstance()
        val content = contentFactory.createContent(myToolWindow, null, false)
        toolWindow.contentManager.addContent(content)
    }
}
