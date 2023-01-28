package com.jetbrains.rider.plugins.codecommenter.toolwindows.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.toolwindows.DataNode

class NameColumnInfo : ColumnInfo<DataNode, String>("Name") {
    override fun valueOf(dataNode: DataNode): String {
        return dataNode.name
    }
}
