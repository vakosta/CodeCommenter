package com.jetbrains.rider.plugins.codecommenter.statisticsview

import com.intellij.openapi.project.Project
import com.jetbrains.rd.ide.model.StatisticsToolWindowModel
import com.jetbrains.rd.ide.model.statisticsToolWindowModel
import com.jetbrains.rd.platform.util.idea.ProtocolSubscribedProjectComponent
import com.jetbrains.rider.protocol.protocol

class StatisticsToolWindowModelHost(project: Project) : ProtocolSubscribedProjectComponent(project) {
    val interactionModel: StatisticsToolWindowModel

    init {
        interactionModel = project.protocol.statisticsToolWindowModel
    }

    companion object {
        fun getInstance(project: Project): StatisticsToolWindowModelHost {
            return project.getComponent(StatisticsToolWindowModelHost::class.java)
        }
    }
}
