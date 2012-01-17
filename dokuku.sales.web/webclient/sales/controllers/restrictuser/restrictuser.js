steal( 'jquery/controller','jquery/view/ejs',
       'jquery/controller/view' )
	.then( './views/init.ejs', function($){

/**
 * @class Sales.Controllers.Restrictuser
 */
$.Controller('sales.Controllers.restrictuser',
/** @Static */
{
	defaults : {}
},
/** @Prototype */
{
	init : function(){
		this.element.html(this.view("//sales/controllers/restrictuser/views/init.ejs"));
	}
})

});