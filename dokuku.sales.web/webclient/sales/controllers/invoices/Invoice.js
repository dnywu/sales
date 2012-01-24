steal('jquery/class', 'sales/scripts/stringformat.js',
      'sales/repository/InvoiceRepository.js',
    function () {
        $.Class('Invoice',
{
    defaults: (invRepo = null, $this = null)
},
{
    init: function () {
        $this = this;
        invRepo = new InvoiceRepository();
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
        $("#baseprice_" + index).val(part.Rate);
        if (!isDifferentCcy) {
            part.Rate = part.Rate / $("#custRate").val();
            part.Rate = part.Rate * 100;
            part.Rate = Math.ceil(part.Rate.toFixed(2)) / 100;
        }
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
    CalculateByRate: function (rate) {
        var subtotal = 0;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').get(i).value != "" && $('.amount').get(i).value != "") {
                var index = $('#itemInvoice tbody tr').get(i).id;
                var index = index.split('_')[1];
                $this.CalculateItem_onChangeRate(index, rate);
            }
        });
    },
    CalculateItem_onChangeRate: function (index, ccy) {
        var Rate = $("#baseprice_" + index).val() / $("#custRate").val();
        Rate = Rate * 100;
        Rate = Math.ceil(Rate.toFixed(2)) / 100;
        $("#rate_" + index).val(Rate);
        var amount = inv.CalculateAmountPerItem($("#qty_"  + index).val(), Rate, $("#disc_" + index).val());
        $("#amount_" + index).val(amount);
        $("#amounttext_" + index).text(String.format("{0:C}", amount));
        $this.GetSubTotal();
        $this.GetTotal();
    },
    GetSubTotal: function () {
        var subtotal = inv.CalculateSubTotal();
        $("#subtotaltext").text(String.format("{0:C}", subtotal));
        $("#subtotal").val(subtotal);
    },
    GetTotal: function () {
        var total = inv.CalculateTotal();
        $("#totaltext").text(String.format("{0:C}", total));
        $("#total").val(total);
    },
    CreateNewInvoice: function () {
        var invoice = this.GetInvoiceDataFromView();
        $.ajax({
            type: 'POST',
            url: '/createinvoice',
            data: { 'invoice': invoice },
            dataType: 'json',
            async: false,
            success: this.CreateInvoiceCallBack
        });
    },
    CreateInvoiceCallBack: function (data) {
        if (data.error == true) {
            $("#errorCreateInv").text(data.message).show();
            return;
        } else {
            $("#body").sales_invoices_invoicedetail('load', data);
        }
    },
    UpdateInvoice: function () {
        var invoice = this.GetInvoiceDataFromView();
        var invId = $("#invoiceId").val();
        $.ajax({
            type: 'POST',
            url: '/UpdateInvoice',
            data: { 'invoice': invoice },
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.error == true) {
                    $("#errorUpdateInv").text(data.message).show();
                    return;
                }
                var invoice = invRepo.GetInvoiceById(invId);
                $("#body").sales_invoices_invoicedetail('load', invoice);
            }
        });
    },
    GetInvoiceDataFromView: function () {
        var length = $("#itemInvoice > tbody > tr").size();
        var objInv = new Object;
        objInv._id = $("#invoiceId").val();
        objInv.Customer = $("#selectcust").val();
        objInv.CustomerId = $("#CustomerId").val();
        objInv.PONo = $("#po").val();
        objInv.InvoiceNo = $("#InvoiceNo").val();
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
        objInv.ExchangeRate = $("#custRate").val();
        objInv.Currency = $("#custCcyCode").val();
        objInv.Items = new Array;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').get(i).value != "" && $('.amount').get(i).value != "") {
                objInv.Items[i] = new Object;
                objInv.Items[i].ItemId = $('.partid').get(i).value;
                objInv.Items[i].PartName = $('.partname').get(i).value;
                objInv.Items[i].Description = $('.description').get(i).value;
                objInv.Items[i].Qty = $('.quantity').get(i).value;
                objInv.Items[i].BaseRate = $('.baseprice').get(i).value;
                objInv.Items[i].Rate = $('.price').get(i).value;
                objInv.Items[i].Discount = $('.discount').get(i).value;
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
        return newInv;
    }
})
    });