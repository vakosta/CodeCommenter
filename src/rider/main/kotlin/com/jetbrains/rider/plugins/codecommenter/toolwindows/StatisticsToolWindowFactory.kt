package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindow
import com.intellij.openapi.wm.ToolWindowFactory
import com.intellij.ui.content.ContentFactory
import com.jetbrains.rd.platform.util.lifetime
import com.jetbrains.rd.ui.bindable.views.utils.BeControlHost

class StatisticsToolWindowFactory : ToolWindowFactory {
    override fun createToolWindowContent(project: Project, toolWindow: ToolWindow) {
        val component = BeControlHost(project).apply {
            bind(
                project.lifetime,
                StatisticsToolWindowModelHost.getInstance(project).interactionModel.toolWindowContent
            )
        }

        val myToolWindow = StatisticsToolWindow()
        val contentFactory = ContentFactory.getInstance()
        val content = contentFactory.createContent(myToolWindow, null, false)
        toolWindow.contentManager.addContent(content)
    }
}
