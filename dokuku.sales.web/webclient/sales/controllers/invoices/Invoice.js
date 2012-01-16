steal('jquery/class', function () {
    $.Class('Invoice',
{
},
{
    init: function () {
    },
    CalculateAmountPerItem: function (qty, rate, disc) {
        var amount = (rate - ((rate * disc) / 100)) * qty;
        return amount;
    },
    CalculateSubTotal: function () {
        var totalItem = $("#itemInvoice > tbody > tr").size();
        var subtotal = 0;
        for (var i = 0; i < totalItem; i++) {
            if ($("#part_" + i).val() != "") {
                subtotal += parseFloat($("#amount_" + i).text());
            }
        }
        return subtotal;
    }
})
});