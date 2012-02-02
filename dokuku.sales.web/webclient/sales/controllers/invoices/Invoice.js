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
        part.Rate = part.Rate / $("#custRate").val();
        part.Rate = part.Rate.toFixed(2)
        $("#partid_" + index).val(part._id);
        $("#part_" + index).val(part.Name);
        $("#desc_" + index).text(part.Description);
        $("#qty_" + index).val('1.00');
        $("#rate_" + index).val(part.Rate);
        $("#disc_" + index).val('0.00');

        $("#taxed_" + index + " option").each(function (i) {
            if ($(this).text() == part.Tax.Name)
                $(this).attr('selected', true);
        });
        
        $("#amounttext_" + index).text(String.format("{0:C}", part.Rate));
        $("#amount_" + index).val(part.Rate);
        $("#itemInvoice tbody tr#tr_" + index).removeClass('errItemNotFound');
    },
    CalculateByRate: function (rate) {
        var subtotal = 0;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').get(i).value != "" && $('.amount').get(i).value != "") {
                var index = $('#itemInvoice tbody tr').get(i).id;
                var index = index.split('_')[1];
                $this.CalculateItemOnChangeRate(index, rate);
                //$this.RecalculateTax(index);
                $this.RecalculateTaxOnChangeRate(index);
            }
        });
    },
    CalculateItemOnChangeRate: function (index, ccy) {
        var Rate = $("#baseprice_" + index).val() / $("#custRate").val();
        Rate = Rate.toFixed(2);
        $("#rate_" + index).val(Rate);
        var amount = inv.CalculateAmountPerItem($("#qty_" + index).val(), Rate, $("#disc_" + index).val());
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
        objInv.BaseCcy = $("#baseCcy").val();
        objInv.Currency = $("#custCcyCode").val();
        objInv.Items = new Array;
        $('#itemInvoice tbody tr').each(function (i) {
            if ($('.partname').length != 0) {
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
            } else {
                return;
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
        return JSON.stringify(objInv);
    },
    CreateInvoiceCallBack: function (data) {
        if (data.error == true) {
            $("#errorCreateInv").text(data.message).show();
            return;
        } else {
            $("#body").sales_invoices_invoicedetail('load', data);
        }
    },
    GetDataInvoice: function (start, limit) {
        var dataInvoice = new Array();
        $.ajax({
            type: 'GET',
            url: '/GetDataInvoiceToPaging/' + start + '/' + limit + '',
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
    SearchInvoice: function () {
        var dataInvoice = new Array();
        var key = $('#SearchInvoice').val();
        $.ajax({
            type: 'GET',
            url: '/SearchInvoice/' + key,
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
    DeleteInvoice: function (invoiceNo) {
        var result;
        $.ajax({
            type: 'DELETE',
            url: '/deleteInvoice/' + invoiceNo,
            dataType: 'json',
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    },
    ApproveInvoiceByID: function (invoiceID) {
        var result;
        $.ajax({
            type: 'POST',
            url: '/approveinvoice/' + invoiceID,
            dataType: 'json',
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    },
    SearchCustomer: function (key) {
        var listCustomer = null;
        $.ajax({
            type: 'GET',
            url: '/SearchCustomer/' + key,
            dataType: 'json',
            failure: $('#DivSearchCustomer').hide(),
            async: false,
            success: function (data) {
                listCustomer = data;
            }
        });
        return listCustomer;
    },
    CancelInvoiceByID: function (invoiceID, Note) {
        var result;
        $.ajax({
            type: 'POST',
            url: '/cancelinvoice/' + invoiceID,
            data: { 'Note': Note },
            dataType: 'json',
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    },
    ForceCancelInvoiceByID: function (invoiceID, Note) {
        var result;
        $.ajax({
            type: 'POST',
            url: '/forcecancelinvoice/' + invoiceID,
            dataType: 'json',
            data: { 'Note': Note },
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    },
    HideList: function (Status, index) {
        if (Status == "Draft") {
            this.HideActionList("ffftt", index);
        } else if (Status == "Belum Bayar") {
            this.HideActionList("ftfft", index);
        } else if (Status == "Belum Lunas") {
            this.HideActionList("ttftt", index);
        } else if (Status == "Sudah Lunas") {
            this.HideActionList("tttft", index);
        } else if (Status == "Batal") {
            this.HideActionList("ttttt", index);
        }
    },
    HideActionList: function (srcPattern, index) {
        var str = srcPattern;

        if (str.substring(0, 1) == "t") {
            this.Menu("div#actionEdit", index);
        }

        if (str.substring(1, 2) == "t") {
            this.Menu("div#actionApprove", index);
        }

        if (str.substring(3, 4) == "t") {
            this.Menu("div#actionCancel", index);
        }

        if (str.substring(4, 5) == "t") {
            this.Menu("div#actionForceCancel", index);
        }
    },
    Menu: function (Name, index) {
        var result;
        var Menu = "tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice";

        result = $(Menu + " " + Name).remove();
    },
    CalculateByTax: function () {
        var optClear = $('.taxed').get(0).id;
        $('#' + optClear + ' option').each(function (n) {
            $('#' + $('#' + optClear + ' option').get(n).text).val(0);
        });

        $('.taxed').each(function (i) {
            if ($('.amount').get(i).value != "") {
                var namapajak = $('.taxed :selected').get(i).text;
                var nilaipajak = $("#" + namapajak).val();
                var nilaiygditambah = $('.taxedAmt').get(i).value == "" ? 0 : $('.taxedAmt').get(i).value; //$('.taxedAmt').get(i).value;
                var total = parseFloat(nilaipajak) + parseFloat(nilaiygditambah);
                $("#" + namapajak).val(total);
            }
        });
    },
    RecalculateTax: function (element) {
        var index = element.attr("id").split('_')[1];
        res = this.SetTaxAmount(index);
        $("#taxedAmt_" + index).val(res);
        this.CalculateByTax();
    },
    RecalculateTaxOnChangeRate: function (index) {
        res = this.SetTaxAmount(index);
        $("#taxedAmt_" + index).val(res);
        this.CalculateByTax();
    },
    SetTaxAmount: function (index) {
        var NilaiTax = $("#taxed_" + index).val();
        var NamaTax = $("#taxed_" + index + " :selected").text();
        var Jumlah = $("#amount_" + index).val();
        var JumlahTax = (Jumlah * NilaiTax) / 100;
        var cek = "#amount_" + index;
        var result;

        if ($(cek).val().length < 1) {
            result = 0;
        } else {
            result = JumlahTax;
        }
        return result;
    }

})
    });
 
