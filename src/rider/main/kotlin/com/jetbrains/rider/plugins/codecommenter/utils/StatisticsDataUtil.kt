package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rider.plugins.codecommenter.commons.Quality
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData

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
