steal( 'jquery/controller','jquery/view/ejs' )
	.then( './views/init.ejs', function($){

/**
 * @class Sales.Controllers.Paymentreceived
 */
$.Controller('Sales.Controllers.Paymentreceived',
/** @Static */
{
	defaults : {}
},
/** @Prototype */
{
	init : function(){
	    this.element.html(this.view("//sales/controllers/paymentreceived/views/paymentreceivedlist.ejs"));
	},
load: function () {
  
    this.element.html(this.view("//sales/controllers/paymentreceived/views/paymentreceivedlist.ejs"));
   
}

})

});