steal('jquery/controller', 'jquery/view/ejs',
        'jquery/controller/view',
        'sales/models',
        './nav.css'
     )
	.then('./views/navHeader.ejs', function ($) {
	    $.Controller('sales.controllers.nav',
        {
            onDocument: true
        },
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/nav/views/navHeader.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
            }
        })
	});