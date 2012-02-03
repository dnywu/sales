steal('jquery/class', function () {
    $.Class('PaymentModeRepository',
{
},
{
    init: function () {
    },
    getPaymentModeById: function (id) {
        var paymentMode;
        $.ajax({
            type: 'GET',
            url: '/findpaymentmodebyid/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                paymentMode = data;
            }
        });
        return paymentMode;
    },
    createPaymentMode: function (paymentmode) {
        _paymentMode = new Array();
        $.ajax({
            type: 'POST',
            url: '/createpaymentmode',
            dataType: 'json',
            data: { 'paymentmode': JSON.stringify(paymentmode) },
            async: false,
            success: function (data) {
                _paymentMode = data;
            }
        });
        return _paymentMode;
    },   
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
    updatePaymentMode: function (paymentMode) {

        _paymentMode = new Array();
        $.ajax({
            type: 'POST',
            url: '/updatepaymentmode',
            dataType: 'json',
            data:{'paymentmode': JSON.stringify(paymentMode)},        
            async: false,
            success: function (data) {
                _paymentMode = data;
            }
        });
        return _paymentMode;
    },
     deletePaymentMode: function (id) {

        _paymentMode = new Array();
        $.ajax({
            type: 'POST',
            url: '/deletepaymentmode/' + id,
            dataType: 'json',           
            async: false,
            success: function (data) {
                _paymentMode = data;
            }
        });
        return _paymentMode;
    }            
})
});