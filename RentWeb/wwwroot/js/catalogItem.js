let table = new DataTable('#DT_load', {
    language: {
        url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/ru.json',
    },
    "ajax": {
        "url": "/api/CatalogItem",
        "type": "GET",
        "datatype": "json"
    },
    "columns": [
        { "data": "name", "width": "25%" },
        { "data": "price", "width": "25%" },
        { "data": "category.name", "width": "25%" },
        { "data": "powerType.name", "width": "25%" },
        {
            "data": "id",
            "render": function (data) {
                return `<div class="w-75 btn-group">
                        <a href="/Admin/CatalogItems/upsert?id=${data}" class="btn btn-warning mx-2">
                        <i class="bi bi-pencil"></i></a>
                        <a onClick=Delete('/api/CatalogItem/'+${data}) class="btn btn-outline-danger" mx-2">
                        <i class="bi bi-trash"></i></a>
                        </div>`
            },
            "width": "25%"
        },

    ],



    "width": "100%"
});

function Delete(url) {
    Swal.fire({
        title: 'Ты уверен?',
        text: "Ты не сможешь изменить действие!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Да, удалить!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        table.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failure notification
                        toastr.error(data.message);

                    }
                }
            })
        }
    })
}