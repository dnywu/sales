steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/models',
      './ItemList.css',
      'sales/controllers/items/create',
      'sales/controllers/items/edit')
	.then('./views/ItemList.ejs', './views/popupEventDialog.ejs', './views/confirmBox.ejs', function ($) {

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
                        '<td class="itemList itemPrice">Rp. ' + $this.rupiahFormat(data[item].Rate) + '</td></tr>');
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
            ".settingButton click": function (el) {
                var index = el.attr("tabindex");
                $("tr#itemContent" + index + " td#settingPanel" + index + " div.popupEventDiv").show();
            },
            "#checkBoxAllList change": function () {
                if ($("#checkBoxAllList").attr('checked')) {
                    $(".checkBoxItem").attr('checked', 'checked');
                } else {
                    $(".checkBoxItem").removeAttr('checked');
                }
            },
            "#btnEdit click": function (el) {
                var index = el.attr("tabindex");
                var id = $("#checkBoxItem" + index).val();
                $('#body').sales_items_edit();
                $('#body').sales_items_edit("load", id);
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
                var countChecked = 0;
                $(".checkBoxItem:checked").each(function (index) {
                    countChecked++;
                });
                if (countChecked==0) {
                    return;
                }
                $("#body").append(this.view("//sales/controllers/items/list/views/confirmBox.ejs"));
            },
            "#confirmYes click": function () {
                $(".ModalDialog").remove();
                $(".checkBoxItem")
                $(".checkBoxItem:checked").each(function (index) {
                    $.ajax({
                        type: 'DELETE',
                        url: '/deleteItem/_id/' + $(this).val(),
                        dataType: 'json'
                    });
                });
                $this.ChangePage();
            },
            "#confirmNo click": function () {
                $('.ModalDialog').remove();
            },
            rupiahFormat: function (number) {
                if (isNaN(number)) return "";
                var str = new String(number);
                var result = "", len = str.length;
                for (var i = len - 1; i >= 0; i--) {
                    if ((i + 1) % 3 == 0 && i + 1 != len) result += ",";
                    result += str.charAt(len - 1 - i);
                }
                return result;
            }
        })

	});