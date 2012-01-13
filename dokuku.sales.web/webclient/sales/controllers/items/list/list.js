steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/models',
      './ItemList.css',
      'sales/controllers/items/create')
	.then('./views/ItemList.ejs', './views/popupEventDialog.ejs', function ($) {

	    $.Controller('sales.Controllers.items.list',

        {
            defaults: (jumlahdata = 0, start = 1, page = 1, totalPage = 1, $this = null)
        },
        {
            init: function () {
                $this = this;
                this.element.html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
                this.RequestNumberOfItem();
            },
            load: function () {
                $this = this;
                this.element.html(this.view("//sales/controllers/items/list/views/ItemList.ejs"));
                this.RequestNumberOfItem();
            },
            initPagination: function () {
                totalPage = Math.ceil(jumlahdata / limit);
                $('#totalPage').text(totalPage);
            },
            RequestNumberOfItem: function () {
                $.ajax({
                    type: 'GET',
                    url: '/CountItem',
                    dataType: 'json',
                    success: this.LimitData
                });
            },
            LimitData: function (data) {
                jumlahdata = data;
                limit = $('#limitData').val();
                $this.initPagination();
                $.ajax({
                    type: 'GET',
                    url: '/LimitItems/start/' + (((start - 1) * limit)) + '/limit/' + limit,
                    dataType: 'json',
                    ajaxStart: $this.LoadingListItem,
                    success: $this.requestAllItemSuccess
                });
                $('#idInputPage').val(1);
                $this.CheckButtonPaging();
            },
            LoadingListItem: function () {
                alert('Tunggu Cuy');
            },
            requestAllItemSuccess: function (data) {
                if (data == null || data.length == 0) {
                    $('#body').sales_items_create();
                }
                else {
                    totalData = data.length;
                    $("table.ItemList tbody").empty();
                    $.each(data, function (item) {
                        $("table.ItemList tbody").append('<tr class="trDataItem" id="itemContent' + item + '" tabindex="' + item + '">' +
                        '<td class="itemList"><input type="checkbox" class="checkBoxItem" id="checkBoxItem' + item + '" value="' + data[item]._id + '" /></td>' +
                        '<td class="itemList" id="settingPanel' + item + '"></td>' +
                        '<td class="itemList itemName">' + data[item].Name + '</td>' +
                        '<td class="itemList itemPrice">' + data[item].Rate + '</td></tr>');
                        $("td#settingPanel" + item).append("//sales/controllers/items/list/views/popupEventDialog.ejs", { index: item });
                    });
                    $('.trDataItem:odd').addClass('odd');
                }
            },
            "table.ItemList tbody tr hover": function (el) {
                var index = el.attr("tabindex");
                $("tr#itemContent" + index + " td#settingPanel" + index + " div.settingButton").show();
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
            },
            CheckButtonPaging: function () {
                var startPage = parseInt($('#idInputPage').val());
                if (isNaN(startPage) || startPage <= 1) {
                    $('.DivPrev').hide();
                    $('.disablePrev').show();
                } else {
                    $('.DivPrev').show();
                    $('.disablePrev').hide();
                }
                var totalPage = parseInt($('#totalPage').text());
                if (totalPage <= 1 || totalPage <= startPage) {
                    $('.DivNext').hide();
                    $('.disableNext').show();
                } else {
                    $('.DivNext').show();
                    $('.disableNext').hide();
                }
            },
            '.prev click': function () {
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                if (isNaN(startPage))
                    startPage = 1;
                else
                    startPage--;
                $('#idInputPage').val(startPage);
                $this.ChangePage();
            },
            '.next click': function () {
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                if (isNaN(startPage))
                    startPage = 2;
                else
                    startPage++;
                $('#idInputPage').val(startPage);
                $this.ChangePage();
            },
            '.last click': function () {
                $('#idInputPage').val(parseInt($('#totalPage').text()));
                $this.ChangePage();
            },
            '.first click': function () {
                $this.initPagination();
                $('#idInputPage').val(1);
                $this.ChangePage();
            },
            '#idInputPage change': function () {
                $this.ChangePage();
            },
            '#limitData change': function () {
                $this.ChangePage();
            },
            ChangePage: function () {
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                $.ajax({
                    type: 'GET',
                    url: '/LimitItems/start/' + (((startPage - 1) * $('#limitData').val())) + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    beforSend: $this.LoadingListItem,
                    success: $this.requestAllItemSuccess
                });
                limit = $('#limitData').val();
                $this.initPagination();
                $this.CheckButtonPaging();
            },
            "#deleteItem click": function () {
                var elemen = $(".checkBoxItem:checked");
                $(".checkBoxItem:checked").each(function (index) {
                    var id = this.val();
                    $.ajax({
                        type: 'DELETE',
                        url: '/deleteItem/_id/' + id,
                        dataType: 'json',
                        success: function () { alert('delete success') }
                    });
                });
            }
        })

	});