package com.jetbrains.rider.plugins.codecommenter.statisticsview.views

import com.intellij.openapi.actionSystem.ActionGroup
import com.intellij.openapi.actionSystem.AnAction
import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.impl.ActionToolbarImpl

class StatisticsToolbar : ActionToolbarImpl("StatisticsToolbar", object : ActionGroup() {
    override fun getChildren(p0: AnActionEvent?): Array<AnAction> {
        return arrayOf(
            StatisticsRefreshButton()
        )
    }
}, false)
