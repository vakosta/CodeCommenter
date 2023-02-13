package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rd.ide.model.RdLoadingState
import com.jetbrains.rd.ide.model.RdQuality
import com.jetbrains.rd.ide.model.RdQualityStatus
import com.jetbrains.rd.ide.model.RdRow
import com.jetbrains.rd.ide.model.RdRowType
import com.jetbrains.rider.plugins.codecommenter.commons.Quality
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
import javax.swing.tree.MutableTreeNode

fun RdRow.toTreeNode(): MutableTreeNode {
    val root = StatisticsData(
        type = this.type.toStatisticsDataType(),
        identifier = this.identifier,
        name = this.name,
        docstring = this.docstring ?: "",
        coverage = this.coverage,
        quality = this.quality.toQuality(),
        loadingState = this.loadingState.toStatisticsLoadingState(),
    )

    for (child in this.children)
        root.add(child.toTreeNode())
    return root
}

fun RdQuality.toQuality(): Quality {
    return Quality(
        value = this.value,
        status = this.status.toQualityStatus(),
    )
}

fun RdRowType.toStatisticsDataType(): StatisticsData.Type =
        when (this) {
            RdRowType.Module ->
                StatisticsData.Type.Module

            RdRowType.File ->
                StatisticsData.Type.File

            RdRowType.Method ->
                StatisticsData.Type.Method

            RdRowType.Root ->
                StatisticsData.Type.Root
        }

fun RdLoadingState.toStatisticsLoadingState(): StatisticsData.LoadingState =
        when (this) {
            RdLoadingState.Loading ->
                StatisticsData.LoadingState.Loading

            RdLoadingState.Loaded ->
                StatisticsData.LoadingState.Loaded

            RdLoadingState.RelativeToChildren ->
                StatisticsData.LoadingState.RelativeToChildren
        }

fun RdQualityStatus.toQualityStatus(): Quality.Status =
        when (this) {
            RdQualityStatus.Ok ->
                Quality.Status.Ok

            RdQualityStatus.Failed ->
                Quality.Status.Failed

            RdQualityStatus.Canceled ->
                Quality.Status.Canceled
        }
