package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData

class QualityColumnInfo : ColumnInfo<StatisticsData, Pair<StatisticsData, String>>(IDENTIFIER) {
    override fun valueOf(statisticsData: StatisticsData): Pair<StatisticsData, String> {
        return statisticsData to "${(statisticsData.quality * 100).toInt()}%"
    }

    companion object {
        const val IDENTIFIER = "Quality"
    }
}
