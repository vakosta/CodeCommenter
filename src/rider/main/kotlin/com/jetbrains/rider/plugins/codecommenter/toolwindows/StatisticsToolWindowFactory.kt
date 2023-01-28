package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.framework.impl.RdTask
import com.jetbrains.rd.ide.model.StatisticsToolWindowModel
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rider.plugins.codecommenter.utils.toTreeNode

class StatisticsToolWindowFactory : ToolWindowFactory {
    private val treeTableModel: StatisticsTreeTableModel = StatisticsTreeTableModel(
        DataNode("123", "1234")
            .apply {
                add(DataNode("333", "4444"))
            }
    )
    private val treeTableView: StatisticsTreeTableView = StatisticsTreeTableView(treeTableModel)
    private lateinit var interactionModel: StatisticsToolWindowModel

    override fun createToolWindowContent(project: Project, toolWindow: ToolWindow) {
        initListeners(project)
        initContent(toolWindow)
        interactionModel.getContent.start(project.lifetime, Unit)
    }

    private fun initContent(toolWindow: ToolWindow) {
        val contentFactory = ContentFactory.getInstance()
        val content = contentFactory.createContent(treeTableView, null, false)
        toolWindow.contentManager.addContent(content)
    }

    private fun initListeners(project: Project) {
        interactionModel = StatisticsToolWindowModelHost.getInstance(project).interactionModel
        interactionModel.onContentUpdated.set { _, toolWindowContent ->
            val root = DataNode("", "")
            for (row in toolWindowContent.rows)
                root.add(row.toTreeNode())
            treeTableModel.actualRoot = root
            RdTask.fromResult(toolWindowContent)
        }
    }
}
