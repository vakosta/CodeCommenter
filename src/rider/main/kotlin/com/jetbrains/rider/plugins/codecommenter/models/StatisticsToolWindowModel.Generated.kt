@file:Suppress("EXPERIMENTAL_API_USAGE","EXPERIMENTAL_UNSIGNED_LITERALS","PackageDirectoryMismatch","UnusedImport","unused","LocalVariableName","CanBeVal","PropertyName","EnumEntryName","ClassName","ObjectPropertyName","UnnecessaryVariable","SpellCheckingInspection")
package com.jetbrains.rd.ide.model

import com.jetbrains.rd.framework.*
import com.jetbrains.rd.framework.base.*
import com.jetbrains.rd.framework.impl.*

import com.jetbrains.rd.util.lifetime.*
import com.jetbrains.rd.util.reactive.*
import com.jetbrains.rd.util.string.*
import com.jetbrains.rd.util.*
import kotlin.time.Duration
import kotlin.reflect.KClass
import kotlin.jvm.JvmStatic



/**
 * #### Generated from [StatisticsToolWindowModel.kt:16]
 */
class StatisticsToolWindowModel private constructor(
    private val _getContent: RdCall<Unit, Unit>,
    private val _onContentUpdated: RdCall<RdToolWindowContent, Unit>,
    private val _onNodeChanged: RdCall<RdChangeNodeContext, Unit>
) : RdExtBase() {
    //companion
    
    companion object : ISerializersOwner {
        
        override fun registerSerializersCore(serializers: ISerializers)  {
            serializers.register(RdRow)
            serializers.register(RdRowType.marshaller)
            serializers.register(RdToolWindowContent)
            serializers.register(RdChangeNodeContext)
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
            
            return StatisticsToolWindowModel()
        }
        
        
        const val serializationHash = -9076123247796669028L
        
    }
    override val serializersOwner: ISerializersOwner get() = StatisticsToolWindowModel
    override val serializationHash: Long get() = StatisticsToolWindowModel.serializationHash
    
    //fields
    val getContent: IRdCall<Unit, Unit> get() = _getContent
    val onContentUpdated: IRdEndpoint<RdToolWindowContent, Unit> get() = _onContentUpdated
    val onNodeChanged: IRdEndpoint<RdChangeNodeContext, Unit> get() = _onNodeChanged
    //methods
    //initializer
    init {
        bindableChildren.add("getContent" to _getContent)
        bindableChildren.add("onContentUpdated" to _onContentUpdated)
        bindableChildren.add("onNodeChanged" to _onNodeChanged)
    }
    
    //secondary constructor
    private constructor(
    ) : this(
        RdCall<Unit, Unit>(FrameworkMarshallers.Void, FrameworkMarshallers.Void),
        RdCall<RdToolWindowContent, Unit>(RdToolWindowContent, FrameworkMarshallers.Void),
        RdCall<RdChangeNodeContext, Unit>(RdChangeNodeContext, FrameworkMarshallers.Void)
    )
    
    //equals trait
    //hash code trait
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("StatisticsToolWindowModel (")
        printer.indent {
            print("getContent = "); _getContent.print(printer); println()
            print("onContentUpdated = "); _onContentUpdated.print(printer); println()
            print("onNodeChanged = "); _onNodeChanged.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    override fun deepClone(): StatisticsToolWindowModel   {
        return StatisticsToolWindowModel(
            _getContent.deepClonePolymorphic(),
            _onContentUpdated.deepClonePolymorphic(),
            _onNodeChanged.deepClonePolymorphic()
        )
    }
    //contexts
}
val IProtocol.statisticsToolWindowModel get() = getOrCreateExtension(StatisticsToolWindowModel::class) { @Suppress("DEPRECATION") StatisticsToolWindowModel.create(lifetime, this) }



/**
 * #### Generated from [StatisticsToolWindowModel.kt:38]
 */
data class RdChangeNodeContext (
    val newNode: RdRow
) : IPrintable {
    //companion
    
    companion object : IMarshaller<RdChangeNodeContext> {
        override val _type: KClass<RdChangeNodeContext> = RdChangeNodeContext::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): RdChangeNodeContext  {
            val newNode = RdRow.read(ctx, buffer)
            return RdChangeNodeContext(newNode)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: RdChangeNodeContext)  {
            RdRow.write(ctx, buffer, value.newNode)
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
        
        other as RdChangeNodeContext
        
        if (newNode != other.newNode) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + newNode.hashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("RdChangeNodeContext (")
        printer.indent {
            print("newNode = "); newNode.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}


/**
 * #### Generated from [StatisticsToolWindowModel.kt:17]
 */
data class RdRow (
    val type: RdRowType,
    val name: String,
    val docstring: String?,
    val coverage: Float,
    val quality: Float,
    val isLoading: Boolean,
    val children: List<RdRow>
) : IPrintable {
    //companion
    
    companion object : IMarshaller<RdRow> {
        override val _type: KClass<RdRow> = RdRow::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): RdRow  {
            val type = buffer.readEnum<RdRowType>()
            val name = buffer.readString()
            val docstring = buffer.readNullable { buffer.readString() }
            val coverage = buffer.readFloat()
            val quality = buffer.readFloat()
            val isLoading = buffer.readBool()
            val children = buffer.readList { RdRow.read(ctx, buffer) }
            return RdRow(type, name, docstring, coverage, quality, isLoading, children)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: RdRow)  {
            buffer.writeEnum(value.type)
            buffer.writeString(value.name)
            buffer.writeNullable(value.docstring) { buffer.writeString(it) }
            buffer.writeFloat(value.coverage)
            buffer.writeFloat(value.quality)
            buffer.writeBool(value.isLoading)
            buffer.writeList(value.children) { v -> RdRow.write(ctx, buffer, v) }
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
        
        other as RdRow
        
        if (type != other.type) return false
        if (name != other.name) return false
        if (docstring != other.docstring) return false
        if (coverage != other.coverage) return false
        if (quality != other.quality) return false
        if (isLoading != other.isLoading) return false
        if (children != other.children) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + type.hashCode()
        __r = __r*31 + name.hashCode()
        __r = __r*31 + if (docstring != null) docstring.hashCode() else 0
        __r = __r*31 + coverage.hashCode()
        __r = __r*31 + quality.hashCode()
        __r = __r*31 + isLoading.hashCode()
        __r = __r*31 + children.hashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("RdRow (")
        printer.indent {
            print("type = "); type.print(printer); println()
            print("name = "); name.print(printer); println()
            print("docstring = "); docstring.print(printer); println()
            print("coverage = "); coverage.print(printer); println()
            print("quality = "); quality.print(printer); println()
            print("isLoading = "); isLoading.print(printer); println()
            print("children = "); children.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}


/**
 * #### Generated from [StatisticsToolWindowModel.kt:27]
 */
enum class RdRowType {
    Module, 
    File, 
    Method, 
    Root;
    
    companion object {
        val marshaller = FrameworkMarshallers.enum<RdRowType>()
        
    }
}


/**
 * #### Generated from [StatisticsToolWindowModel.kt:34]
 */
data class RdToolWindowContent (
    val rows: List<RdRow>
) : IPrintable {
    //companion
    
    companion object : IMarshaller<RdToolWindowContent> {
        override val _type: KClass<RdToolWindowContent> = RdToolWindowContent::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): RdToolWindowContent  {
            val rows = buffer.readList { RdRow.read(ctx, buffer) }
            return RdToolWindowContent(rows)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: RdToolWindowContent)  {
            buffer.writeList(value.rows) { v -> RdRow.write(ctx, buffer, v) }
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
        
        other as RdToolWindowContent
        
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
        printer.println("RdToolWindowContent (")
        printer.indent {
            print("rows = "); rows.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}
