$(document).ready(function () {

    $(".dropdown.dropdown-notification").on("click", function () {
        $(".NotificationBar").toggleClass("DisNone");
        $(".page-content").toggleClass("col-10").toggleClass("col-12");

    });




    $('.mdb-select').materialSelect();

    $(function () {
        $('.material-tooltip-main').tooltip();
    })

    $("#form-sample-1").validate({
        rules: {
            name: {
                minlength: 2,
                required: !0
            },
            email: {
                required: !0,
                email: !0
            },
            url: {
                required: !0,
                url: !0
            },
            number: {
                required: !0,
                number: !0
            },
            min: {
                required: !0,
                minlength: 3
            },
            max: {
                required: !0,
                maxlength: 4
            },
            password: {
                required: !0
            },
            password_confirmation: {
                required: !0,
                equalTo: "#password"
            }
        },
        errorClass: "help-block error",
        highlight: function (e) {
            $(e).closest(".form-group.row").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group.row").removeClass("has-error")
        },
    });
});

$(function () {
    $('#login-form').validate({
        errorClass: "help-block",
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true
            }
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
    });
});
$(function () {
    $('#example-table').DataTable({
        pageLength: 10,
        //"ajax": '~/Assets/demo/data/table_data.json',
        /*"columns": [
            { "data": "name" },
            { "data": "office" },
            { "data": "extn" },
            { "data": "start_date" },
            { "data": "salary" }
        ]*/
    });
})

$(function () {
    $('#forgot-form').validate({
        errorClass: "help-block",
        rules: {
            email: {
                required: true,
                email: true
            },
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
    });
});
$(function () {
    $('#ex-date').mask('99/99/9999', {
        placeholder: 'dd/mm/yyyy'
    });
    $('#ex-phone').mask('(999) 999-9999');
    $('#ex-phone2').mask('+186 999 999-9999');
    $('#ex-ext').mask('(999) 999-9999? x9999');
    $('#ex-credit').mask('****-****-****-****', {
        placeholder: '*'
    });
    $('#ex-tax').mask('99-9999999');
    $('#ex-currency').mask('$ 99.99');
    $('#ex-product').mask('a*-999-a999', {
        placeholder: 'a*-999-a999'
    });

    $.mask.definitions['~'] = '[+-]';
    $('#ex-eye').mask('~9.99 ~9.99 999');
})

$(function () {
    $('#summernote').summernote();
    $('#summernote_air').summernote({
        airMode: true
    });
});

$(function () {
    $('#summernote').summernote();
    $('.note-popover').css({
        'display': 'none'
    });
})
$(function () {
    $('#lock-form').validate({
        errorClass: "help-block",
        rules: {
            password: {
                required: true
            }
        },
        highlight: function (e) {
            $(e).closest(".form-group").addClass("has-error")
        },
        unhighlight: function (e) {
            $(e).closest(".form-group").removeClass("has-error")
        },
        errorPlacement: function (e, r) {
            var i = $(r).parents(".input-group, .check-list");
            i.length ? i.after(e) : r.after(e)
        },
    });
});