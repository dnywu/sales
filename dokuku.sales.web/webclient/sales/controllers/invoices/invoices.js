steal( 'jquery/controller','jquery/view/ejs' )
	.then( './views/init.ejs', function($){

/**
 * @class Sales.Controllers.Invoices
 */
$.Controller('Sales.Controllers.Invoices',
/** @Static */
{
	defaults : {}
},
/** @Prototype */
{
	init : function(){
		this.element.html("//sales/controllers/invoices/views/init.ejs",{
			message: "Hello World"
		});
	}
})

});