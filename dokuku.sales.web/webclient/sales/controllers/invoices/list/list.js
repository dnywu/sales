steal('jquery/controller',
	   'jquery/view/ejs',
	   'jquery/controller/view',
	   'sales/models',
       'sales/controllers/invoices/create',
       './listinvoice.css')
.then('./views/listinvoice.ejs',
       './views/invoices.ejs',
       function ($) {

           $.Controller('Sales.Controllers.Invoices.List',
           /** @Static */
            {
            defaults: {}
        },
            {
                init: function () {
                    var invoices = this.GetInvoices();
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices));
                },
                load: function () {
                    var invoices = this.GetInvoices();
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices))
                },
                GetInvoices: function () {
                    var invoices = new Array({ name: 'a', des: 'a' }, { name: 'b', des: 'b' });
                    return invoices;
                },
                '#selectall change': function () {
                    if ($("#selectall").attr('checked')) {
                        $(".selectInvoice").attr('checked', 'checked');
                    } else {
                        $(".selectInvoice").removeAttr('checked');
                    }
                },
                '#newinvoices click': function () {
                    $("#body").sales_invoices_create("load");
                }
            });

       });