package com.jetbrains.rider.plugins.codecommenter.entities.statistics

import com.jetbrains.rider.plugins.codecommenter.commons.Quality
import javax.swing.tree.DefaultMutableTreeNode

data class StatisticsData(
    var type: Type,
    var identifier: String,
    var name: String,
    var docstring: String,
    var coverage: Double,
    var quality: Quality,
    var id: String = "",
) : DefaultMutableTreeNode() {
    override fun equals(other: Any?): Boolean = other is StatisticsData
            && EssentialData(this) == EssentialData(other)

    override fun hashCode(): Int = EssentialData(this).hashCode()

    enum class Type {
        Module,
        File,
        Method,
        Root,
    }

    companion object {
        fun getRoot(): StatisticsData = StatisticsData(
            type = Type.Root,
            identifier = "",
            name = "",
            docstring = "",
            coverage = 0.0,
            quality = Quality(0.0, Quality.Status.RelativeToChildren),
        )
    }
}

private data class EssentialData(
    val id: String,
    val name: String,
) {
    constructor(person: StatisticsData) : this(
        id = person.id,
        name = person.name,
    )
}

fun StatisticsData.isLoadingRecursive(): Boolean {
    return quality.status == Quality.Status.Loading
            || !isLeaf && quality.status == Quality.Status.RelativeToChildren && children().asSequence()
        .filter { it is StatisticsData }
        .any { (it as StatisticsData).isLoadingRecursive() }
}

fun StatisticsData.isErrorRecursive(): Boolean {
    return quality.status == Quality.Status.Failed || quality.status == Quality.Status.Canceled
            || !isLeaf && quality.status == Quality.Status.RelativeToChildren && children().asSequence()
        .filter { it is StatisticsData }
        .any { (it as StatisticsData).isErrorRecursive() }
}
