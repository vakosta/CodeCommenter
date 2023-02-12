package com.jetbrains.rider.plugins.codecommenter.entities.statistics

import javax.swing.tree.DefaultMutableTreeNode

data class StatisticsData(
    var type: Type,
    var identifier: String,
    var name: String,
    var docstring: String,
    var coverage: Float,
    var quality: Float,
    var loadingState: LoadingState,
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

    enum class LoadingState {
        Loading,
        Loaded,
        RelativeToChildren,
    }

    companion object {
        fun getRoot(): StatisticsData = StatisticsData(
            type = Type.Root,
            identifier = "",
            name = "",
            docstring = "",
            coverage = 0F,
            quality = 0F,
            loadingState = LoadingState.Loaded,
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
    return loadingState == StatisticsData.LoadingState.Loading
            || !isLeaf && loadingState == StatisticsData.LoadingState.RelativeToChildren && children().asSequence()
        .filter { it is StatisticsData }
        .any { (it as StatisticsData).isLoadingRecursive() }
}
