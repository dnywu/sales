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

                },
                load: function (id) {
                    var item = this.getItem(id);
                    this.element.html("//sales/controllers/items/edit/views/editItem.ejs", item);
                },
                getItem: function (id) {
                    var item = null;
                    $.ajax({
                        type: 'GET',
                        url: 'Items/_id/' + id,
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            item = data;
                        }
                    });
                    return item;
                },
                "#editItemsForm submit": function (el, ev) {
                    var form = $("#editItemsForm");
                    $.ajax({
                        type: "POST",
                        url: "/editItem",
                        data: form.serialize(),
                        dataType: "json",
                        success: function () { $("#body").sales_items_list("load"); }
                    });
                    ev.preventDefault();
                }
            })
	});