steal('jquery/controller', 'jquery/view/ejs')
	.then('./views/init.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Items.Create
	    */
	    $.Controller('Sales.Items.Create',
	    /** @Static */
{
defaults: {}
},
	    /** @Prototype */
{
init: function () {
    $('#section').empty();
    this.element.html(this.view("//sales/controllers/items/create/views/init.ejs"));
}
})

	});