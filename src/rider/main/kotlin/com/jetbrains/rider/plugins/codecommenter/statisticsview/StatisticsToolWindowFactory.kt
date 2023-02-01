package com.jetbrains.rider.plugins.codecommenter.statisticsview

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.framework.impl.RdTask
import com.jetbrains.rd.ide.model.StatisticsToolWindowModel
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.statisticsview.views.StatisticsTreeTableView
import com.jetbrains.rider.plugins.codecommenter.utils.toTreeNode

class StatisticsToolWindowFactory : ToolWindowFactory {
    private val treeTableModel: StatisticsTreeTableModel = StatisticsTreeTableModel()
    private lateinit var treeTableView: StatisticsTreeTableView
    private lateinit var interactionModel: StatisticsToolWindowModel

    override fun createToolWindowContent(project: Project, toolWindow: ToolWindow) {
        initListeners(project)
        initContent(project, toolWindow)
        interactionModel.getContent.start(project.lifetime, Unit)
    }

    private fun initContent(project: Project, toolWindow: ToolWindow) {
        treeTableView = StatisticsTreeTableView(treeTableModel, project, interactionModel.getContent)
        val contentFactory = ContentFactory.getInstance()
        val content = contentFactory.createContent(treeTableView, null, false)
        toolWindow.contentManager.addContent(content)
    }

    private fun initListeners(project: Project) {
        interactionModel = StatisticsToolWindowModelHost.getInstance(project).interactionModel
        interactionModel.onContentUpdated.set { _, toolWindowContent ->
            val root = StatisticsData.getRoot()
            for (row in toolWindowContent.rows)
                root.add(row.toTreeNode())
            treeTableModel.setRoot(root)
            RdTask.fromResult(toolWindowContent)
        }
    }
}
