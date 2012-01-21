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
        var dataInvoice = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetDataInvoice',
            dataType: 'json',
            async: false,
            success: function (data) {
                $.each(data, function (i) {
                    dataInvoice[i] = data[i];
                    var InvoiceDate = new Date(parseInt(dataInvoice[i].InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    var DueDate = new Date(parseInt(dataInvoice[i].DueDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    dataInvoice[i].InvoiceDate = $.datepicker.formatDate('dd M yy', InvoiceDate);
                    dataInvoice[i].DueDate = $.datepicker.formatDate('dd M yy', DueDate);
                    dataInvoice[i].Total = String.format("{0:C}", dataInvoice[i].Total);
                });

            }
        });
        return dataInvoice;
    }
})
});