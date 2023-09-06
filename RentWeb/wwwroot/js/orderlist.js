var table;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled")
    }
    else {
        if (url.includes("completed")) {
            loadList("completed")
        }
        else {
            if (url.includes("inProcess")) {
                loadList("inProcess")
            }
            else {
                if (url.includes("ready")) {
                    loadList("ready")
                }
                else {
                    if (url.includes("issued")) {
                        loadList("issued")
                    }
                    else {
                        if (url.includes("returned")) {
                            loadList("returned")
                        }
                    }
                }
            }
        }
    }
});

function loadList(param) {
    table = $('#DT_load').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/ru.json',
        },
        "ajax": {
            "url": "/api/order?status=" + param,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "pickupName", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpTime", "width": "25%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                        <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-warning mx-2">
                        <i class="bi bi-pencil"></i></a>
                        </div>`
                },
                "width": "25%"
            },

        ],
        "width": "100%"
    });
}