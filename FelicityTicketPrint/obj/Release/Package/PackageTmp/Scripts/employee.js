//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
});
//Load Data function
function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.EmployeeID + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.TicketNo + '</td>';
                html += '<td>' + item.AirlinePnr + '</td>';
                html += '<td>' + item.CSRPnr + '</td>';
                html += '<td>' + item.TravelDate + '</td>';
                html += '<td>' + item.FlightNo + '</td>';
                html += '<td>' + item.FlightClass + '</td>';
                html += '<td>' + item.Origin + '</td>';
                html += '<td>' + item.DepTime + '</td>';
                html += '<td>' + item.Departure + '</td>';
                html += '<td>' + item.ArrTime + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.EmployeeID + ')">Edit</a> | <a href="#" onclick="Delele(' + item.EmployeeID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Add Data Function
function Add() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        EmployeeID: $('#EmployeeID').val(),
        Name: $('#Name').val(),
        TicketNo: $('#TicketNo').val(),
        AirlinePnr: $('#AirlinePnr').val(),
        CSRPnr: $('#CSRPnr').val(),
        FlightNo: $('#FlightNo').val(),
        FlightClass: $('#FlightClass').val(),
        Origin: $('#Origin').val(),
        DepTime: $('#DepTime').val(),
        Departure: $('#Departure').val(),
        TravelDate: $('#TravelDate').val(),
        ArrTime: $('#ArrTime').val()
    };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Function for getting the Data Based upon Employee ID
function getbyID(EmpID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#TicketNo').css('border-color', 'lightgrey');
    $('#AirlinePnr').css('border-color', 'lightgrey');
    $('#CSRPnr').css('border-color', 'lightgrey');
    $('#TravelDate').css('border-color', 'lightgrey');
    $('#FlightNo').css('border-color', 'lightgrey');
    $('#FlightClass').css('border-color', 'lightgrey');
    $('#Origin').css('border-color', 'lightgrey');
    $('#DepTime').css('border-color', 'lightgrey');
    $('#Departure').css('border-color', 'lightgrey');
    $('#ArrTime').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Home/getbyID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#EmployeeID').val(result.EmployeeID);
            $('#Name').val(result.Name);
            $('#TicketNo').val(result.TicketNo);
            $('#AirlinePnr').val(result.AirlinePnr);
            $('#CSRPnr').val(result.CSRPnr);
            $('#TravelDate').val(result.TravelDate);
            $('#FlightNo').val(result.FlightNo);
            $('#FlightClass').val(result.FlightClass);
            $('#Origin').val(result.Origin);
            $('#DepTime').val(result.DepTime);
            $('#Departure').val(result.Departure);
            $('#ArrTime').val(result.ArrTime);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//function for updating employee's record
function Update() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        EmployeeID: $('#EmployeeID').val(),
        Name: $('#Name').val(),
        TicketNo: $('#TicketNo').val(),
        AirlinePnr: $('#AirlinePnr').val(),
        CSRPnr: $('#CSRPnr').val(),
        FlightNo: $('#FlightNo').val(),
        FlightClass: $('#FlightClass').val(),
        Origin: $('#Origin').val(),
        DepTime: $('#DepTime').val(),
        Departure: $('#Departure').val(),
        TravelDate: $('#TravelDate').val(),
        ArrTime: $('#ArrTime').val()
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#EmployeeID').val("");
            $('#Name').val("");
            $('#TicketNo').val("");
            $('#AirlinePnr').val("");
            $('#CSRPnr').val("");
            $('#FlightNo').val("");
            $('#FlightClass').val("");
            $('#Origin').val("");
            $('#DepTime').val("");
            $('#Departure').val("");
            $('#TravelDate').val("");
            $('#ArrTime').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//function for deleting employee's record
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Home/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
//Function for clearing the textboxes
function clearTextBox() {
    $('#EmployeeID').val("");
    $('#Name').val("");
    $('#TicketNo').val("");
    $('#AirlinePnr').val("");
    $('#CSRPnr').val("");
    $('#FlightNo').val("");
    $('#FlightClass').val("");
    $('#Origin').val("");
    $('#DepTime').val("");
    $('#Departure').val("");
    $('#TravelDate').val("");
    $('#ArrTime').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#TicketNo').css('border-color', 'lightgrey');
    $('#AirlinePnr').css('border-color', 'lightgrey');
    $('#CSRPnr').css('border-color', 'lightgrey');
    $('#TravelDate').css('border-color', 'lightgrey');
    $('#FlightNo').css('border-color', 'lightgrey');
    $('#FlightClass').css('border-color', 'lightgrey');
    $('#Origin').css('border-color', 'lightgrey');
    $('#DepTime').css('border-color', 'lightgrey');
    $('#Departure').css('border-color', 'lightgrey');
    $('#ArrTime').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() === "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#TicketNo').val().trim() === "") {
        $('#TicketNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TicketNo').css('border-color', 'lightgrey');
    }
    if ($('#AirlinePnr').val().trim() === "") {
        $('#AirlinePnr').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#AirlinePnr').css('border-color', 'lightgrey');
    }
    if ($('#CSRPnr').val().trim() === "") {
        $('#CSRPnr').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CSRPnr').css('border-color', 'lightgrey');
    }
    if ($('#TravelDate').val().trim() === "") {
        $('#TravelDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TravelDate').css('border-color', 'lightgrey');
    }
    if ($('#FlightNo').val().trim() === "") {
        $('#FlightNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FlightNo').css('border-color', 'lightgrey');
    }
    if ($('#FlightClass').val().trim() === "") {
        $('#FlightClass').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FlightClass').css('border-color', 'lightgrey');
    }
    if ($('#Origin').val().trim() === "") {
        $('#Origin').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Origin').css('border-color', 'lightgrey');
    }
    if ($('#DepTime').val().trim() === "") {
        $('#DepTime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DepTime').css('border-color', 'lightgrey');
    }
    if ($('#Departure').val().trim() === "") {
        $('#Departure').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Departure').css('border-color', 'lightgrey');
    }
    if ($('#ArrTime').val().trim() === "") {
        $('#ArrTime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ArrTime').css('border-color', 'lightgrey');
    }
    return isValid;
}

function PrintWF() {
    var ticketObj = {
        TotalFare: $('#TotalFare').val(),
        Tnc: $('#Tnc').val(),
        Support: $('#Support').val(),
        AgentName: $('#AgentName').val(),
        AgentAddress: $('#AgentsAddress').val(),
        AgentContact: $('#AgentsContact').val(),
        AdditonalDetails: $('#AdditionalDetails').val()
    };
    $.ajax({
        url: "/Home/PrintWF",
        data: JSON.stringify(ticketObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function PrintWOF() {
    var ticketObj = {
        Tnc: $('#Tnc').val(),
        Support: $('#Support').val()
    };
    $.ajax({
        url: "/Home/PrintWoF",
        data: JSON.stringify(ticketObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}