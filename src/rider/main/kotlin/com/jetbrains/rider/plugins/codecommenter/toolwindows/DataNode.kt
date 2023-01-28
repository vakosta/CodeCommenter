package com.jetbrains.rider.plugins.codecommenter.toolwindows

import org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode

data class DataNode(
    var name: String,
    var docstring: String,
) : DefaultMutableTreeTableNode()
