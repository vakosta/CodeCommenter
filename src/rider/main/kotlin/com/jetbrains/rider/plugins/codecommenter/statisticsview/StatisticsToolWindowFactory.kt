package com.jetbrains.rider.plugins.codecommenter.statisticsview

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.framework.impl.RdTask
import com.jetbrains.rd.ide.model.StatisticsToolWindowModel
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rider.plugins.codecommenter.model.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.statisticsview.views.StatisticsTreeTableView
import com.jetbrains.rider.plugins.codecommenter.utils.toTreeNode

class StatisticsToolWindowFactory : ToolWindowFactory {
    private val treeTableModel: StatisticsTreeTableModel = StatisticsTreeTableModel(
        StatisticsData("123", "1234", 0.5F, 0.6F)
            .apply {
                add(StatisticsData("333", "4444", 0.1F, 1F))
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
            val root = StatisticsData("", "", 0F, 0F)
            for (row in toolWindowContent.rows)
                root.add(row.toTreeNode())
            treeTableModel.actualRoot = root
            RdTask.fromResult(toolWindowContent)
        }
    }
}
