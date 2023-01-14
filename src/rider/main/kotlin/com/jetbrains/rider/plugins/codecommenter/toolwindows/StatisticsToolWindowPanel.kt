package com.jetbrains.rider.plugins.codecommenter.toolwindows

import com.jetbrains.rd.ide.model.BeStatisticsToolWindowPanel
import com.jetbrains.rd.ui.bindable.ViewBinder
import com.jetbrains.rd.util.lifetime.Lifetime
import javax.swing.JComponent

class StatisticsToolWindowPanel : ViewBinder<BeStatisticsToolWindowPanel> {
    override fun bind(viewModel: BeStatisticsToolWindowPanel, lifetime: Lifetime): JComponent {
        return StatisticsToolWindow()
    }
}
