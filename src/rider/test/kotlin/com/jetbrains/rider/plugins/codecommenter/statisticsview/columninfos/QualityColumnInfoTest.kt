package com.jetbrains.rider.plugins.codecommenter.statisticsview.columninfos

import com.jetbrains.rider.plugins.codecommenter.commons.Quality
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
import org.junit.Test
import kotlin.test.assertEquals

class QualityColumnInfoTest {
    @Test fun `test 10 percents`() {
        val qualityColumnInfo = getQualityColumnInfo()
        assertEquals(
            "10%",
            qualityColumnInfo.valueOf(getStatisticsData(0.1)).second
        )
    }

    @Test fun `test 13 percents`() {
        val qualityColumnInfo = getQualityColumnInfo()
        assertEquals(
            "13%",
            qualityColumnInfo.valueOf(getStatisticsData(0.13)).second
        )
    }

    @Test fun `test 100 percents`() {
        val qualityColumnInfo = getQualityColumnInfo()
        assertEquals(
            "100%",
            qualityColumnInfo.valueOf(getStatisticsData(1.0)).second
        )
    }

    private fun getQualityColumnInfo(): QualityColumnInfo {
        return QualityColumnInfo()
    }

    private fun getStatisticsData(value: Double): StatisticsData {
        return StatisticsData.getRoot().apply {
            this.quality = Quality(value, Quality.Status.Success)
        }
    }
}
