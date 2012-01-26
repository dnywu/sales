steal( 'jquery/controller','jquery/view/ejs', './addcurrency.css' )
	.then( './views/AddCurrency.ejs', function($){

/**
 * @class Sales.Controllers.Currency.Create
 */
$.Controller('Sales.Controllers.Currency.Create',
/** @Static */
{
	defaults : {}
},
/** @Prototype */
{
	init : function(){
	    this.element.html(this.view("//sales/controllers/currency/create/views/AddCurrency.ejs"));
	}
    load : function(){
	    this.element.html(this.view("//sales/controllers/currency/create/views/AddCurrency.ejs"));
	}
})


});