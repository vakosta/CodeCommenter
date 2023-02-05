package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.StatisticsCellRenderer
import javax.swing.table.TableCellRenderer

class QualityColumnInfo : ColumnInfo<StatisticsData, Float>("Quality") {
    override fun valueOf(statisticsData: StatisticsData): Float {
        return statisticsData.quality
    }

    override fun getRenderer(item: StatisticsData?): TableCellRenderer {
        return StatisticsCellRenderer()
    }
}
