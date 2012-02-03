steal('jquery/class','sales/scripts/stringformat.js', function () {
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
                var InvoiceDate = new Date(parseInt(data.InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                data.InvoiceDate = $.datepicker.formatDate('dd M yy', InvoiceDate);
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