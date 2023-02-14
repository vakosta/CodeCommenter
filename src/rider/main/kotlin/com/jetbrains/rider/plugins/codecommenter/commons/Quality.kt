package com.jetbrains.rider.plugins.codecommenter.commons

data class Quality(
    var value: Double,
    var status: Status,
) {
    enum class Status {
        Loading,
        Success,
        Failed,
        Canceled,
        RelativeToChildren,
    }
}
