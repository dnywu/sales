steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemEdit.css')
	.then('./views/editItem.ejs', function ($) {

	    $.Controller('sales.Controllers.items.edit',
            {
        },
            {
                //                init: function (el, ev, id) {
                //                    var item = this.getItem(id);
                //                    this.element.html("//sales/controllers/items/edit/views/editItem.ejs", item );
                //                },
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
                    var err = $("#errorCreateItemDiv");
                    var defaults = {
                        name: $("#itemName").val(),
                        description: $("#description").val(),
                        price: $("#itemPrice").val(),
                        tax: $("#tax").val()
                    };
                    err.empty();
                    if (defaults.name !== "" && defaults.price != 0)
                        $.ajax({
                            type: "POST",
                            url: "/editItem/item/" + form.serialize(),
                            dataType: "json",
                            success: function () { $("#body").sales_items_list("load"); }
                        });
                    if (defaults.name == "")
                        $('<li>', { 'class': 'name', text: "Nama Barang harus di isi" }).appendTo(err.show());
                    if (defaults.price == "")
                        $('<li>', { 'class': 'price', text: "Harga harus di diisi" }).appendTo(err.show());
                    if (defaults.description.length > 500)
                        $('<li>', { 'class': 'description', text: "Deskripsi barang tidak boleh lebih dari 500 karakter" }).appendTo(err.show());
                    ev.preventDefault();
                }
            })
	});