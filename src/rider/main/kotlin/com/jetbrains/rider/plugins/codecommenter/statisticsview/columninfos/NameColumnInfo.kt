package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.model.StatisticsData

class NameColumnInfo : ColumnInfo<StatisticsData, String>("Name") {
    override fun valueOf(statisticsData: StatisticsData): String {
        return statisticsData.name
    }
}
