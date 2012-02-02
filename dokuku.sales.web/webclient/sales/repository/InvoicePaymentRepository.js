steal('jquery/class', function () {
    $.Class('InvoicePaymentRepository',
{
},
{
    init: function () {
    },
    GetPaymentById: function (id) {
        var invoice;
        $.ajax({
            type: 'GET',
            url: '/GetPaymentById/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                invoice = data;
            }
        });
        return invoice;
    },
    GetAllPayment: function () {
        var dataPayment = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetAllPayment',
            dataType: 'json',
            async: false,
            success: function (data) {
                $.each(data, function (i) {
                    //                    dataInvoice[i] = data[i];
                    //                    var InvoiceDate = new Date(parseInt(dataInvoice[i].InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    //                    var DueDate = new Date(parseInt(dataInvoice[i].DueDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    //                    dataInvoice[i].InvoiceDate = $.datepicker.formatDate('dd M yy', InvoiceDate);
                    //                    dataInvoice[i].DueDate = $.datepicker.formatDate('dd M yy', DueDate);
                    //                    dataInvoice[i].Total = String.format("{0:C}", dataInvoice[i].Total);
                });

            }
        });
        return null;
    },
//    PaymentByIdInvoice: function (id) {

//        _payment = new Array();
//        $.ajax({
//            type: 'POST',
//            url: '/Save/' + id,
//            dataType: 'json',
//            data: { 'data': JSON.stringify(_payment) },
//            async: false,
//            success: function (data) {
//                _payment = data;
//            }
//        });
//        return _payment;
//    }, 
    getAllPaymentMode: function () {

        _paymentMode = new Array();
        $.ajax({
            type: 'GET',
            url: '/findallpaymentmode',
            dataType: 'json',           
            async: false,
            success: function (data) {
                _paymentMode = data;
            }
        });
        return _paymentMode;
    },
    pay: function (invoicepayment) {
        _paymentMode = new Array();
        $.ajax({
            type: 'POST',
            url: '/pay',
            dataType: 'json',
            data:{'invoicepayment':JSON.stringify(invoicepayment)},           
            async: false,
            success: function (data) {
                _paymentMode = data;
            }
        });
        return _paymentMode;
    },
     sendToEmail: function (emailTo) {
        $.ajax({
            type: 'POST',
            url: '/SendToEmail/'+emailTo,
            dataType: 'json',          
            async: false,
            success: function (data) {              
            }
        });
    }

   
})
});