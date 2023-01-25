package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.framework.impl.RdTask
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rider.plugins.codecommenter.toTreeNode
import javax.swing.tree.DefaultMutableTreeNode

class StatisticsToolWindowFactory : ToolWindowFactory {
    private val treeTableModel = StatisticsTreeTableModel(DefaultMutableTreeNode())
    private val treeTableView = StatisticsTreeTableView(treeTableModel)

    override fun createToolWindowContent(project: Project, toolWindow: ToolWindow) {
        val interactionModel = StatisticsToolWindowModelHost.getInstance(project).interactionModel
        interactionModel.onContentUpdated.set { _, toolWindowContent ->
            val root = DefaultMutableTreeNode()
            for (row in toolWindowContent.rows)
                root.add(toolWindowContent.rows[0].toTreeNode())
            treeTableModel.setRoot(root)
            RdTask.fromResult(toolWindowContent)
        }

        val contentFactory = ContentFactory.getInstance()
        val content = contentFactory.createContent(treeTableView, null, false)
        toolWindow.contentManager.addContent(content)
        interactionModel.getContent.start(project.lifetime, Unit)
    }
}
