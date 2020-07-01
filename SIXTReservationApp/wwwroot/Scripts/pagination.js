var page = 1;
var pageSize = 10;
var total = 0;

function setPageSize(n = 10) {
    pageSize = n;
}

function updatePagination(p, ps, t, count) {
    page = p;
    pageSize = ps;
    total = t;

    const start = ((page - 1) * pageSize) + 1;
    const end = start + count - 1;
    $('#txtPaginationInfo').text(`Showing ${start} - ${end} of total ${total} items`);
    $('#txtPage').text(page);
    if (page <= 1) {
        $('#btnPrev').prop('disabled', true);
    } else {
        $('#btnPrev').prop('disabled', false);
    }

    if (page >= Math.ceil(total / pageSize)) {
        $('#btnNext').prop('disabled', true);
    } else {
        $('#btnNext').prop('disabled', false);
    }
}

function resetPagination()
{
    page = 1;
    pageSize = 10;
    total = 0;
}