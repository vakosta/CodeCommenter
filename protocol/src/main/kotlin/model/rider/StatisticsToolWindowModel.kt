package model.rider

import com.jetbrains.rd.generator.nova.Ext
import com.jetbrains.rd.generator.nova.PredefinedType.bool
import com.jetbrains.rd.generator.nova.PredefinedType.string
import com.jetbrains.rd.generator.nova.PredefinedType.void
import com.jetbrains.rd.generator.nova.async
import com.jetbrains.rd.generator.nova.call
import com.jetbrains.rd.generator.nova.callback
import com.jetbrains.rd.generator.nova.field
import com.jetbrains.rd.generator.nova.nullable
import com.jetbrains.rd.generator.nova.property
import com.jetbrains.rd.generator.nova.signal
import com.jetbrains.rd.generator.nova.source
import com.jetbrains.rider.model.nova.ide.IdeRoot
import com.jetbrains.rider.model.nova.ide.UIAutomationInteractionModel.BeControl

object StatisticsToolWindowModel : Ext(IdeRoot) {
    init {
        property("toolWindowContent", BeControl)
        property("activateToolWindow", bool)

        @Suppress("LocalVariableName")
        val StatisticsToolWindowPanel = classdef("BeStatisticsToolWindowPanel") extends BeControl {
            field("url", string.nullable)
            field("html", string.nullable)

            signal("openDevTools", bool)
            signal("openUrl", string)

            call("getResource", string, string).async

            callback("sendMessage", string, void).async
            source("messageReceived", string).async
        }
    }
}
