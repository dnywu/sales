var start = 0, limit = 20, totalData = 0, totalPage = 0, currentPage = 1;

steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/models',
      './ItemList.css',
      'sales/controllers/items/create')
	.then('./views/ItemList.ejs', './views/popupEventDialog.ejs', function ($) {

	    $.Controller('sales.Controllers.items.list',

        {
            onDocument: true
        },
        {
            init: function () {
                $("#body").html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
                this.RequestAllItems();
            },
            load: function () {
                $("#body").html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
                this.RequestAllItems();
            },
            initpagination: function () {
                totalPage = Math.round(totalData / limit);
                 
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
                    totalData = data.length;
                    $("table.ItemList tbody").empty();
                    $.each(data, function (item) {
                        $("table.ItemList tbody").append('<tr id="itemContent' + item + '" tabindex="' + item + '">' +
                        '<td class="itemList"><input type="checkbox" class="checkBoxItem" id="checkBoxItem' + item + '" value="" /></td>' +
                        '<td class="itemList" id="settingPanel' + item + '"></td>' +
                        '<td class="itemList itemName">' + data[item].Name + '</td>' +
                        '<td class="itemList itemPrice">' + data[item].Rate + '</td></tr>');
                        $("td#settingPanel" + item).append("//sales/controllers/items/list/views/popupEventDialog.ejs", { index: item });
                    });
                }
            },
            saveDataToModel: function (data) {
                $.each(data, function (item) {
                    new Sales.Items({
                        id: item,
                        namaBarang: data[item].Name,
                        hargaBarang: data[item].Rate
                    }).save();
                });
                $("#body").html("//sales/controllers/items/list/views/ItemList.ejs", Sales.Items.findAll());
            },
            "table.ItemList tbody tr hover": function (el) {
                var index = el.attr("tabindex");
            },
            "table.ItemList tbody tr mouseleave": function (el) {
                var index = el.attr("tabindex");
                $("tr#itemContent" + index + " td#settingPanel" + index + " div.settingButton").hide();
                $("tr#itemContent" + index + " td#settingPanel" + index + " div.popupEventDiv").hide();
            },
            "#gotoCreateItem click": function () {
                $('#body').empty();
                $('#body').sales_items_create('load');
            },
            ".settingButton click": function () {
                var index = $("table.ItemList tbody tr").attr("tabindex");
                $("tr#itemContent" + index + " td#settingPanel" + index + " div.popupEventDiv").show();
            },
            "#checkBoxAllList change": function () {
                if ($("#checkBoxAllList").attr('checked')) {
                    $(".checkBoxItem").attr('checked', 'checked');
                } else {
                    $(".checkBoxItem").removeAttr('checked');
                }
            },
            "#btnEdit click": function () {
                alert("test button edit");
            },
            "#btnDelete click": function () {
                alert("test button hapus");
            }

        })

	});