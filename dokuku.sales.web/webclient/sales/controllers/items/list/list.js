steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemList.css',
      'sales/controllers/items/create')
	.then('./views/ItemList.ejs', function ($) {

	    $.Controller('sales.Controllers.items.list',

        {
            onDocument: true
        },
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
                this.RequestAllItems();
            },
            load: function () {
                this.element.html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
                this.RequestAllItems();
            },
            RequestAllItems: function () {
                $.ajax({
                    type: 'GET',
                    url: '/Items',
                    dataType: 'json',
                    success: this.requestAllItemSuccess
                });
            },
            requestAllItemSuccess: function (data) {
                if (data == null) {
                    $('#body').sales_items_create();
                }
                else {
                    $("table.ItemList tbody").empty();
                    $.each(data, function (item) {
                        $("table.ItemList tbody").append('<tr id="itemContent' + item + '" tabindex="' + item + '">' +
                        '<td class="itemList"><input type="checkbox" id="checkBoxItem' + item + '" value="" /></td>' +
                        '<td class="itemList"><div class="settingButton" id="settingButton' + item + '"><img class="" src="/sales/controllers/items/list/images/setting.png"/></div></td>' +
                        '<td class="itemList itemName">' + data[item].Name + '</td>' +
                        '<td class="itemList itemPrice">' + data[item].Rate + '</td></tr>');
                    });
                }
            },
            "table.ItemList tbody tr hover": function (el) {
                var index = el.attr("tabindex");
                $("#settingButton" + index).show();
            },
            "table.ItemList tbody tr mouseleave": function (el) {
                var index = el.attr("tabindex");
                $("#settingButton" + index).hide();
            },
            "#gotoCreateItem click": function () {
                $('#body').empty();
                $('#body').sales_items_create('load');
            }
        })

	});