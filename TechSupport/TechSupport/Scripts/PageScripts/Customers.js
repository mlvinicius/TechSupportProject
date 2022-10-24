function tryDelete(custId) {
    $.ajax({
        url: `/Customers/DeleteCustomer/?id=${custId}`,
        type: 'GET',
        success: function (data) {
            window.location.reload();
        },
        error: function (data) {
            $('.toast').toast({delay: 5000});
            $('.toast').toast('show');
            $('.toast-body').html(data.responseText);
        }
    });
}
