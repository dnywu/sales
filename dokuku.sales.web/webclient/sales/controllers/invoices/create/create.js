steal('jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
       'sales/controllers/invoices/Invoice.js',
       'sales/repository/ItemRepository.js',
       'sales/repository/CustomerRepository.js',
	   'sales/models')
	.then('./views/createinvoices.ejs', function ($) {
	    $.Controller('Sales.Invoices.Create',
        {
            defaults: (custid = 0, tabIndexTr = 0,
                        $this = null,
                        inv = null,
                        itmRepo = null,
                        custRepo = null)
        },
        {
            init: function () {
                $this = this;
                inv = new Invoice();
                itmRepo = new ItemRepository();
                custRepo = new CustomerRepository();
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
                var dataCust = custRepo.GetCustomerByName(el.val());
                if (dataCust != null) {
                    $("#keteranganSelectCust").text(dataCust.Currency);
                    custid = dataCust._id;
                }
                $("#keteranganSelectCust").text("Pelanggan ini tidak ditemukan");
                $("#selectcust").focus().select();
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
                if ($("#itemInvoice > tbody > tr").size() == 1)
                    return;
                var index = el.attr('id').split('_')[1];
                $("#itemInvoice tbody tr#tr_" + index + "").remove();
                $this.GetSubTotal();
            },
            '.partname change': function (el) {
                var partName = el.val();
                var index = el.attr("id").split('_')[1];
                var part = itmRepo.GetItemByName(partName);

                if (part != null) {
                    $("#part_" + index).val(part.Name);
                    $("#desc_" + index).text(part.Description);
                    $("#qty_" + index).val('1.00');
                    $("#rate_" + index).val(part.Rate);
                    $("#disc_" + index).val('0.00');
                    $("#amount_" + index).text(part.Rate);
                    $("#itemInvoice tbody tr#tr_" + index).removeClass('errItemNotFound');
                    $this.GetSubTotal();
                    return;
                }
                $this.ClearItemField;
                $("#itemInvoice tbody tr#tr_" + index).addClass('errItemNotFound');
            },
            '.qty change': function (el) {
                this.CalculateItem(el);
            },
            '.rate change': function (el) {
                this.CalculateItem(el);
            },
            '.disc change': function (el) {
                this.CalculateItem(el);
            },
            CalculateItem: function (element) {
                var index = element.attr("id").split('_')[1];
                var qty = $("#qty_" + index).val();
                var rate = $("#rate_" + index).val();
                var disc = $("#disc_" + index).val();
                var amount = inv.CalculateAmountPerItem(qty, rate, disc);
                $("#amount_" + index).text(amount);
                $this.GetSubTotal();
            },
            ClearItemField: function () {
                $("#desc_" + index).empty();
                $("#qty_" + index).empty();
                $("#rate_" + index).empty();
                $("#disc_" + index).empty();
                $("#amount_" + index).empty();
            },
            LoadTax: function (index) {
                $("#taxed_" + index).append("<option value=1>None</option>");
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
            GetSubTotal: function () {
                $("#subtotal").text(inv.CalculateSubTotal);
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