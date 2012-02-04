steal('jquery/class', function () {
    $.Class('PaymentReceivedRepository',
{
},
{
    init: function () {
    },
    GetPaymentReceivedById: function (id) {
        var invoice;
        $.ajax({
            type: 'GET',
            url: '/GetPaymentReceivedById/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                invoice = data;
            }
        });
        return invoice;
    },
    getAllPaymentReceived: function () {
        var dataPaymentReceived = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetAllPaymentReceived',
            dataType: 'json',
            async: false,
            success: function (data) {
                dataPaymentReceived = data;
            }
        });
        return dataPaymentReceived;
    },
    updatePaymentReceived: function (dataPaymentReceived) {
        var _dataPaymentReceived = new Array();
        $.ajax({
            type: 'POST',
            url: '/UpdatePaymentReceived',
            dataType: 'json',
            data:{'paymentreceived':JSON.stringify(dataPaymentReceived)},
            async: false,
            success: function (data) {
                _dataPaymentReceived = data;
            }
        });
        return _dataPaymentReceived;
    },
    getDataPaymentReceivedByLimit: function (start, limit) {
        var dataInvoice = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetDataPaymentReceivedToPaging/' + start + '/' + limit + '',
            dataType: 'json',
            async: false,
            success: function (data) {
//                $.each(data, function (i) {
//                    dataInvoice[i] = data[i];
//                    var InvoiceDate = new Date(parseInt(dataInvoice[i].InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
//                    var DueDate = new Date(parseInt(dataInvoice[i].DueDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
//                    dataInvoice[i].InvoiceDate = $.datepicker.formatDate('dd M yy', InvoiceDate);
//                    dataInvoice[i].DueDate = $.datepicker.formatDate('dd M yy', DueDate);
//                    dataInvoice[i].Total = String.format("{0:C}", dataInvoice[i].Total);
//                });
            }
        });
        return dataInvoice;
    },

})
});