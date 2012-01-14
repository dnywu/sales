steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/customers/customer.css')
	.then('./views/editCustomer.ejs', function ($) {

	    $.Controller('Sales.Controllers.Customers.Edit',
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/customers/edit/views/editCustomer.ejs"));
            }
        })
	});

