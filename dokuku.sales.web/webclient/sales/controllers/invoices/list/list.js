steal('jquery/controller',
	   'jquery/view/ejs',
	   'jquery/controller/view',
	   'sales/models',
       'sales/scripts/stringformat.js',
       'sales/controllers/invoices/create',
       'sales/controllers/invoices/invoicedetail',
       './listinvoice.css')
.then('./views/listinvoice.ejs',
       './views/invoices.ejs',
       function ($) {

           $.Controller('Sales.Controllers.Invoices.List',
            {
                defaults: ($this = null,
                           inv = null)
            },
            {
                init: function () {
                    $this = this;
                    inv = new Invoice();
                    var invoices = this.GetInvoices();
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices));
                },
                load: function () {
                    var invoices = this.GetInvoices();
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices))
                },
                GetInvoices: function () {
                    var invoices = inv.GetDataInvoice();

                    return invoices;
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
                '.invNo click': function (el, ev) {
                    var invoiceId = $("#invoiceId_" + el.attr("id")).val();
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
            });

       });