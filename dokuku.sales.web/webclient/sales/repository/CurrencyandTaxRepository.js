steal('jquery/class', function () {
    $.Class('CurrencyandTaxRepository',
{
},
{
    init: function () {
    },
    getAllTax: function () {
        var dataTax = new Array();
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
        $.ajax({
            type: 'POST',
            url: '/SaveTax/'+taxModel,
            dataType: 'json',
            async: false,
            success: function (data) {
            }
        });
    },
    DeleteTax: function () {
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
    DeleteCurrency: function () {
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