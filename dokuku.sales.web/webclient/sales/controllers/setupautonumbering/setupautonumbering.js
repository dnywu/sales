steal( 'jquery/controller','jquery/view/ejs' )
	.then( './views/init.ejs', function($){

/**
 * @class Sales.Controllers.Setupautonumbering
 */
$.Controller('Sales.Controllers.Setupautonumbering',
/** @Static */
{
	defaults : {}
},
/** @Prototype */
{
	init : function(){
		this.element.html("//sales/controllers/setupautonumbering/views/init.ejs",{
			message: "Hello World"
		});
	}
})

});