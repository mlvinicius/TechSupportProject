
function selectCustomer() {
    $.get('/Incident/ChooseCustomer/', function (data) {
        $('#customerModal').modal('show');
        $('.modal-body').html(data);
        $('.modal-title').html('Select Customer');
    });
}

function getCustName(custId) {

    $.ajax({
        type: 'GET',
        url: `/Incident/SelectCustomer/?custId=${custId}`,
        data: {},
        success: function (data) {
            if (data) {
                $('#custName').val(data);
                $('#custId').val(custId);
                $('#customerModal').modal('toggle');
            }
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    });
}

function selectTechnician() {
    $.get('/Incident/ChooseTechnician/', function (data) {
        $('#customerModal').modal('show');
        $('.modal-body').html(data);
        $('.modal-title').html('Select Technician');
    });
}

function getTechName(techId) {

    $.ajax({
        type: 'GET',
        url: `/Incident/SelectTechnician/?techId=${techId}`,
        data: {},
        success: function (data) {
            if (data) {
                $('#techName').val(data);
                $('#techId').val(techId);
                $('#customerModal').modal('toggle');
            }
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    });
}

function selectProduct() {
    $.get('/Incident/ChooseProduct/', function (data) {
        $('#customerModal').modal('show');
        $('.modal-body').html(data);
        $('.modal-title').html('Select Product');
    });
}

function getProdName(prodCode) {

    $.ajax({
        type: 'GET',
        url: `/Incident/SelectProduct/?prodCode=${prodCode}`,
        data: {},
        success: function (data) {
            if (data) {
                $('#prodName').val(data);
                $('#prodCode').val(prodCode);
                $('#customerModal').modal('toggle');
            }
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    });
}

function custSearch() {
    var term = document.getElementById('custSearch').value;
    $.get('/Incident/ChooseCustomerSearch/?id=' + term, function (data) {
        $('#customerModal').modal('show');
        $('.modal-body').html(data);
    });
}

function prodSearch() {
    var term = document.getElementById('prodSearch').value;
    $.get('/Incident/ChooseProductSearch/?id=' + term, function (data) {
        $('#customerModal').modal('show');
        $('.modal-body').html(data);
    });
}

function techSearch() {
    var term = document.getElementById('techSearch').value;
    $.get('/Incident/ChooseTechnicianSearch/?id=' + term, function (data) {
        $('#customerModal').modal('show');
        $('.modal-body').html(data);
    });
}

function displayIncident(id) {
    $.get('/Incident/EditIncident/?id=' + id, function (data) {
        $('#incidentModal').modal('show');
        $('.modal-body').html(data);
        $('.modal-title').html('Incident');
    });
}