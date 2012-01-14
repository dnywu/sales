steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemEdit.css')
	.then('./views/editItem.ejs', function ($) {

	    $.Controller('sales.Controllers.items.edit',
            {
        },
            {
                init: function () {
                    this.element.html(this.view("//sales/controllers/items/edit/views/editItem.ejs"));
                },
                setItems: function (item) {
                    this.items = item;
                }
            })
	});