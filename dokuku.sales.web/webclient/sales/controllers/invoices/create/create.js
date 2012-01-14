steal('jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
       'sales/controllers/invoices/Invoice.js',
	   'sales/models')
	.then('./views/createinvoices.ejs', function ($) {
	    $.Controller('Sales.Invoices.Create',
        {
            defaults: (custid = 0, tabIndexTr = 0, subtotal = 0, total = 0, tax = 0, $this = null, inv = null)
        },
        {
            init: function () {
                $this = this;
                inv = new Invoice();
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
                this.CreateListItem(3);
            },
            load: function () {
                tabIndexTr = 0;
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
                this.CreateListItem(3);
            },
            '#selectcust change': function (el, ev) {
                $("#keteranganSelectCust").empty();
                var custName = el.val();
                if (custName == "") {
                    $("#keteranganSelectCust").text("Pelanggan harus di pilih");
                    $("#selectcust").focus().select();
                }
                else {
                    if (inv.GetCustomer(custName) != null) {
                        $("#keteranganSelectCust").text(data.Currency);
                        custid = data._id;
                    }
                    $("#keteranganSelectCust").text("Pelanggan ini tidak ditemukan");
                    $("#selectcust").focus().select();
                }
            },
            '#addItemRow click': function () {
                this.CreateListItem(1);
            },
            '#itemInvoice tbody tr hover': function (el) {
                var index = el.attr('tabindex');
                $("#deleteItem_" + index).show();
            },
            "#itemInvoice tbody tr mouseleave": function (el) {
                var index = el.attr("tabindex");
                $("#deleteItem_" + index).hide();
            },
            ".clsDeleteItem click": function (el) {
                if ($("#itemInvoice > tbody > tr").size() == 1) {
                    return;
                }
                var index = el.attr('id').split('_')[1];
                $("#itemInvoice tbody tr#tr_" + index + "").remove();
            },
            '.partname change': function (el) {
                var value = el.val();
                var index = el.attr("id").split('_')[1];
                $.ajax({
                    type: 'GET',
                    url: '/getItemByName/' + value,
                    dataType: 'json',
                    success: function (data) {
                        if (data != null) {
                            $("#part_" + index).val(data.Name);
                            $("#desc_" + index).text(data.Description);
                            $("#qty_" + index).val('1.00');
                            $("#rate_" + index).val(data.Rate);
                            $("#disc_" + index).val('0.00');
                            $("#amount_" + index).text(data.Rate);
                            $("#itemInvoice tbody tr#tr_" + index).removeClass('errItemNotFound');

                            subtotals = $this.GetGridTotal;
                            $("#subtotal").text(subtotals);

                            //subtotal += data.Rate;
                            //$("#subtotal").text(subtotal);
                            return;
                        }
                        $this.ClearItemField;
                        $("#itemInvoice tbody tr#tr_" + index).addClass('errItemNotFound');
                    }
                });
            },
            '.qty change': function (el) {
                var index = el.attr("id").split('_')[1];
                this.CalculateItem(index);
            },
            '.rate change': function (el) {
                var index = el.attr("id").split('_')[1];
                this.CalculateItem(index);
            },
            '.disc change': function (el) {
                var index = el.attr("id").split('_')[1];
                this.CalculateItem(index);
            },
            CalculateItem: function (index) {
                var qty = $("#qty_" + index).val();
                var rate = $("#rate_" + index).val();
                var disc = $("#disc_" + index).val();
                var amount = inv.CalculateAmountPerItem(qty, rate, disc);
                $("#amount_" + index).text(amount);
            },
            ClearItemField: function () {
                $("#desc_" + index).empty();
                $("#qty_" + index).empty();
                $("#rate_" + index).empty();
                $("#disc_" + index).empty();
                $("#amount_" + index).empty();
            },

            LoadTax: function (index) {
                $("#taxed_" + index).append("<option value=1>None</option><option value=1>1</option>" +
                                "<option value=2>2</option>");
            },
            CreateListItem: function (count) {
                while (count > 0) {
                    $("#itemInvoice tbody").append("<tr id='tr_" + tabIndexTr + "' tabindex='" + tabIndexTr + "'>" +
                                    "<td><input type='text' name='part' class='partname' id='part_" + tabIndexTr + "'/></td>" +
                                    "<td><textarea name='desc' id='desc_" + tabIndexTr + "'></textarea></td>" +
                                    "<td><input type='text' name='qty' class='qty right' id='qty_" + tabIndexTr + "'></input></td>" +
                                    "<td><input type='text' name='rate' class='rate right' id='rate_" + tabIndexTr + "'></input></td>" +
                                    "<td><input type='text' name='disc' class='disc right' id='disc_" + tabIndexTr + "'></input></td>" +
                                    "<td><select name='taxed' class='taxed' id='taxed_" + tabIndexTr + "'>" +
                                    "</select></td>" +
                                    "<td><span class='amount' id='amount_" + tabIndexTr + "'></span></td>" +
                                    "<td valign='middle'><div class='clsDeleteItem' id='deleteItem_" + tabIndexTr + "'>X</div></td></tr>");
                    this.LoadTax(tabIndexTr);
                    count--;
                    tabIndexTr++;
                }
            },
            GetGridTotal: function () {
                var length = $("#itemInvoice > tbody > tr").size();
                var total = 0;
                for (var i = 0; i < length; i++) {
                    if ($("#part_" + i).val() != "") {
                        total = total + $("#amount_" + i).val();
                    }
                }

                return total
            },
            '#NewInvoiceSave click': function () {
                var length = $("#itemInvoice > tbody > tr").size();
                var JSONObject = new Object;
                JSONObject.custname = $("#selectcust").val();
                JSONObject.po = $("#po").val();
                JSONObject.items = new Array;

                for (var i = 0; i < length; i++) {
                    if ($("#part" + i).val() != "" && $("#desc" + i).val() != "") {
                        JSONObject.items[i] = new Object;
                        JSONObject.items[i].part = $("#part" + i).val();
                        JSONObject.items[i].desc = $("#desc" + i).val();
                    }
                }
                JSONstring = JSON.stringify(JSONObject);
            }
        })
	});