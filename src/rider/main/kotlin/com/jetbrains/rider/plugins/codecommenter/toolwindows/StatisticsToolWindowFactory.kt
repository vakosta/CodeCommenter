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

        val treeTableView = StatisticsTreeTableView(StatisticsTreeTableModel(getTreeNodes()))
        val contentFactory = ContentFactory.getInstance()
        val content = contentFactory.createContent(treeTableView, null, false)
        toolWindow.contentManager.addContent(content)
    }

    private fun getTreeNodes(): DefaultMutableTreeNode {
        val root = DefaultMutableTreeNode()

        val dataNode1 = DataNode("Name 1", "Description 1")
        val dataNode2 = DataNode("Name 2", "Description 2")
        val dataNode3 = DataNode("Name 3", "Description 3")

        root.add(dataNode1)
        dataNode1.add(dataNode2)
        root.add(dataNode3)

        return root
    }
}
