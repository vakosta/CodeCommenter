package com.jetbrains.rider.plugins.codecommenter.model

import org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode

data class StatisticsData(
    var name: String,
    var docstring: String,
) : DefaultMutableTreeTableNode()
