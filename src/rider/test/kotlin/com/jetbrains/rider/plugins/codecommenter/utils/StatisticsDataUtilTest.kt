package com.jetbrains.rider.plugins.codecommenter.utils

import com.jetbrains.rider.plugins.codecommenter.commons.Quality
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsData
import com.jetbrains.rider.plugins.codecommenter.entities.statistics.StatisticsDataType
import org.junit.Assert.assertEquals
import org.junit.Test

class StatisticsDataUtilTest {
    @Test
    fun `with loading`() {
        val statisticsData = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Loading),
        )

        assertEquals(true, statisticsData.isLoadingRecursive())
        assertEquals(false, statisticsData.isErrorRecursive())
    }

    @Test
    fun `without loading - success`() {
        val statisticsData = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Success),
        )

        assertEquals(false, statisticsData.isLoadingRecursive())
        assertEquals(false, statisticsData.isErrorRecursive())
    }

    @Test
    fun `without loading - failed`() {
        val statisticsData = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Failed),
        )

        assertEquals(false, statisticsData.isLoadingRecursive())
        assertEquals(true, statisticsData.isErrorRecursive())
    }

    @Test
    fun `without loading - canceled`() {
        val statisticsData = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Canceled),
        )

        assertEquals(false, statisticsData.isLoadingRecursive())
        assertEquals(true, statisticsData.isErrorRecursive())
    }

    @Test
    fun `without loading - relative to children`() {
        val statisticsData = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )

        assertEquals(false, statisticsData.isLoadingRecursive())
        assertEquals(false, statisticsData.isErrorRecursive())
    }

    @Test
    fun `recursive - without loading - relative to children`() {
        val statisticsDataLevel1 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel21 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel3 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel22 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )

        statisticsDataLevel1.add(statisticsDataLevel21)
        statisticsDataLevel1.add(statisticsDataLevel22)
        statisticsDataLevel21.add(statisticsDataLevel3)

        assertEquals(false, statisticsDataLevel1.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel1.isErrorRecursive())

        assertEquals(false, statisticsDataLevel21.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel21.isErrorRecursive())

        assertEquals(false, statisticsDataLevel22.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel22.isErrorRecursive())

        assertEquals(false, statisticsDataLevel3.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel3.isErrorRecursive())
    }

    @Test
    fun `recursive - without loading - loading 1`() {
        val statisticsDataLevel1 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel21 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel3 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel22 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Loading),
        )

        statisticsDataLevel1.add(statisticsDataLevel21)
        statisticsDataLevel1.add(statisticsDataLevel22)
        statisticsDataLevel21.add(statisticsDataLevel3)

        assertEquals(true, statisticsDataLevel1.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel1.isErrorRecursive())

        assertEquals(false, statisticsDataLevel21.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel21.isErrorRecursive())

        assertEquals(true, statisticsDataLevel22.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel22.isErrorRecursive())

        assertEquals(false, statisticsDataLevel3.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel3.isErrorRecursive())
    }

    @Test
    fun `recursive - without loading - loading 2`() {
        val statisticsDataLevel1 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel21 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel3 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Loading),
        )
        val statisticsDataLevel22 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Success),
        )

        statisticsDataLevel1.add(statisticsDataLevel21)
        statisticsDataLevel1.add(statisticsDataLevel22)
        statisticsDataLevel21.add(statisticsDataLevel3)

        assertEquals(true, statisticsDataLevel1.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel1.isErrorRecursive())

        assertEquals(true, statisticsDataLevel21.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel21.isErrorRecursive())

        assertEquals(false, statisticsDataLevel22.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel22.isErrorRecursive())

        assertEquals(true, statisticsDataLevel3.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel3.isErrorRecursive())
    }

    @Test
    fun `recursive - without loading - loading and failed`() {
        val statisticsDataLevel1 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel21 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.RelativeToChildren),
        )
        val statisticsDataLevel3 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Loading),
        )
        val statisticsDataLevel22 = StatisticsData(
            StatisticsDataType.File, "", "", "", 0.1,
            Quality(0.2, Quality.Status.Failed),
        )

        statisticsDataLevel1.add(statisticsDataLevel21)
        statisticsDataLevel1.add(statisticsDataLevel22)
        statisticsDataLevel21.add(statisticsDataLevel3)

        assertEquals(true, statisticsDataLevel1.isLoadingRecursive())
        assertEquals(true, statisticsDataLevel1.isErrorRecursive())

        assertEquals(true, statisticsDataLevel21.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel21.isErrorRecursive())

        assertEquals(false, statisticsDataLevel22.isLoadingRecursive())
        assertEquals(true, statisticsDataLevel22.isErrorRecursive())

        assertEquals(true, statisticsDataLevel3.isLoadingRecursive())
        assertEquals(false, statisticsDataLevel3.isErrorRecursive())
    }
}
