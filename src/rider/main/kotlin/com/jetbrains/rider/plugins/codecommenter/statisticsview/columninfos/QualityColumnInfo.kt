package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData

class QualityColumnInfo : ColumnInfo<StatisticsData, Float>("Quality") {
    override fun valueOf(statisticsData: StatisticsData): Float {
        return statisticsData.quality
    }
}
