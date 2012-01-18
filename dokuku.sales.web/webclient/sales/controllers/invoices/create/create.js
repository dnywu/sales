steal('jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
       'sales/controllers/invoices/Invoice.js',
       'sales/controllers/invoices/AddCustomer.js',
       'sales/scripts/jquery-ui-1.8.11.min.js',
       'sales/styles/jquery-ui-1.8.14.custom.css',
       'sales/repository/ItemRepository.js',
       'sales/repository/CustomerRepository.js',
	   'sales/models')
	.then('./views/createinvoices.ejs',
          './views/AddCustomer.ejs', function ($) {
              $.Controller('Sales.Invoices.Create',
        {
            defaults: ( tabIndexTr = 0,
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
                this.load();
            },
            load: function () {
                tabIndexTr = 0;
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
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
            '#selectcust change': function (el, ev) {
                $("#keteranganSelectCust").empty();
                var dataCust = custRepo.GetCustomerByName(el.val());
                if (dataCust != null) {
                    $("#selectcust").val(dataCust.Name);
                    $("#currency").text(dataCust.Currency).show();
                    $("#CustomerId").val(dataCust._id);
                    return;
                }
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
                //var t = $(".amount");
                $this.GetSubTotal();
            },
            '.partname change': function (el) {
                var partName = el.val();
                var index = el.attr("id").split('_')[1];
                var part = itmRepo.GetItemByName(partName);

                if (part != null) {
                    $("#partid_" + index).val(part._id);
                    $("#part_" + index).val(part.Name);
                    $("#desc_" + index).text(part.Description);
                    $("#qty_" + index).val('1.00');
                    $("#rate_" + index).val(part.Rate);
                    $("#disc_" + index).val('0.00');
                    $("#amount_" + index).text(part.Rate);
                    $("#itemInvoice tbody tr#tr_" + index).removeClass('errItemNotFound');
                    $this.GetSubTotal();
                    $this.GetTotal();
                    return;
                }
                $this.ClearItemField;
                $("#itemInvoice tbody tr#tr_" + index).addClass('errItemNotFound');
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
            CalculateItem: function (element) {
                var index = element.attr("id").split('_')[1];
                var qty = $("#qty_" + index).val();
                var rate = $("#rate_" + index).val();
                var disc = $("#disc_" + index).val();
                var amount = inv.CalculateAmountPerItem(qty, rate, disc);
                $("#amount_" + index).text(amount);
                $this.GetSubTotal();
                $this.GetTotal();
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
                                    "<td><input type='text' name='part' class='partname' id='part_" + tabIndexTr + "'/>" +
                                    "<input type='hidden' class='partid' id='partid_" + tabIndexTr + "'/></td>" +
                                    "<td><textarea name='description' class='description' id='desc_" + tabIndexTr + "'></textarea></td>" +
                                    "<td><input type='text' name='quantity' class='quantity right' id='qty_" + tabIndexTr + "'></input></td>" +
                                    "<td><input type='text' name='price' class='price right' id='rate_" + tabIndexTr + "'></input></td>" +
                                    "<td><input type='text' name='discount' class='discount right' id='disc_" + tabIndexTr + "'></input></td>" +
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
            GetTotal: function () {
                $("#total").text(inv.CalculateTotal);
            },
            '#formNewIvoice submit': function (el, ev) {
                ev.preventDefault();
                inv.CreateNewInvoice();
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
            }
        })
          });