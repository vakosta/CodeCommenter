package model.rider

import com.jetbrains.rd.generator.nova.Ext
import com.jetbrains.rd.generator.nova.PredefinedType.string
import com.jetbrains.rd.generator.nova.PredefinedType.void
import com.jetbrains.rd.generator.nova.call
import com.jetbrains.rd.generator.nova.callback
import com.jetbrains.rd.generator.nova.field
import com.jetbrains.rd.generator.nova.immutableList
import com.jetbrains.rider.model.nova.ide.IdeRoot

@Suppress("unused")
object StatisticsToolWindowModel : Ext(IdeRoot) {
    private val Row = structdef {
        field("property", string)
        field("docstring", string)
    }

    private val ToolWindowContent = structdef {
        field("rows", immutableList(Row))
    }

    init {
        call("getContent", void, void)
        callback("onContentUpdated", ToolWindowContent, ToolWindowContent)
    }
}
