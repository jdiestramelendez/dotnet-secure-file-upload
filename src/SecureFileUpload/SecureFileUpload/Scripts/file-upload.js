$(document).ready(function () {
    $("#pnlResults, #pnlErrors").hide();

    $("#btnSubmitFileUpload").click(function (event) {
        $("#pnlResults, #pnlErrors").hide();
        event.preventDefault();
        var fileUploadUrl = window.window.fileUploadServiceUrl.concat("FileUpload");

        var data = new FormData();
        var field = $('[name=csvFile1]')[0];
        data.append(field.name, field.files[0], field.value);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: fileUploadUrl,
            data: data,
            processData: false,
            contentType: false,
            dataType: 'json',
            cache: false,
            timeout: 800000,
            success: function (data, textStatus, jqXHR) {
                var filename = data.blobs[0];
                $("#results").empty();
                getAndUpdateTrackerResults(filename);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error(jqXHR)
                $("#pnlErrors").show();
                $("#errors").text(textStatus + ":" + errorThrown);
            }
        });
    });

    function getAndUpdateTrackerResults(filename) {
        $("#pnlResults").show();
        var url = window.fileUploadServiceUrl.concat('FileTracker/').concat(filename);
        $.ajax({
            type: "GET",
            url: url,
            dataType: 'json',
            cache: false,
            timeout: 5000,
            success: function (data, textStatus, jqXHR) {
                $("#results").append("<pre>" + JSON.stringify(data, null, 2) + "</pre>");
                if (data.processingComplete) {
                    $("#results").append("<div class='alert alert-success'>Processing Complete!</div>");
                } else {
                    $("#results").append("<div class='alert alert-info'>Checking for update...</div>");
                    setTimeout(getAndUpdateTrackerResults, 1000, filename);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error(jqXHR)
                $("#pnlErrors").show();
                $("#errors").text(textStatus + ":" + errorThrown);
            }
        });
    }
});