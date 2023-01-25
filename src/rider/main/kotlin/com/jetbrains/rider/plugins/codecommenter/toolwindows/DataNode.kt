package com.jetbrains.rider.plugins.codecommenter.toolwindows

import javax.swing.tree.DefaultMutableTreeNode

data class DataNode(
    var name: String,
    var docstring: String,
) : DefaultMutableTreeNode() {
    override fun toString(): String {
        return name
    }
}
