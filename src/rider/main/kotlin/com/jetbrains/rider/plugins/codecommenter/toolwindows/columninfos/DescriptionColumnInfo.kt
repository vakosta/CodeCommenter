package com.jetbrains.rider.plugins.codecommenter.toolwindows.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.toolwindows.DataNode

class DescriptionColumnInfo : ColumnInfo<DataNode, String>("Description") {
    override fun valueOf(dataNode: DataNode): String {
        return dataNode.docstring
    }
}
