var clockworkHost = "http://localhost:19897/";
clockwork = {
    tableResponsiveRow: function (timeZone, localTime, utcTime) {
        return "<div class='row no-gutters'>"
                + "<div class='col-md-4 col-lg-6'>"
                    + "<div class='row no-gutters' timezone-length='" + timeZone.length + "'>"
                        + "<div class='col-md-12 col-4 table-header'>TimeZone</div>"
                        + "<div class='col-md-12 col table-data'>" + timeZone + "</div>"
                    + "</div>"
                + "</div>"
                + "<div class='col-md-4 col-lg-3'>"
                    + "<div class='row no-gutters'>"
                        + "<div class='col-md-12 col-4 table-header'>Local</div>"
                        + "<div class='col-md-12 col table-data'>" + new Date(localTime).toLocaleString() + "</div>"
                    + "</div>"
                + "</div>"
                + "<div class='col-md-4 col-lg-3'>"
                    + "<div class='row no-gutters'>"
                        + "<div class='col-md-12 col-4 table-header'>UTC</div>"
                        + "<div class='col-md-12 col table-data'>" + new Date(utcTime).toLocaleString() + "</div>"
                    + "</div>"
                + "</div>"
            + "</div>";
    },
    initPage: function () {
        $.ajax({
            url: clockworkHost+ "api/logs",
            success: function (result) {
                $.each(result, function (i, item) {
                    $('#logs').append(
                        clockwork.tableResponsiveRow(item.timeZone, item.localTime, item.utcTime)
                    );
                });
            }
        })
        $.ajax({
            url: clockworkHost + "api/timezones",
            success: function (result) {
                $.each(result, function (i, item) {
                    $("#timeZoneId").append(
                        "<option value='" + item.id + "'>" + item.name + "</option>"
                    )
                })
            }
        })
    },
    getCurrentTime: function () {
        $.ajax({
            url: clockworkHost + "api/currenttime",
            data: { timeZoneId: $("#timeZoneId").val() },
            success: function (result) {
                $('#logs').prepend(
                    clockwork.tableResponsiveRow(result.timeZone, result.localTime, result.utcTime)
                )
            }
        })
    }
}
