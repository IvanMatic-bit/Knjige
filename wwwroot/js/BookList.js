var dataTable = $('#DT_load');
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    $.ajax({
        url: "book/getall",
        type: "GET",
        dataType: "json",
        success: function (obj) {
            var podaci = obj.data;
            for (i in podaci) {
                var row = `<tr>
                        <td>` + podaci[i].name + `</td>
                        <td>` + podaci[i].author + `</td>
                        <td>` + podaci[i].isbn + `</td>
                        <td><a href="Book/Update?id=`+podaci[i].id+`"  asp-controller="Book" class="btn btn-success">
                        Edit</a>
                        <button onclick="Izbrisi('Book/Delete?id=`+podaci[i].id+`')" class="btn btn-danger">Delete</button>
                        </td></tr>`;
                dataTable.append(row);
             }
            
        }
        
    });
}


function Izbrisi(url) {
    $.ajax({
        url: url,
        type: "DELETE",
        success: function (data) {  
            dataTable.empty();
                loadDataTable();
                alert(data.message);
             
        }
    });
}
