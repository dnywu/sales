steal('jquery/class', function () {
    $.Class('PaymentTermsRepository',
{
},
{
    init: function () {
    },
    getPaymentTermsById: function (id) {
        var PaymentTerms;
        $.ajax({
            type: 'GET',
            url: '/findpaymenttermsbyid/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                PaymentTerms = data;
            }
        });
        return PaymentTerms;
    },
    createPaymentTerms: function (PaymentTerms) {
        _PaymentTerms = new Array();
        $.ajax({
            type: 'POST',
            url: '/createpaymentterms',
            dataType: 'json',
            data: { 'paymentterms': JSON.stringify(PaymentTerms) },
            async: false,
            success: function (data) {
                _PaymentTerms = data;
            }
        });
        return _PaymentTerms;
    },   
    getAllPaymentTerms: function () {

        _PaymentTerms = new Array();
        $.ajax({
            type: 'GET',
            url: '/findallpaymentterms',
            dataType: 'json',           
            async: false,
            success: function (data) {
                _PaymentTerms = data;
            }
        });
        return _PaymentTerms;
    },
    updatePaymentTerms: function (PaymentTerms) {

        _PaymentTerms = new Array();
        $.ajax({
            type: 'POST',
            url: '/updatepaymentterms',
            dataType: 'json',
            data:{'paymentterms': JSON.stringify(PaymentTerms)},        
            async: false,
            success: function (data) {
                _PaymentTerms = data;
            }
        });
        return _PaymentTerms;
    },
     deletePaymentTerms: function (id) {

        _PaymentTerms = new Array();
        $.ajax({
            type: 'POST',
            url: '/deletepaymentterms/' + id,
            dataType: 'json',           
            async: false,
            success: function (data) {
                _PaymentTerms = data;
            }
        });
        return _PaymentTerms;
    }            
})
});