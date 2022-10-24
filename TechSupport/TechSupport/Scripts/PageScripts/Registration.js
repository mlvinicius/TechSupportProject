function selectCustomer() {
    $.get('/Incident/ChooseCustomer/', function (data) {
        $('#registrationModal').modal('show');
        $('.modal-body').html(data);
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
                $('#registrationModal').modal('toggle');
            }
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    });
}

function selectProduct() {
    $.get('/Incident/ChooseProduct/', function (data) {
        $('#registrationModal').modal('show');
        $('.modal-body').html(data);
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
                $('#registrationModal').modal('toggle');
            }
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    });
}

function custSearch() {
    var term = document.getElementById('custSearch').value;
    $.get('/Incident/ChooseCustomerSearch/?id=' + term, function (data) {
        $('#registrationModal').modal('show');
        $('.modal-body').html(data);
    });
}

function prodSearch() {
    var term = document.getElementById('prodSearch').value;
    $.get('/Incident/ChooseProductSearch/?id=' + term, function (data) {
        $('#registrationModal').modal('show');
        $('.modal-body').html(data);
    });
}