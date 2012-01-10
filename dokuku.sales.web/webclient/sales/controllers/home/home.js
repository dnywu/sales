steal('jquery/controller', 'jquery/view/ejs',
        'jquery/controller/view',
        './Home.css',
        'sales/controllers/items/list',
        'sales/controllers/customers'
     )
	.then('./views/home.ejs', function ($) {
	    $.Controller('sales.controllers.home',
    {
        init: function () {
            this.element.html(this.view("//sales/controllers/home/views/home.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
        },
        '#CustomerLink click': function () {
            $("#body").empty();
            $("#body").sales_customers('load');
        },
        '#InvoiceLink click': function () {
            $("#body").empty();
            $("#body").sales_items_list('load');
        }
    })
	});