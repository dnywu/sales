steal('jquery/controller',
        'jquery/view/ejs',
	    'jquery/dom/form_params',
        'jquery/controller/view',
        './editinvoices.css',
        'sales/scripts/jquery-ui-1.8.11.min.js',
        'sales/styles/jquery-ui-1.8.14.custom.css',
        'sales/repository/ItemRepository.js',
        'sales/repository/CustomerRepository.js',
        'jquery/view/ejs')
	.then('./views/editinvoices.ejs', function ($) {

	    $.Controller('Sales.Controllers.Invoices.Edit',
        {
            defaults: (tabIndexTr = 0,
                        $this = null,
                        inv = null,
                        itmRepo = null,
                        custRepo = null)
        },
        {
            init: function () {
                $this = this;
                itmRepo = new ItemRepository();
                custRepo = new CustomerRepository();
                inv = new Invoice();
            },
            load: function (id) {
                tabIndexTr = 0;
                var item = inv.GetDataInvoiceByID(id);
                var term = item.Terms;
                var term = item.LateFee;

                this.element.html("//sales/controllers/invoices/edit/views/editinvoices.ejs", item);
                this.CreateListItem(3);
                this.SetDatePicker();
                this.selectTerm(term);
                this.selectLateFee(term);
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
            '#terms change': function (el) {
                var invDate = $("#invDate").val();
                var dueDate = new Date(invDate);
                dueDate.setDate(dueDate.getDate() + parseInt(el.val()));
                $("#dueDate").val($.datepicker.formatDate('dd M yy', dueDate));
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
            selectTerm: function (term) {
                $('.TermOpt').each(function (index) {
                    if ($(this).val() == term) {
                        $(this).attr('selected', true);
                    }
                });
            },
            selectLateFee: function (LateFee) {
                $('.LateFeeOpt').each(function (index) {
                    if ($(this).val() == LateFee) {
                        $(this).attr('selected', true);
                    }
                });
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
            }
        })
	});