steal('jquery/controller',
	   'jquery/view/ejs',
	   'jquery/controller/view',
	   'sales/models',
       'sales/scripts/stringformat.js',
       'sales/controllers/invoices/create',
       'sales/controllers/invoices/edit',
       'sales/controllers/invoices/invoicedetail',
       'sales/repository/InvoiceRepository.js',
       './listinvoice.css',
       './DeleteConfirmBox.css')
.then('./views/listinvoice.ejs',
       './views/invoices.ejs',
       './views/confirmDeleteInvoice.ejs',
       function ($) {

           $.Controller('Sales.Controllers.Invoices.List',
            {
                defaults: ($this = null,
                           inv = null,
                           invRepo = null)
            },
            {
                init: function () {
                    $this = this;
                    inv = new Invoice();
                    invRepo = new InvoiceRepository();
                    this.load();
                },
                load: function () {
                    $this = this;
                    var invoices = invRepo.GetAllInvoice();
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices))
                },
                GetInvoices: function () {
                    var invoices = inv.GetDataInvoice();
                    return invoices;
                },
                '#SearchInvoice keypress': function (el, ev) {
                    if (ev.keyCode == "13") {
                        var invoices = inv.SearchInvoice();
                        this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices));
                    }
                },
                '#SearchInvoice focus': function () {
                    $(".DivSearch").attr("style", "background:#FFFFFF; border-color:#3BB9FF");
                    $("#SearchInvoice").attr("style", "outline:none; background:#FFFFFF");
                },
                '#SearchInvoice blur': function () {
                    $(".DivSearch").attr("style", "background:#F3F3F3");
                    $("#SearchInvoice").attr("style", "background:#F3F3F3");
                },
                '#selectall change': function () {
                    if ($("#selectall").attr('checked')) {
                        $(".selectInvoice").attr('checked', 'checked');
                    } else {
                        $(".selectInvoice").removeAttr('checked');
                    }
                },
                'table#tblListInvoice tbody.bodyDataInvoice tr.trbodyDataInvoice hover': function (el) {
                    var index = el.attr('tabindex');
                    $('#settingListInvoice' + index).show();
                    //$("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").hide();
                },
                'table#tblListInvoice tbody.bodyDataInvoice tr.trbodyDataInvoice mouseleave': function (el) {
                    var index = el.attr('tabindex');
                   // $('#settingListInvoice' + index).hide();
                    //$("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").hide();
                },
                '.settingListInvoice click': function (el) {
                    var index = el.attr('tabindex');
                    $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").show();
                },
                '#newinvoices click': function () {
                    $("#body").sales_invoices_create("load");
                },
                '.EditContextMenuInvoive click': function (el) {
                    var id = el.attr('id');
                    $('#body').sales_invoices_edit('load', id);
                },
                '.ApproveContextMenuInvoive click': function (el) {
                    //var id = el.attr('id');
                    var index = $(this).attr("id");
                    var no = $("#invoiceId_" + index).val();
                    result = inv.ApproveInvoiceByID(no);
                },
                '.invNo click': function (el, ev) {
                    var invoiceId = $("#invoiceId_" + el.attr("id")).val();
                    var invoice = invRepo.GetInvoiceById(invoiceId);
                    if (invoice != null)
                        $("#body").sales_invoices_invoicedetail('load', invoice);
                },
                '#deleteinvoice click': function () {
                    var checkList = $this.IsCheckListNull();
                    if (checkList != 0) {
                        var message = $("<div>Apakah anda yakin akan menghapus pelanggan ini</div>" +
                                    "<div class='ButtonConfirmYes'>Ya</div>" +
                                    "<div class='ButtonConfirmClose'>Tidak</div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
                },
                '.ButtonConfirmYes click': function () {
                    var result;
                    $(".selectInvoice:checked").each(function (index) {
                        var index = $(this).attr("id");
                        var no = $("#invoiceId_" + index).val();
                        result = inv.DeleteInvoice(no);
                    });
                    if (result == "OK") {
                        $(".DeleteConfirmation").remove();
                        $this.load();
                    } else {
                        $(".BodyConfirmMassage").empty();
                        var message = $("<div>" + result.message + "</div>" +
                                    "<div class='ButtonConfirmClose'>Tutup Pesan</div>");
                        $(".BodyConfirmMassage").append(message);
                    }
                },
                '.ButtonConfirmClose click': function () {
                    $(".DeleteConfirmation").remove();
                },
                IsCheckListNull: function () {
                    var countChecked = 0;
                    $(".selectInvoice:checked").each(function (index) {
                        countChecked++;
                    });
                    if (countChecked == 0) {
                        return 0;
                    }
                    return countChecked;
                }
            });

       });