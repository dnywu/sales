steal('jquery/class', function () {
    $.Class('CurrencyandTaxRepository',
{
},
{
    init: function () {
    },
    getAllTax: function () {
        var dataTax = null;
        $.ajax({
            type: 'GET',
            url: '/GetAllTax',
            dataType: 'json',
            async: false,
            success: function (data) {
                dataTax = data;
            }
        });
        return dataTax;
    },
    SaveTax: function (taxModel) {
        var tax = taxModel;
        var response = false;
        $.ajax({
            type: 'POST',
            url: '/SaveTax',
            dataType: 'json',
            data: { 'data': JSON.stringify(tax) },
            async: false,
            success: function (data) {
                response = true;
            }
        });
        return response;
    },
    DeleteTax: function (id) {
        $.ajax({
            type: 'GET',
            url: '/DeleteTax',
            dataType: 'json',
            async: false,
            success: function (data) {
            }
        });
    },
    getAllCurrency: function () {
        var dataInvoice = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetAllCurrency',
            dataType: 'json',
            async: false,
            success: function (data) {

            }
        });
        return alert("test");
    },
    SaveCurrency: function (currencyModel) {
        $.ajax({
            type: 'POST',
            url: '/SaveCurrency',
            dataType: 'json',
            async: false,
            success: function (data) {
            }
        });
    },
    DeleteCurrency: function (id) {
        $.ajax({
            type: 'GET',
            url: '/DeleteCurrency',
            dataType: 'json',
            async: false,
            success: function (data) {
            }
        });
    }
})
});