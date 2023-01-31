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
 * #### Generated from [StatisticsToolWindowModel.kt:15]
 */
class StatisticsToolWindowModel private constructor(
    private val _getContent: RdCall<Unit, Unit>,
    private val _onContentUpdated: RdCall<RdToolWindowContent, RdToolWindowContent>
) : RdExtBase() {
    //companion
    
    companion object : ISerializersOwner {
        
        override fun registerSerializersCore(serializers: ISerializers)  {
            serializers.register(RdRow)
            serializers.register(RdToolWindowContent)
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
        
        
        const val serializationHash = 3062530557444078538L
        
    }
    override val serializersOwner: ISerializersOwner get() = StatisticsToolWindowModel
    override val serializationHash: Long get() = StatisticsToolWindowModel.serializationHash
    
    //fields
    val getContent: IRdCall<Unit, Unit> get() = _getContent
    val onContentUpdated: IRdEndpoint<RdToolWindowContent, RdToolWindowContent> get() = _onContentUpdated
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
        RdCall<RdToolWindowContent, RdToolWindowContent>(RdToolWindowContent, RdToolWindowContent)
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
 * #### Generated from [StatisticsToolWindowModel.kt:16]
 */
data class RdRow (
    val name: String,
    val docstring: String?,
    val coverage: Float,
    val quality: Float,
    val children: List<RdRow>
) : IPrintable {
    //companion
    
    companion object : IMarshaller<RdRow> {
        override val _type: KClass<RdRow> = RdRow::class
        
        @Suppress("UNCHECKED_CAST")
        override fun read(ctx: SerializationCtx, buffer: AbstractBuffer): RdRow  {
            val name = buffer.readString()
            val docstring = buffer.readNullable { buffer.readString() }
            val coverage = buffer.readFloat()
            val quality = buffer.readFloat()
            val children = buffer.readList { RdRow.read(ctx, buffer) }
            return RdRow(name, docstring, coverage, quality, children)
        }
        
        override fun write(ctx: SerializationCtx, buffer: AbstractBuffer, value: RdRow)  {
            buffer.writeString(value.name)
            buffer.writeNullable(value.docstring) { buffer.writeString(it) }
            buffer.writeFloat(value.coverage)
            buffer.writeFloat(value.quality)
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
        
        if (name != other.name) return false
        if (docstring != other.docstring) return false
        if (coverage != other.coverage) return false
        if (quality != other.quality) return false
        if (children != other.children) return false
        
        return true
    }
    //hash code trait
    override fun hashCode(): Int  {
        var __r = 0
        __r = __r*31 + name.hashCode()
        __r = __r*31 + if (docstring != null) docstring.hashCode() else 0
        __r = __r*31 + coverage.hashCode()
        __r = __r*31 + quality.hashCode()
        __r = __r*31 + children.hashCode()
        return __r
    }
    //pretty print
    override fun print(printer: PrettyPrinter)  {
        printer.println("RdRow (")
        printer.indent {
            print("name = "); name.print(printer); println()
            print("docstring = "); docstring.print(printer); println()
            print("coverage = "); coverage.print(printer); println()
            print("quality = "); quality.print(printer); println()
            print("children = "); children.print(printer); println()
        }
        printer.print(")")
    }
    //deepClone
    //contexts
}


/**
 * #### Generated from [StatisticsToolWindowModel.kt:24]
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
