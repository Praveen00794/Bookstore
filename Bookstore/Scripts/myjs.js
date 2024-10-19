$(document).ready(function () {

    

    $("#concmb").change(function () {
        var ddlStateid = $("#statecmb");
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
    //Date validation
    $("#pubdate").change(function () {
        debugger
        var currentdate = new Date();
        var pubdate = new Date($("#pubdate").val());
        $("#pubdateval").text("");
        if (pubdate > currentdate) {
            $("#pubdateval").text("Cannot future date")
            $("#pubdate").val("");

        }
    });


    $("#statecmb").click(function () {
        
        var country = $("#concmb").val(); // Get the value of the country dropdown
        $("#concmbval").text(""); // Clear previous messages

        // Check if the country is empty or the default option
        if (country === "" || country === "select-Country") {
            $("#concmbval").text("Please select a valid country first.");
        }
        else {
            $("#concmbval").text("");
        }
    });

    $('.del-btn').click(function () {

        employeeId = $(this).data('id');
        $('#confirm-delete').modal('show');
    });

    $('#confirm-btn').click(function () {
        
        $.post('/Books/Deletebook', { id: employeeId }, function () {
            location.reload();
        });
    });

    $('#cancel-btn').click(function () {
        $('#confirm-delete').hide();
    });

    //$('#resetFields').click(function (e) {
    //    e.preventDefault(); // Prevent the default action of the button

    //    // Clear all textboxes
    //    $('#Hideformload').find('input[type="text"]').val('');

    //    // Reset dropdowns to the first option
    //    $('#Hideformload').find('select').prop('selectedIndex', 0);

    //    // Clear the date input
    //    $('#Hideformload').find('input[type="date"]').val('');
    //});
})