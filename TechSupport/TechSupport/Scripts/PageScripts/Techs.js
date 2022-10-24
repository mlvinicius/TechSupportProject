function checkEntities(techId) {
    $.ajax({
        url: `/Techs/DeleteTechnician/?id=${techId}`,
        type: 'GET',
        success: function (data) {
            window.location.reload();
        },
        error: function (data) {
            $('.toast').toast({ delay: 20000 });
            $('.toast').toast('show');
            $('.toast-body').html(data.responseText);
        }
    });
}