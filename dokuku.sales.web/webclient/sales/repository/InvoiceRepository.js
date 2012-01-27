steal('jquery/class', function () {
    $.Class('InvoiceRepository',
{
},
{
    init: function () {
    },
    GetInvoiceById: function (id) {
        var invoice;
        $.ajax({
            type: 'GET',
            url: '/invoice/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                invoice = data;
            }
        });
        return invoice;
    },
    GetAllInvoice: function () {
        var dataInvoice = null;
        $.ajax({
            type: 'GET',
            url: '/GetDataInvoice',
            dataType: 'json',
            async: false,
            success: function (data) {
                dataInvoice = data;
            }
        });
        return dataInvoice;
    }
})
});