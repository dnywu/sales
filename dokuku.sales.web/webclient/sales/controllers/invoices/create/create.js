steal('jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
	   'sales/models',
       'sales/scripts/SearchDialog.js')
	.then('./views/createinvoices.ejs', function ($) {
	    $.Controller('Sales.Invoices.Create',
    {
        init: function () {
            this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
        },
        load: function () {
            this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
        }
    })

	});