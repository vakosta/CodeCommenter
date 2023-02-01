package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.openapi.actionSystem.ActionGroup
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.impl.ActionToolbarImpl
import com.intellij.openapi.project.Project
import com.jetbrains.rd.framework.IRdCall

class StatisticsToolbar(
    project: Project,
    getContentCall: IRdCall<Unit, Unit>,
) : ActionToolbarImpl(
    "StatisticsToolbar",
    object : ActionGroup() {
        override fun getChildren(actionEvent: AnActionEvent?): Array<AnAction> {
            return arrayOf(
                StatisticsRefreshButton(project, getContentCall)
            )
        }
    },
    false,
)
