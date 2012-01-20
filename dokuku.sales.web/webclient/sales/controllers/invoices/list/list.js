steal('jquery/controller',
	   'jquery/view/ejs',
	   'jquery/controller/view',
	   'sales/models',
       'sales/scripts/stringformat.js',
       'sales/controllers/invoices/create',
       'sales/controllers/invoices/edit',
       'sales/controllers/invoices/invoicedetail',
       'sales/repository/InvoiceRepository.js',
       './listinvoice.css')
.then('./views/listinvoice.ejs',
       './views/invoices.ejs',
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
                    var invoices = invRepo.GetAllInvoice();
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices))
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
                    $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").hide();
                },
                'table#tblListInvoice tbody.bodyDataInvoice tr.trbodyDataInvoice mouseleave': function (el) {
                    var index = el.attr('tabindex');
                    $('#settingListInvoice' + index).hide();
                    $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").hide();
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
                '.invNo click': function (el, ev) {
                    var invoiceId = $("#invoiceId_" + el.attr("id")).val();
<<<<<<< HEAD
                    $.ajax({
                        type: 'GET',
                        url: '/invoice/' + invoiceId,
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            $("#body").sales_invoices_invoicedetail('load', data);
                        }
                    });
                },
                '#deleteinvoice click': function () {
                    $(".selectInvoice:checked").each(function (index) {
                        var no = $(".invoiceNo").text();
                        inv.DeleteInvoice(no);
                    });
=======
                    var invoice = invRepo.GetInvoiceById(invoiceId);
                    if (invoice != null)
                        $("#body").sales_invoices_invoicedetail('load', invoice);
                }
>>>>>>> 9cf6ad2f76c5c9de2a2cc68c47688ddd784d64f8
            });

       });