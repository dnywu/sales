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
        var subtotal = 0;
        var tmpval = 0;
        $('.amount').each(function (index) {
            tmpval = parseFloat($(this).text());
            if (!isNaN(tmpval))
                subtotal += tmpval;
        });
        return subtotal;
    },
    CalculateTotal: function () {
        var tax = 0;
        var subtotal = parseFloat($("#subtotal").text());
        var total = subtotal;
        if (tax > 0)
            total = subtotal * tax;
        return total;
    },
    CreateNewInvoice: function () {
        var length = $("#itemInvoice > tbody > tr").size();
        var objInv = new Object;
        objInv.Customer = custid;
        objInv.CustomerId = custid;
        objInv.PONo = $("#po").val();
        objInv.InvoiceDate = $("#invDate").val();
        objInv.Terms = $("#terms").val();
        objInv.DueDate = $("#dueDate").val();
        objInv.LateFee = $("#latefee").val();
        objInv.Note = $("#custMsg").val();
        objInv.TermCondition = $("#termAndCond").val();
        objInv.SubTotal = $("#subtotal").text();
        objInv.Total = $("#total").text();
        objInv.Items = new Array;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').get(i).value != "") {
                objInv.Items[i] = new Object;
                objInv.Items[i].PartName = $('.partname').get(i).value;
                objInv.Items[i].Description = $('.description').get(i).value;
                objInv.Items[i].Qty = $('.quantity').get(i).value;
                objInv.Items[i].Rate = $('.price').get(i).value;
                objInv.Items[i].Discount = $('.discount').get(i).value;
                objInv.Items[i].Tax = $('.taxed').get(i).value;
                objInv.Items[i].Amount = $('.amount').get(i).innerText;
            }
        });
        var newInv = JSON.stringify(objInv);

        $.ajax({
            type: 'POST',
            url: '/createinvoice',
            data: { 'invoice': newInv },
            dataType: 'json',
            async: false,
            success: function (data) {

            }
        });
    }
})
});