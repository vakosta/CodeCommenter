package com.jetbrains.rider.plugins.codecommenter.toolwindows

import java.awt.FlowLayout
import javax.swing.JButton
import javax.swing.JPanel

class StatisticsToolWindow : JPanel() {
    init {
        layout = FlowLayout()
        createUI()
    }

    private fun createUI() {
        val closeBtn = JButton("123")
        add(closeBtn)
    }
}
