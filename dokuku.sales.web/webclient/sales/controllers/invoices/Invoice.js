steal('jquery/class', 'sales/scripts/stringformat.js', function () {
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
            tmpval = parseFloat($(this).val());
            if (!isNaN(tmpval))
                subtotal += tmpval;
        });
        return subtotal;
    },
    CalculateTotal: function () {
        var tax = 0;
        var subtotal = parseFloat($("#subtotal").val());
        var total = subtotal;
        if (tax > 0)
            total = subtotal * tax;
        return total;
    },
    ShowListItem: function (part, index) {
        $("#partid_" + index).val(part._id);
        $("#part_" + index).val(part.Name);
        $("#desc_" + index).text(part.Description);
        $("#qty_" + index).val('1.00');
        $("#rate_" + index).val(part.Rate);
        $("#disc_" + index).val('0.00');
        $("#amount_" + index).val(part.Rate);
        $("#amounttext_" + index).text(String.format("{0:C}", part.Rate));
        $("#itemInvoice tbody tr#tr_" + index).removeClass('errItemNotFound');
    },
    CreateNewInvoice: function () {
        var length = $("#itemInvoice > tbody > tr").size();
        var objInv = new Object;
        objInv.Customer = $("#selectcust").val();
        objInv.CustomerId = $("#CustomerId").val();
        objInv.PONo = $("#po").val();
        objInv.InvoiceDate = $("#invDate").val();
        objInv.Terms = new Object();
        objInv.Terms.Value = $("#terms").val();
        objInv.Terms.Name = $("#terms option[value='" + objInv.Terms.Value + "']").text().trim();
        objInv.DueDate = $("#dueDate").val();
        objInv.LateFee = $("#latefee").val();
        objInv.Note = $("#custMsg").val();
        objInv.TermCondition = $("#termAndCond").val();
        objInv.SubTotal = $("#subtotal").val();
        objInv.Total = $("#total").val();
        objInv.Items = new Array;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').get(i).value != "") {
                objInv.Items[i] = new Object;
                objInv.Items[i].ItemId = $('.partid').get(i).value;
                objInv.Items[i].PartName = $('.partname').get(i).value;
                objInv.Items[i].Description = $('.description').get(i).value;
                objInv.Items[i].Qty = $('.quantity').get(i).value;
                objInv.Items[i].Rate = $('.price').get(i).value;
                objInv.Items[i].Discount = $('.discount').get(i).value;

                objInv.Items[i].Taxes = new Array;
                objInv.Items[i].Taxes = new Object;
                objInv.Items[i].Taxes.name = $("#taxed_" + i + " option[value='" + $('.taxed').get(i).value + "']").text().trim();
                objInv.Items[i].Taxes.value = $('.taxed').get(i).value;
                objInv.Items[i].Amount = $('.amount').get(i).innerText;
                objInv.Items[i].Tax = new Object();
                objInv.Items[i].Tax.Value = $('.taxed').get(i).value;
                objInv.Items[i].Tax.Name = $('.taxed option[value=' + objInv.Items[i].Tax.Value + ']').get(i).text;
                objInv.Items[i].Amount = $('.amount').get(i).value;
            }
        });
        if (objInv.CustomerId == 0) {
            $("#errorCreateInv").text("Silahkan masukkan Nama Pelanggan dengan benar").show();
            return;
        }
        if (objInv.Items.length == 0) {
            $("#errorCreateInv").text("Silahkan masukkan barang di invoice ini").show();
            return;
        }

        $("#errorCreateInv").empty().hide();
        var newInv = JSON.stringify(objInv);
        $.ajax({
            type: 'POST',
            url: '/createinvoice',
            data: { 'invoice': newInv },
            dataType: 'json',
            async: false,
            success: this.CreateInvoiceCallBack
        });
    },
    CreateInvoiceCallBack: function (data) {
        if (data.error == true) {
            $("#errorCreateInv").text(data.message).show();
            return;
        }
    },
    GetDataInvoice: function () {
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
    },
    GetDataInvoiceByID: function (Guid) {
        var dataInvoice = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetDataInvoiceByInvoiceID/_id/' + Guid,
            data: 'json',
            async: false,
            success: function (data) {
                dataInvoice = data;
                var InvoiceDate = new Date(parseInt(data.InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                var DueDate = new Date(parseInt(data.DueDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                dataInvoice.InvoiceDate = $.datepicker.formatDate('dd M yy', InvoiceDate);
                dataInvoice.DueDate = $.datepicker.formatDate('dd M yy', DueDate);
            }
        });
        return dataInvoice;
    },
    UpdateInvoice: function () {
        var length = $("#itemInvoice > tbody > tr").size();
        var objInv = new Object;
        objInv.Customer = $("#selectcust").val();
        objInv.CustomerId = $("#CustomerId").val();
        objInv.PONo = $("#po").val();
        objInv.InvoiceDate = $("#invDate").val();
        objInv.Terms = new Object();
        objInv.Terms.Value = $("#terms").val();
        objInv.Terms.Name = $("#terms option[value='" + objInv.Terms.Value + "']").text().trim();
        objInv.DueDate = $("#dueDate").val();
        objInv.LateFee = $("#latefee").val();
        objInv.Note = $("#custMsg").val();
        objInv.TermCondition = $("#termAndCond").val();
        objInv.SubTotal = $("#subtotal").val();
        objInv.Total = $("#total").val();
        objInv.Items = new Array;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').get(i).value != "") {
                objInv.Items[i] = new Object;
                objInv.Items[i].ItemId = $('.partid').get(i).value;
                objInv.Items[i].PartName = $('.partname').get(i).value;
                objInv.Items[i].Description = $('.description').get(i).value;
                objInv.Items[i].Qty = $('.quantity').get(i).value;
                objInv.Items[i].Rate = $('.price').get(i).value;
                objInv.Items[i].Discount = $('.discount').get(i).value;

                objInv.Items[i].Taxes = new Array;
                objInv.Items[i].Taxes = new Object;
                objInv.Items[i].Taxes.name = $("#taxed_" + i + " option[value='" + $('.taxed').get(i).value + "']").text().trim();
                objInv.Items[i].Taxes.value = $('.taxed').get(i).value;
                objInv.Items[i].Amount = $('.amount').get(i).innerText;
                objInv.Items[i].Tax = new Object();
                objInv.Items[i].Tax.Value = $('.taxed').get(i).value;
                objInv.Items[i].Tax.Name = $('.taxed option[value=' + objInv.Items[i].Tax.Value + ']').get(i).text;
                objInv.Items[i].Amount = $('.amount').get(i).value;
            }
        });
        if (objInv.CustomerId == 0) {
            $("#errorCreateInv").text("Silahkan masukkan Nama Pelanggan dengan benar").show();
            return;
        }
        if (objInv.Items.length == 0) {
            $("#errorCreateInv").text("Silahkan masukkan barang di invoice ini").show();
            return;
        }

        $("#errorCreateInv").empty().hide();
        var newInv = JSON.stringify(objInv);
        $.ajax({
            type: 'POST',
            url: '/UpdateInvoice',
            data: { 'invoice': newInv },
            dataType: 'json',
            async: false,
            success: this.CreateInvoiceCallBack
        });
    }

})
});