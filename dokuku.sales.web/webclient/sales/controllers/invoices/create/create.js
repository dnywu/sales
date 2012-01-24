steal('jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
       'sales/scripts/stringformat.js',
       'sales/controllers/invoices/Invoice.js',
       'sales/controllers/invoices/AddCustomer.js',
       'sales/controllers/invoices/AddItem.js',
       'sales/scripts/jquery-ui-1.8.11.min.js',
       'sales/styles/jquery-ui-1.8.14.custom.css',
       'sales/repository/ItemRepository.js',
       'sales/repository/CustomerRepository.js',
	   'sales/models')
	.then('./views/createinvoices.ejs',
          './views/AddCustomer.ejs',
          './views/AddItem.ejs', function ($) {
              $.Controller('Sales.Invoices.Create',
        {
            defaults: (tabIndexTr = 0,
                        $this = null,
                        inv = null,
                        itmRepo = null,
                        custRepo = null,
                        baseCcy = null,
                        isDifferentCcy = true)
        },
        {
            init: function (ev, el, customer) {
                $this = this;
                inv = new Invoice();
                itmRepo = new ItemRepository();
                custRepo = new CustomerRepository();
                this.load(customer);
                this.SetCurrency();
                this.load(customer);
            },
            load: function (customer) {
                tabIndexTr = 0;
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs", customer));
                if (customer != null)
                    $("#currency").text(customer.Currency).show();
                this.CreateListItem(3);
                this.SetDatePicker();
                this.SetDefaultDate();
            },
            '#terms change': function (el) {
                var invDate = $("#invDate").val();
                var dueDate = new Date(invDate);
                dueDate.setDate(dueDate.getDate() + parseInt(el.val()));
                $("#dueDate").val($.datepicker.formatDate('dd M yy', dueDate));
            },
            '#tambahPelanggan click': function () {
                new ModalDialog("Tambah Pelanggan Baru");
                $("#dialogContent").html(this.view("//sales/controllers/invoices/create/views/AddCustomer.ejs"));
                var addCust = new AddCustomer();
                addCust.TriggerEvent();
            },
            '.additem click': function (el, ev) {
                new ModalDialog("Tambah Barang Baru");
                $("#dialogContent").html(this.view("//sales/controllers/invoices/create/views/AddItem.ejs"));
                var addItem = new AddItem(el.attr("id").split('_')[1]);
                addItem.TriggerEvent();
            },
            '#selectcust change': function (el, ev) {
                isDifferentCcy = true;
                $("#divExchangeRate").hide();
                $("#custCcyCode").val(baseCcy);
                $("#keteranganSelectCust").empty();
                this.ShowCurrencyToView();
                var dataCust = custRepo.GetCustomerByName(el.val());
                if (dataCust != null) {
                    if (dataCust.Currency != baseCcy) {
                        isDifferentCcy = false;
                        $("#divExchangeRate").show();
                        //this.ShowExchangRate(dataCust.Currency, baseCcy);
                    } else {
                        $("#divExchangeRate").hide();
                    }

                    this.ShowExchangRate(dataCust.Currency, baseCcy);
                    $("#selectcust").val(dataCust.Name);
                    $("#currency").text(dataCust.Currency).show();
                    $("#CustomerId").val(dataCust._id);
                    return;
                }
                $("#CustomerId").val("0");
                $("#currency").hide();
                $("#keteranganSelectCust").text("Pelanggan '" + el.val() + "' tidak ditemukan");
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
                this.GetSubTotal();
                this.GetTotal();
            },
            '.partname focus': function (el) {
                var index = el.attr("id").split('_')[1];
                $(".resultItemDiv").remove();
                $("<div class='resultItemDiv'id='resultItemDiv_" + index + "'><table id='itemList'></table></div>").insertAfter("#tr_" + index + " td:first-child input.partname");
            },

            '.partname change': function (el) {
                var partName = el.val();
                var index = el.attr("id").split('_')[1];
                var part = itmRepo.GetItemByName(partName);
                if (part != null) {
                    inv.ShowListItem(part, index);
                    this.GetSubTotal();
                    this.GetTotal();
                    $("#additem_" + index).hide();
                    return;
                }
                this.ClearItemField(index);
                $("#additem_" + index).show();
                this.GetSubTotal();
                this.GetTotal();
                $("#itemInvoice tbody tr#tr_" + index).addClass('errItemNotFound');
            },
            '.partname keyup': function (el) {
                var searchResultList = null;
                var searchResult = itmRepo.SearchItem(el.val());
                $(".resultItemDiv").show();
                $("#itemList").empty();
                $.each(searchResult, function (index) {
                    searchResultList = $("<tr class='itemTr'><td class='itemTd' id='" + index + "'><div id='itemName" + index + "'>" + searchResult[index].Name + "</div></td></tr>");
                    searchResultList.appendTo($("#itemList"));
                });
            },
            '.itemTd click': function (el) {
                var index = el.attr("id");
                $('#part_' + $('.resultItemDiv').attr("id").split('_')[1]).val($("div#itemName" + index).text());
                $(".partname").change();
                $(".resultItemDiv").remove();
            },
            '.resultItemDiv mouseleave': function () {
                $(".resultItemDiv").remove();
            },
            '.quantity change': function (el) {
                this.CalculateItem(el);
            },
            '.price change': function (el) {
                this.CalculateItem(el);
            },
            '.discount change': function (el) {
                this.CalculateItem(el);
            },
            '#formNewIvoice submit': function (el, ev) {
                ev.preventDefault();
                inv.CreateNewInvoice();
            },
            '#btnCancelInvoice click': function () {
                $("#body").sales_invoices_list('load');
            },
            '#custRate change': function () {
                inv.CalculateByRate($("#custRate").val());
            },
            CalculateItem: function (element) {
                var index = element.attr("id").split('_')[1];
                var qty = $("#qty_" + index).val();
                var rate = $("#rate_" + index).val();
                var disc = $("#disc_" + index).val();
                var amount = inv.CalculateAmountPerItem(qty, rate, disc);
                $("#amount_" + index).val(amount);
                $("#amounttext_" + index).text(String.format("{0:C}", amount));
                this.GetSubTotal();
                this.GetTotal();
            },
            ClearItemField: function (index) {
                $("#desc_" + index).empty();
                $("#qty_" + index).val('');
                $("#rate_" + index).val('');
                $("#disc_" + index).val('');
                $("#amounttext_" + index).empty();
                $("#amount_" + index).val('');
            },
            LoadTax: function (index) {
                $("#taxed_" + index).append("<option value='0'>None</option><option value='1'>A</option>");
            },
            CreateListItem: function (count) {
                while (count > 0) {
                    $("#itemInvoice tbody").append("<tr id='tr_" + tabIndexTr + "' tabindex='" + tabIndexTr + "'>" +
                                    "<td><input type='text' name='part' class='partname' id='part_" + tabIndexTr + "'/>" +
                                    "<input type='hidden' class='partid' id='partid_" + tabIndexTr + "'/>" +
                                    "<label class='additem' id='additem_" + tabIndexTr + "'>Tambah Barang</label></td>" +
                                    "<td><textarea name='description' class='description' id='desc_" + tabIndexTr + "'></textarea></td>" +
                                    "<td><input type='text' name='quantity' class='quantity right' id='qty_" + tabIndexTr + "'></input></td>" +
                                    "<td><input type='text' name='price' class='price right' id='rate_" + tabIndexTr + "'></input>" +
                                    "<input type='text' class='baseprice' id='baseprice_" + tabIndexTr + "'/></td>" +
                                    "<td><input type='text' name='discount' class='discount right' id='disc_" + tabIndexTr + "'></input></td>" +
                                    "<td><select name='taxed' class='taxed' id='taxed_" + tabIndexTr + "'>" +
                                    "</select></td>" +
                                    "<td class='right'><span class='amounttext' id='amounttext_" + tabIndexTr + "'></span>" +
                                    "<input type='hidden' class='amount' id='amount_" + tabIndexTr + "'/></td>" +
                                    "<td valign='middle'><div class='clsDeleteItem' id='deleteItem_" + tabIndexTr + "'>X</div></td></tr>");
                    this.LoadTax(tabIndexTr);
                    count--;
                    tabIndexTr++;
                }
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
            SetDatePicker: function () {
                var dates = $("#invDate, #dueDate").datepicker({ dateFormat: 'dd M yy',
                    defaultDate: "+1w",
                    changeMonth: true,
                    numberOfMonths: 1,
                    onSelect: function (selectedDate) {
                        var option = this.id == "invDate" ? "" : "",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                        if (this.id == "invDate") {
                            var currdate = new Date(date);
                            var term = $("#terms").val();
                            currdate.setDate(currdate.getDate() + parseInt(term));
                            dates.not(this).val($.datepicker.formatDate('dd M yy', currdate));
                        }
                    }
                });
            },
            SetDefaultDate: function () {
                var currdate = new Date();
                var term = $("#terms").val();
                var dueDate = currdate;
                $("#invDate").val($.datepicker.formatDate('dd M yy', currdate));
                dueDate.setDate(dueDate.getDate() + parseInt(term));
                $("#dueDate").val($.datepicker.formatDate('dd M yy', dueDate));
            },
            SetCurrency: function () {
                Sales.Models.Currency.findOne({ id: '1' }, function (data) {
                    baseCcy = data.curr;
                });
            },
            ShowCurrencyToView: function () {
                $("#curr").text(baseCcy);
            },
            ShowExchangRate: function (custCcy, baseCcy) {
                $("#curr").text(custCcy);

                $("#custCcy").val("1 " + custCcy + " =");
                $("#baseCcy").val(baseCcy);
                $("#custCcyCode").val(custCcy);
            }
        })
          });