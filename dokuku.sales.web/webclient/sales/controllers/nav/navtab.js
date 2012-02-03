steal(  'jquery/controller',
        'jquery/view/ejs',
        'jquery/controller/view',
        'sales/models'
     )
	.then(function ($) {
	    $.Controller('sales.controllers.navtab',
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/nav/views/navtab.ejs"));
            },
            '#HomeLink click': function (el) {
                window.location = 'home';
            },
            '#CustomerLink click': function (el) {
                window.location = 'customer';
            },
            '#InvoiceLink click': function (el) {
                window.location = 'invoices';
            },
            '#SettingLink click': function (el) {
                window.location = 'setting';
            }
        })
        });