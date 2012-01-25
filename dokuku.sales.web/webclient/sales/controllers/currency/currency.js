steal('jquery/controller', 'jquery/view/ejs', './currency.css')
	.then('./views/listcurrency.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Currency
	    */
	    $.Controller('Sales.Controllers.Currency',
	    /** @Static */
    {
    defaults: {}
    },
	        /** @Prototype */
    {
            init: function () {
                this.element.html(this.view("//sales/controllers/currency/views/listcurrency.ejs"));
            },
            load: function () {
                this.element.html(this.view("//sales/controllers/currency/views/listcurrency.ejs"));
            }
        })

    });