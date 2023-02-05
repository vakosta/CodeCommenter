package com.jetbrains.rider.plugins.codecommenter.models

import javax.swing.tree.DefaultMutableTreeNode

data class StatisticsData(
    var name: String,
    var docstring: String,
    var coverage: Float,
    var quality: Float,
    var isLoading: Boolean,
    var id: String = "",
) : DefaultMutableTreeNode() {
    override fun equals(other: Any?): Boolean = other is StatisticsData
            && EssentialData(this) == EssentialData(other)

    override fun hashCode(): Int = EssentialData(this).hashCode()

    companion object {
        fun getRoot(): StatisticsData = StatisticsData(
            name = "",
            docstring = "",
            coverage = 0F,
            quality = 0F,
            isLoading = false,
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
