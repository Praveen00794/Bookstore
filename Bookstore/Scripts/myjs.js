$(document).ready(function () {

    var ddlStateid = $("#statecmb");
    $("#concmb").change(function () {

        var countryId = $('#concmb').val();


        $.ajax({
            type: "POST",
            url: '../Books/PopulateStatecmb',
            data: JSON.stringify({ countryid: countryId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {


                ddlStateid.empty().append('<option value="0">Select State</option>');
                $.each(data, function () {

                    ddlStateid.append($("<option></option>").val(this['StateID']).html(this['State']));
                });
            },
            failure: function (data) {
                alert(data);
            }
        });


    });
})