package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.intellij.openapi.project.Project
import com.intellij.openapi.wm.ToolWindowManager
import com.jetbrains.rd.ide.model.StatisticsToolWindowModel
import com.jetbrains.rd.ide.model.statisticsToolWindowModel
import com.jetbrains.rd.platform.util.getComponent
import com.jetbrains.rd.platform.util.idea.ProtocolSubscribedProjectComponent
import com.jetbrains.rider.protocol.protocol

class StatisticsToolWindowModelHost(project: Project) : ProtocolSubscribedProjectComponent(project) {
    val interactionModel: StatisticsToolWindowModel

    init {
        interactionModel = project.protocol.statisticsToolWindowModel
        interactionModel.activateToolWindow.change.advise(projectComponentLifetime) {
            if (!it) return@advise
            val toolWindowManager = ToolWindowManager.getInstance(project)
            toolWindowManager.getToolWindow("CefToolWindow")!!.show()
        }
    }

    companion object {
        fun getInstance(project: Project): StatisticsToolWindowModelHost {
            return project.getComponent()
        }
    }
}
