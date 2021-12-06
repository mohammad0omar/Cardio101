////$("#modal-details-study").modal("show");

var link = '/studies/modal';


$(".sdsds").on('click', function (e) {
    console.log()
    modelPopup(this, e.currentTarget.id);
});

function modelPopup(reff,id) {
    $.ajax({
        type: "GET",
        url: link,
        contentType: "application/json; charset=utf-8",
        data: { "Id": id },
        datatype: "json",
        success: function (data) {
            console.log(data)
            $('#modal-details-study-data').html(data);
            $('#modal-details-study').modal("show");

        },
        error: function (e) {
            console.log(e)
            alert("Dynamic content load failed.");
        }
    });
    //var url = $(reff).data('url');

    //$.get(url).done(function (data) {
    //    //debugger;
    //    $('#modal-details-study').modal("show");
    //    //$('#modal-details-study > .modal', data).modal("show");
    //});

}