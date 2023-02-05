package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.intellij.util.ui.ColumnInfo
import com.jetbrains.rider.plugins.codecommenter.models.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.statisticsview.renderers.StatisticsCellRenderer
import javax.swing.table.TableCellRenderer

class CoverageColumnInfo : ColumnInfo<StatisticsData, String>("Coverage") {
    override fun valueOf(statisticsData: StatisticsData): String {
        return "${(statisticsData.coverage * 100).toInt()}%"
    }

    override fun getRenderer(item: StatisticsData?): TableCellRenderer {
        return StatisticsCellRenderer()
    }
}
