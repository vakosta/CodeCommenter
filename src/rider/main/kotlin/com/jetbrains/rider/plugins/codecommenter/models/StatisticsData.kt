package com.jetbrains.rider.plugins.codecommenter.models

import org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode

data class StatisticsData(
    var name: String,
    var docstring: String,
    var coverage: Float,
    var quality: Float,
) : DefaultMutableTreeTableNode() {

    override fun toString(): String {
        return name
    }
}
