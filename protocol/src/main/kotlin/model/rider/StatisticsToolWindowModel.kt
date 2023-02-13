package model.rider

import com.jetbrains.rd.generator.nova.Ext
import com.jetbrains.rd.generator.nova.PredefinedType.double
import com.jetbrains.rd.generator.nova.PredefinedType.string
import com.jetbrains.rd.generator.nova.PredefinedType.void
import com.jetbrains.rd.generator.nova.call
import com.jetbrains.rd.generator.nova.callback
import com.jetbrains.rd.generator.nova.field
import com.jetbrains.rd.generator.nova.immutableList
import com.jetbrains.rd.generator.nova.nullable
import com.jetbrains.rider.model.nova.ide.IdeRoot

@Suppress("unused")
object StatisticsToolWindowModel : Ext(IdeRoot) {
    private val RdRow = structdef {
        field("type", RdRowType)
        field("identifier", string)
        field("name", string)
        field("docstring", string.nullable)
        field("coverage", double)
        field("quality", RdQuality)
        field("loadingState", RdLoadingState)
        field("children", immutableList(this))
    }

    private val RdRowType = enum {
        +"Module"
        +"File"
        +"Method"
        +"Root"
    }

    private val RdQuality = structdef {
        field("value", double)
        field("status", RdQualityStatus)
    }

    private val RdQualityStatus = enum {
        +"Ok"
        +"Failed"
        +"Canceled"
    }

    private val RdLoadingState = enum {
        +"Loading"
        +"Loaded"
        +"RelativeToChildren"
    }

    private val RdToolWindowContent = structdef {
        field("rows", immutableList(RdRow))
    }

    private val RdChangeNodeContext = structdef {
        field("newNode", RdRow)
    }

    init {
        call("getContent", void, void)

        callback("onContentUpdated", RdToolWindowContent, void)
        callback("onNodeChanged", RdChangeNodeContext, void)
    }
}
