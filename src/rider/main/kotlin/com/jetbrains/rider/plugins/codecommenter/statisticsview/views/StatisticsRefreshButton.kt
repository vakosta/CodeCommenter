package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.icons.AllIcons
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.project.Project
import com.jetbrains.rd.framework.IRdCall
import com.jetbrains.rd.platform.util.lifetime

class StatisticsRefreshButton(
    private val project: Project,
    private val getContentCall: IRdCall<Unit, Unit>,
) : AnAction(
    "Refresh Tree View",
    null,
    AllIcons.Actions.Refresh,
) {
    override fun actionPerformed(actionEvent: AnActionEvent) {
        getContentCall.start(project.lifetime, Unit)
    }
}
