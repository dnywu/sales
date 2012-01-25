steal('jquery/class', function () {
    $.Class('CurrencyandTaxRepository',
{
},
{
    init: function () {
    },
    getAllTax: function () {
        var dataInvoice = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetAllTax',
            dataType: 'json',
            async: false,
            success: function (data) {

            }
        });
        return alert("test"); 
    },
    SaveTax: function () {
        $.ajax({
            type: 'POST',
            url: '/SaveTax',
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
    }
})
});