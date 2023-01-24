@file:Suppress("EXPERIMENTAL_API_USAGE","EXPERIMENTAL_UNSIGNED_LITERALS","PackageDirectoryMismatch","UnusedImport","unused","LocalVariableName","CanBeVal","PropertyName","EnumEntryName","ClassName","ObjectPropertyName","UnnecessaryVariable","SpellCheckingInspection")
package com.jetbrains.rd.ide.model

import com.jetbrains.rd.framework.*
import com.jetbrains.rd.framework.base.*
import com.jetbrains.rd.framework.impl.*

import com.jetbrains.rd.util.lifetime.*
import com.jetbrains.rd.util.reactive.*
import com.jetbrains.rd.util.string.*
import com.jetbrains.rd.util.*
import kotlin.reflect.KClass
import kotlin.jvm.JvmStatic



/**
 * #### Generated from [StatisticsToolWindowModel.kt:13]
 */
class StatisticsToolWindowModel private constructor(
    private val _getContent: RdCall<Unit, Unit>,
    private val _onContentUpdated: RdCall<ToolWindowContent, ToolWindowContent>
) : RdExtBase() {
    //companion
    
    companion object : ISerializersOwner {
        
        override fun registerSerializersCore(serializers: ISerializers)  {
            serializers.register(Row)
            serializers.register(ToolWindowContent)
        }
        
        
        @JvmStatic
        @JvmName("internalCreateModel")
        @Deprecated("Use create instead", ReplaceWith("create(lifetime, protocol)"))
        internal fun createModel(lifetime: Lifetime, protocol: IProtocol): StatisticsToolWindowModel  {
            @Suppress("DEPRECATION")
            return create(lifetime, protocol)
        }
        
        @JvmStatic
        @Deprecated("Use protocol.statisticsToolWindowModel or revise the extension scope instead", ReplaceWith("protocol.statisticsToolWindowModel"))
        fun create(lifetime: Lifetime, protocol: IProtocol): StatisticsToolWindowModel  {
            IdeRoot.register(protocol.serializers)
            
            return StatisticsToolWindowModel().apply {
                identify(protocol.identity, RdId.Null.mix("StatisticsToolWindowModel"))
                bind(lifetime, protocol, "StatisticsToolWindowModel")
            }
        }
        
        
        const val serializationHash = 934911186581831628L
        
    }
    override val serializersOwner: ISerializersOwner get() = StatisticsToolWindowModel
    override val serializationHash: Long get() = StatisticsToolWindowModel.serializationHash
    
    //fields
    val getContent: IRdCall<Unit, Unit> get() = _getContent
    val onContentUpdated: IRdEndpoint<ToolWindowContent, ToolWindowContent> get() = _onContentUpdated
    //methods
    //initializer
    init {
        bindableChildren.add("getContent" to _getContent)
        bindableChildren.add("onContentUpdated" to _onContentUpdated)
    }
    
    //secondary constructor
    private constructor(
    ) : this(
        RdCall<Unit, Unit>(FrameworkMarshallers.Void, FrameworkMarshallers.Void),
        RdCall<ToolWindowContent, ToolWindowContent>(ToolWindowContent, ToolWindowContent)
    )
    
    //equals trait
    //hash code trait
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("StatisticsToolWindowModel (")
        printer.indent {
            print("getContent = "); _getContent.print(printer); println()
            print("onContentUpdated = "); _onContentUpdated.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    override fun deepClone(): StatisticsToolWindowModel   {
        return StatisticsToolWindowModel(
            _getContent.deepClonePolymorphic(),
            _onContentUpdated.deepClonePolymorphic()
        )
    }
    //contexts
}
val IProtocol.statisticsToolWindowModel get() = getOrCreateExtension(StatisticsToolWindowModel::class) { @Suppress("DEPRECATION") StatisticsToolWindowModel.create(lifetime, this) }



/**
 * #### Generated from [StatisticsToolWindowModel.kt:14]
 */
data class Row (
    val `property`: String,
    val docstring: String
) : IPrintable {
    //companion
    
    companion object : IMarshaller<Row> {
        override val _type: KClass<Row> = Row::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): Row  {
            val `property` = buffer.readString()
            val docstring = buffer.readString()
            return Row(`property`, docstring)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: Row)  {
            buffer.writeString(value.`property`)
            buffer.writeString(value.docstring)
        }
        
        
    }
    //fields
    //methods
    //initializer
    //secondary constructor
    //equals trait
    override fun equals(other: Any?): Boolean  {
        if (this === other) return true
        if (other == null || other::class != this::class) return false
        
        other as Row
        
        if (`property` != other.`property`) return false
        if (docstring != other.docstring) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + `property`.hashCode()
        __r = __r*31 + docstring.hashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("Row (")
        printer.indent {
            print("property = "); `property`.print(printer); println()
            print("docstring = "); docstring.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}


/**
 * #### Generated from [StatisticsToolWindowModel.kt:19]
 */
data class ToolWindowContent (
    val rows: List<Row>
) : IPrintable {
    //companion
    
    companion object : IMarshaller<ToolWindowContent> {
        override val _type: KClass<ToolWindowContent> = ToolWindowContent::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): ToolWindowContent  {
            val rows = buffer.readList { Row.read(ctx, buffer) }
            return ToolWindowContent(rows)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: ToolWindowContent)  {
            buffer.writeList(value.rows) { v -> Row.write(ctx, buffer, v) }
        }
        
        
    }
    //fields
    //methods
    //initializer
    //secondary constructor
    //equals trait
    override fun equals(other: Any?): Boolean  {
        if (this === other) return true
        if (other == null || other::class != this::class) return false
        
        other as ToolWindowContent
        
        if (rows != other.rows) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + rows.hashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("ToolWindowContent (")
        printer.indent {
            print("rows = "); rows.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}
