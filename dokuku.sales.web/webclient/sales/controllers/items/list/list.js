steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemList.css')
	.then('./views/ItemList.ejs', function ($) {

	    $.Controller('Sales.Items.List',

{
    defaults: {}
},
{
    init: function () {
        this.element.html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
    }
})

});