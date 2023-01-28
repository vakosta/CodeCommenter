package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.model.StatisticsData

class DescriptionColumnInfo : ColumnInfo<StatisticsData, String>("Description") {
    override fun valueOf(statisticsData: StatisticsData): String {
        return statisticsData.docstring
    }
}
