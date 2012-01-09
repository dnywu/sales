steal('jquery/controller', 'jquery/view/ejs',
        'jquery/controller/view',
        './Home.css',
        'sales/controllers/items/create',
        'sales/controllers/setuporganization'
     )
	.then('./views/home.ejs', function ($) {
	    $.Controller('sales.controllers.home',
    {
        init: function () {
            this.element.html(this.view("//sales/controllers/home/views/home.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
        },
        '#CustomerLink click': function () {
            $("#body").empty();
            $("#body").removeClass();
            $("#body").sales_items_create().GetView();
        },
        '#InvoiceLink click': function () {
            $("#body").empty();
            $("#body").removeClass();
            alert('test');
        }
    })
	});