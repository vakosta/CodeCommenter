package model.rider

import com.jetbrains.rd.generator.nova.Ext
import com.jetbrains.rd.generator.nova.PredefinedType.bool
import com.jetbrains.rd.generator.nova.PredefinedType.float
import com.jetbrains.rd.generator.nova.PredefinedType.int
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
        field("name", string)
        field("docstring", string.nullable)
        field("coverage", float)
        field("quality", float)
        field("isLoading", bool)
        field("children", immutableList(this))
    }

    private val RdToolWindowContent = structdef {
        field("rows", immutableList(RdRow))
    }

    private val RdInsertNodeContext = structdef {
        field("child", RdRow)
        field("parent", RdRow)
        field("index", int)
    }

    private val RdChangeNodeContext = structdef {
        field("oldNode", RdRow)
        field("newNode", RdRow)
    }

    init {
        call("getContent", void, void)

        callback("onContentUpdated", RdToolWindowContent, void)
        callback("onNodeInserted", RdInsertNodeContext, void)
        callback("onNodeChanged", RdChangeNodeContext, void)
    }
}
