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

    $("#btnBookImportCLEAR").click(function () {
        var vrtbldata = "";
        $("#flBookInfo").val(null);
        $("#tblImportExcelData").empty();
    });
    $("#btnBookImportShow").click(function () {
        
       
        ExportToTable();//Call the function

    });

    var vrtbldata = "";
    function ExportToTable() {
        
        vrtbldata = "";
        var val = $("#flBookInfo").val().toLowerCase();
        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
        /*Checks whether the file is a valid excel file*/
        if (regex.test($("#flBookInfo").val().toLowerCase())) {
            var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/
            if ($("#flBookInfo").val().toLowerCase().indexOf(".xlsx") > 0) {
                xlsxflag = true;
            }
            /*Checks whether the browser supports HTML5*/
            if (typeof (FileReader) != "undefined") {
                
                var reader = new FileReader();
                reader.onload = function (e) {
                    
                    var data = e.target.result;

                    /* Converts the Excel data into an object */
                    var workbook;
                    if (xlsxflag) {
                        workbook = XLSX.read(data, { type: 'binary' });
                    } else {
                        workbook = XLS.read(data, { type: 'binary' });
                    }
                    /*Gets all the sheetnames of excel in to a variable*/
                    var sheet_name_list = workbook.SheetNames;

                    var cnt = 0; /*This is used for restricting the script to consider only first sheet of excel*/
                    sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/
                        /*Convert the cell value to Json*/
                        if (xlsxflag) {
                            var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                            //var exceljson = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                        }
                        else {
                            var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                        }
                        if (exceljson.length > 0 && cnt == 0) {
                            
                            BindTable(exceljson, '#tblImportExcelData');
                            cnt++;
                        }
                    });
                    $('#tblImportExcelData').show();
                }
                if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/
                    reader.readAsArrayBuffer($("#flBookInfo")[0].files[0]);
                }
                else {
                    reader.readAsBinaryString($("#flBookInfo")[0].files[0]);
                }
            }
            else {
                alert("Sorry! Your browser does not support HTML5!");
            }
        }
        else {
            debugger
            alert("Please upload a valid Excel file!");
        }

    }
    function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/
        
        var columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/
        for (var i = 0; i < jsondata.length; i++) {
            var row$ = $('<tr/>');
            var tddat
            for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                var cellValue = jsondata[i][columns[colIndex]];
                var cel1, cel2, cel3, cel4, cel5, cel6, cel7, cel8, cel9, cel10, cel11, cel12, cel13, cel14, cel15, cel16, cel17, cel18, cel19, cel20, cel21, cel21, cel22, cel23;
                if (cellValue == null) {
                    cellValue = "0";
                }
                if (colIndex == 0) { cel1 = cellValue; }
                if (colIndex == 1) { cel2 = cellValue; }
                if (colIndex == 2) { cel3 = cellValue; }
                if (colIndex == 3) { cel4 = cellValue; }
                if (colIndex == 4) { cel5 = cellValue; }
                if (colIndex == 5) { cel6 = cellValue; }
                if (colIndex == 6) { cel7 = cellValue; }
                if (colIndex == 7) { cel8 = cellValue; }
                if (colIndex == 8) { cel9 = cellValue; }
                if (colIndex == 9) { cel10 = cellValue; }
                if (colIndex == 10) { cel11 = cellValue; }
                if (colIndex == 11) { cel12 = cellValue; }
                if (colIndex == 12) { cel13 = cellValue; }
                if (colIndex == 13) { cel14 = cellValue; }
                if (colIndex == 14) { cel15 = cellValue; }
                if (colIndex == 15) { cel16 = cellValue; }
                if (colIndex == 16) { cel17 = cellValue; }
                if (colIndex == 17) { cel18 = cellValue; }
                if (colIndex == 18) { cel19 = cellValue; }
                if (colIndex == 19) { cel20 = cellValue; }
                if (colIndex == 20) { cel21 = cellValue; }
                if (colIndex == 21) { cel22 = cellValue; }
                if (colIndex == 22) { cel23 = cellValue; }
                if (colIndex == 23) { cel24 = cellValue; }
                row$.append($('<td/>').html(cellValue));
                vrtbldata += cellValue + '|';
            }
            vrtbldata += '~';
            $(tableid).append(row$);
        }
    }
    function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/
        var columnSet = [];
        var headerTr$ = $('<tr/>');
        for (var i = 0; i < jsondata.length; i++) {
            var rowHash = jsondata[i];
            for (var key in rowHash) {
                if (rowHash.hasOwnProperty(key)) {
                    if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/
                        columnSet.push(key);
                        headerTr$.append($('<th/>').html(key));
                    }
                }
            }
        }
        $(tableid).append(headerTr$);
        return columnSet;
    }


})