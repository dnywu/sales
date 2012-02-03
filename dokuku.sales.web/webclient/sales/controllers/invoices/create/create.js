steal('jquery',
       'jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
       'sales/scripts/stringformat.js',
       'sales/controllers/invoices/InvoiceClass.js',
       'sales/controllers/invoices/AddCustomer.js',
       'sales/controllers/invoices/AddItem.js',
       'sales/scripts/jquery-ui-1.8.11.min.js',
       'sales/styles/jquery-ui-1.8.14.custom.css',
       'sales/repository/ItemRepository.js',
       'sales/repository/CustomerRepository.js',
       'sales/repository/CurrencyandTaxRepository.js',
       'sales/scripts/ModalDialog.js',
       'sales/styles/ModalDialog.css',
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
                        curTaxRepo = null,
                        baseCcy = null,
                        isDifferentCcy = true)
        },
        {
            init: function (ev, el, customer) {
                $this = this;
                inv = new Invoiceclass();
                itmRepo = new ItemRepository();
                custRepo = new CustomerRepository();
                curTaxRepo = new CurrencyandTaxRepository();
                this.load(customer);
                this.SetCurrency();
                this.load(customer);
            },
            load: function (customer) {
                tabIndexTr = 0;
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs", customer));
                if (customer != null)
                    $("#currency").text(customer.Currency).show();
                this.SetCurrency();
                this.CreateListItem(3);
                this.CreateRowTax();
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
                $("#dialogContent").empty();
                $("#dialogContent").html(this.view("//sales/controllers/invoices/create/views/AddCustomer.ejs"));
                var addCust = new AddCustomer();
                addCust.TriggerEvent();
            },
            '.additem click': function (el, ev) {
                new ModalDialog("Tambah Barang Baru");
                $("#dialogContent").empty();
                $("#dialogContent").html(this.view("//sales/controllers/invoices/create/views/AddItem.ejs"));
                var addItem = new AddItem(el.attr("id").split('_')[1]);
                addItem.TriggerEvent();
            },
            //            '#selectcust change': function (el, ev) {
            //                this.CheckNameCutomer(el.val());
            //            },
            CheckNameCutomer: function (name) {
                isDifferentCcy = true;
                $("#divExchangeRate").hide();
                $("#custCcyCode").val(baseCcy);
                $("#keteranganSelectCust").empty();
                this.ShowCurrencyToView();
                var dataCust = custRepo.GetCustomerByName(name);
                if (dataCust != null) {
                    if (dataCust.Currency != baseCcy) {
                        isDifferentCcy = false;
                        $("#divExchangeRate").show();
                    } else {
                        $("#divExchangeRate").hide();
                    }
                    this.ShowExchangRate(dataCust.Currency, baseCcy);
                    $("#selectcust").val(dataCust.Name);
                    $("#currency").text(dataCust.Currency).show();
                    $("#CustomerId").val(dataCust._id);
                    $("#custRate").val(1);
                    $("#custRate").change();
                    return;
                }
                $("#CustomerId").val("0");
                $("#currency").hide();
                $("#keteranganSelectCust").text("Pelanggan '" + name + "' tidak ditemukan");
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
                inv.CalculateByTax();
            },
            //            '.partname change': function (el) {
            //                var partName = el.val();
            //                var index = el.attr("id").split('_')[1];
            //                this.FillItemAtribut(partName, index);
            //            },
            '.partname keyup': function (el, ev) {
                if (el.val() != "") {
                    var limit = 0;
                    if (ev.keyCode == 13) {
                        var partName = $("#itemList tr td.selected div.itemName").text();
                        el.val(partName);
                        var index = el.attr("id").split('_')[1];
                        this.FillItemAtribut(partName, index);

                        $(".resultItemDiv").remove();
                        el.trigger("blur");
                        $("#qty_" + index).trigger("focus");
                    } else {
                        var index = el.attr("id").split('_')[1];
                        if (ev.keyCode == 40) {
                            var indexposition = $(".selected").attr("tabindex") + 1;
                            limit = $("#itemList tr").length;
                            indexposition++;
                            if (limit < indexposition)
                                indexposition = 1;
                            $("#itemList tr td").removeClass("selected");
                            $("#itemList tr:nth-child(" + indexposition + ") td").addClass("selected");
                        } else if (ev.keyCode == 38) {
                            var indexposition = $(".selected").attr("tabindex") + 1;
                            limit = $("#itemList tr").length;
                            indexposition--;
                            if (indexposition < 1)
                                indexposition = limit;
                            $("#itemList tr td").removeClass("selected");
                            $("#itemList tr:nth-child(" + indexposition + ") td").addClass("selected");
                        } else {
                            var searchResultList = null;
                            if ($(".resultItemDiv").length == 0) {
                                $(".resultItemDiv").remove();
                                $("<div class='resultItemDiv'id='resultItemDiv_" + index + "'><table id='itemList'></table></div>").insertAfter("#tr_" + index + " td:first-child input.partname");
                            }
                            var searchResult = itmRepo.SearchItem(el.val());
                            this.RenderToSearchList(searchResult);
                            $("#itemList tr:nth-child(1) td").addClass("selected");
                        }
                    }
                }
            },
            '.itemTd hover': function (el) {
                $("#itemList tr td").removeClass("selected");
                el.addClass("selected");
            },
            '.itemTd click': function (el) {
                var index = el.attr("id");
                var fieldIndex = $('.resultItemDiv').attr("id").split('_')[1];
                var partName = $("div#itemName" + index).text();

                $('#part_' + fieldIndex).val(partName);
                this.FillItemAtribut(partName, fieldIndex);
                $(".resultItemDiv").remove();
            },
            '.quantity focus': function (el) {
                el.select();
            },
            '.quantity change': function (el) {
                this.CalculateItem(el);

                inv.RecalculateTax(el);
            },
            '.price change': function (el) {
                this.CalculateItem(el);

                inv.RecalculateTax(el);
            },
            '.discount change': function (el) {
                this.CalculateItem(el);

                inv.RecalculateTax(el);
            },
            //            '#formNewIvoice submit': function (el, ev) {
            //                ev.preventDefault();
            //                inv.CreateNewInvoice();
            //            },
            '#NewInvoiceSave click': function () {
                inv.CreateNewInvoice();
            },
            '#btnCancelInvoice click': function () {
                $("#body").sales_invoices_list('load');
            },
            '#custRate change': function () {
                inv.CalculateByRate($("#custRate").val());
            },
            '.taxed change': function (el) {
                var index = el.attr("id").split('_')[1];
                var res;

                res = inv.SetTaxAmount(index);
                $("#taxedAmt_" + index).val(res);

                inv.CalculateByTax();
            },
            CalculateItem: function (element) {
                var index = element.attr("id").split('_')[1];
                var qty = $("#qty_" + index).val();
                var rate = $("#rate_" + index).val();
                var disc = $("#disc_" + index).val();
                var amount = inv.CalculateAmountPerItem(qty, rate, disc);
                $("#amount_" + index).val(amount.toFixed(2));
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
            loadDataTax: function (index) {
                var tax = curTaxRepo.getAllTax();

                $.each(tax, function (i) {
                    $("#taxed_" + index).append("<option value='" + tax[i].Value + "'>" + tax[i].Name + "</option>");
                });
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
                                    "<input type='hidden' class='baseprice' id='baseprice_" + tabIndexTr + "'/></td>" +
                                    "<td><input type='text' name='discount' class='discount right' id='disc_" + tabIndexTr + "'></input></td>" +
                                    "<td><select name='taxed' class='taxed' id='taxed_" + tabIndexTr + "'><option value='0'>NONE</option></select>" +
                                    "<input type='text' class='taxedAmt' id='taxedAmt_" + tabIndexTr + "'/></td>" +
                                    "<td class='right'><span class='amounttext' id='amounttext_" + tabIndexTr + "'></span>" +
                                    "<input type='hidden' class='amount' id='amount_" + tabIndexTr + "'/></td>" +
                                    "<td valign='middle'><div class='clsDeleteItem' id='deleteItem_" + tabIndexTr + "'>X</div></td></tr>");
                    this.loadDataTax(tabIndexTr);
                    count--;
                    tabIndexTr++;
                }
            },
            CreateRowTax: function () {
                var tax = curTaxRepo.getAllTax();
                var pos;
                $.each(tax, function (i) {
                    pos = i + 1;
                    $("#itemInvoice tfoot tr:nth-child(" + pos + ")").after("<tr><td colspan='4'></td>" +
                    "<td colspan='2' class='right borderbottom'>" + tax[i].Name + "(" + tax[i].Value + "%)</td>" +
                    "<td class='right borderbottom'>" +
                    "<span id='taxValue" + tax[i].Name + "'></span>" +
                    "<input type='text' id='" + tax[i].Name + "'/></td>" +
                    "<td>&nbsp;</td>" +
                    "</tr>");
                });
            },
            GetSubTotal: function () {
                var subtotal = inv.CalculateSubTotal();
                $("#subtotaltext").text(String.format("{0:C}", subtotal));
                $("#subtotal").val(subtotal.toFixed(2));
            },
            GetTotal: function () {
                var total = inv.CalculateTotal();
                $("#totaltext").text(String.format("{0:C}", total));
                $("#total").val(total.toFixed(2));
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
            },
            '#selectcust keyup': function (el, ev) {
                var limit = 0;
                if (ev.keyCode == "13") {
                    var iname = el.val();
                    this.CheckNameCutomer(el.val());
                    $('.DivSearchCustomer').hide();
                    var fields = el.parents('form:eq(0),body').find('button, input, textarea, select');
                    var index = fields.index(el);
                    if (index > -1 && (index + 1) < fields.length) {
                        fields.eq(index + 1).focus();
                    }
                    return false;
                } else {
                    var key = $('#selectcust').val();
                    if (key == "") {
                        $('.DivSearchCustomer').hide();
                    }
                    else {
                        if (ev.keyCode == "38") {
                            var indexposition = parseInt($(".selectedCustomer").attr("tabIndex") + 1);
                            limit = $('#bodySearchCustomer tr').length;
                            indexposition = indexposition - 1;
                            if (indexposition <= 0) {
                                indexposition = limit;
                            }

                            $('#bodySearchCustomer tr td ').removeClass("selectedCustomer");
                            $('#bodySearchCustomer tr:nth-child(' + indexposition + ') td').addClass("selectedCustomer");
                            $('#bodySearchCustomer tr td ').removeClass("selectedCustomer2");
                            $('#bodySearchCustomer tr:nth-child(' + indexposition + ') td').addClass("selectedCustomer2");
                            $('#selectcust').val($('#bodySearchCustomer tr:nth-child(' + indexposition + ') td div.DivNamaCustomer').text());
                        }
                        else if (ev.keyCode == "40") {
                            if ($(".selectedCustomer").length > 1)
                                var indexposition = $(".selectedCustomer").length - 1;
                            else

                                var indexposition = $(".selectedCustomer").attr("tabIndex");

                            indexposition += 1;
                            limit = $('#bodySearchCustomer tr').length;
                            if (indexposition >= limit) {
                                indexposition = 1;
                            } else {
                                indexposition += 1;
                            }
                            $('#bodySearchCustomer tr td ').removeClass("selectedCustomer");
                            $('#bodySearchCustomer tr:nth-child(' + indexposition + ') td').addClass("selectedCustomer");
                            $('#bodySearchCustomer tr td ').removeClass("selectedCustomer2");
                            $('#bodySearchCustomer tr:nth-child(' + indexposition + ') td').addClass("selectedCustomer2");
                            $('#selectcust').val($('#bodySearchCustomer tr:nth-child(' + indexposition + ') td div.DivNamaCustomer').text());
                        }
                        else {
                            var key = $('#selectcust').val();
                            if (key == "") {
                                $('.DivSearchCustomer').hide();
                            }
                            else {
                                var customer = inv.SearchCustomer(key);
                                $('.DivSearchCustomer').show();
                                $("table#tblSearchCustomer tbody#bodySearchCustomer").empty();
                                $.each(customer, function (item) {
                                    $("table#tblSearchCustomer tbody#bodySearchCustomer").append(
                                        '<tr id="trSearchCustomer">' +
                                        '<td id="tdSearchCustomer' + item + '" class="selectedCustomer" style="border-bottom:solid 1px grey" tabIndex = "' + item + '">' +
                                            '<div class="DivNamaCustomer" id="' + customer[item]._id + '">' + customer[item].Name + '</div>' +
                                            '<div class="DivFieldCustomer">' + customer[item].Email + '</div>' +
                                            '<div class="DivFieldCustomer">' + customer[item].BillingAddress + '</div>' +
                                        '</td>' +
                                        '</tr>'
                                    );
                                })
                            }
                        }
                    }
                }
            },
            '.DivNamaCustomer click': function (el) {
                var id = el.attr('id');
                var name = el.text();
                $('#selectcust').val(name);
                $('#CustomerId').val(id);
                this.CheckNameCutomer(name);
                $('.DivSearchCustomer').hide();
            },
            RenderToSearchList: function (searchResult) {
                $(".resultItemDiv").show();
                $("#itemList").empty();
                $.each(searchResult, function (index) {
                    searchResultList = $("<tr class='itemTr'><td class='itemTd' id='" + index + "' tabIndex='" + index + "'>" +
                                "<div class='itemName' id='itemName" + index + "'>" + searchResult[index].Name + "</div>" +
                                "<div class='itemDesc' id='itemDesc" + index + "'>" + searchResult[index].Description + "</div></td></tr>");
                    searchResultList.appendTo($("#itemList"));
                });
            },
            FillItemAtribut: function (name, index) {
                var part = itmRepo.GetItemByName(name);
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
            "input keypress": function (el, ev) {
                if (el.not($(":button"))) {
                    if (el.attr("id") != "selectcust") {
                        if (el.attr("class") != "partname") {
                            if (ev.keyCode == 13) {
                                var iname = el.val();
                                if (iname !== 'Simpan') {
                                    var fields = el.parents('form:eq(0),body').find('button, input[type!=hidden], textarea, select');
                                    var index = fields.index(el);
                                    if (index > -1 && (index + 1) < fields.length) {
                                        fields.eq(index + 1).focus();
                                    }
                                    return false;
                                }
                            }
                        }
                    }
                }
            },
            "select keypress": function (el, ev) {
                if (ev.keyCode == 13) {
                    var iname = el.val();
                    if (iname !== 'Simpan') {
                        var fields = el.parents('form:eq(0),body').find('button, input[type!=hidden], textarea, select');
                        var index = fields.index(el);
                        if (index > -1 && (index + 1) < fields.length) {
                            fields.eq(index + 1).focus();
                        }
                        return false;
                    }
                }
            },
            "#latefee keyup": function (el, ev) {
                if (ev.keyCode == 13) {
                    if ($("divExchangeRate").is(":visible")) {
                        $("#custRate").focus();
                    } else {
                        $("#part_0").focus();
                    }
                    return;
                }
            }
        })
          });