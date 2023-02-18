package com.jetbrains.rider.plugins.codecommenter.entities.statistics

import com.jetbrains.rider.plugins.codecommenter.commons.Quality
import javax.swing.tree.DefaultMutableTreeNode

data class StatisticsData(
    var type: StatisticsDataType,
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

    companion object {
        fun getRoot(): StatisticsData = StatisticsData(
            type = StatisticsDataType.Root,
            identifier = "",
            name = "",
            docstring = "",
            coverage = 0.0,
            quality = Quality(0.0, Quality.Status.RelativeToChildren),
        )
    }
}

data class EssentialData(
    val id: String,
    val name: String,
) {
    constructor(person: StatisticsData) : this(
        id = person.id,
        name = person.name,
    )
}
