steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/models',
      './ItemList.css',
      'sales/controllers/items/create')
	.then('./views/ItemList.ejs', './views/popupEventDialog.ejs', function ($) {

	    $.Controller('sales.Controllers.items.list',

        {
            defaults: (totalData = 0, start = 0, limit = 1, page = 1, totalPage = 1, $this = null)
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
                totalPage = Math.ceil(totalData / limit);
                $('#idInputPage').val(page);
                $('#totalPage').text(totalPage);
                $('#firstButton').addClass('firstInactive');
                $('#prevButton').addClass('prevInactive');
                if (totalPage > page) {
                    $('#nextButton').addClass('nextActive');
                    $('#lastButton').addClass('lastActive');
                }
                else {
                    $('#nextButton').addClass('nextInactive');
                    $('#lastButton').addClass('lastInactive');
                }
            },
            RequestNumberOfItem: function () {
                $.ajax({
                    type: 'GET',
                    url: '/Items',
                    dataType: 'json',
                    success: this.RequestNumberOfItemSuccess
                });
            },
            RequestNumberOfItemSuccess: function (data) {
                totalData = data;
                $this.initPagination();
                $this.requestLimitData();
            },
            requestLimitData: function () {
                $.ajax({
                    type: 'GET',
                    url: '/LimitItems/start/' + start + '/limit/' + limit,
                    dataType: 'json',
                    success: $this.requestAllItemSuccess
                });
            },
            requestAllItemSuccess: function (data) {
                if (data == null || data.length == 0) {
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
            },
            ".nextActive click": function () {
                start = start + 1;
                page = page + 1;
                $('#idInputPage').val(page);
                $this.requestLimitData();
                if (totalPage == page) {
                    $('#nextButton').removeClass('nextActive');
                    $('#lastButton').removeClass('lastActive');
                    $('#nextButton').addClass('nextInactive');
                    $('#lastButton').addClass('lastInactive');
                }
                $('#firstButton').removeClass('firstInactive');
                $('#prevButton').removeClass('prevInactive');
                $('#firstButton').addClass('firstActive');
                $('#prevButton').addClass('prevActive');
            },
            ".prevActive click": function () {
                start = start - 1;
                page = page - 1;
                $('#idInputPage').val(page);
                $this.requestLimitData();
                if (page == 1) {
                    $('#prevButton').removeClass('nextActive');
                    $('#firstButton').removeClass('lastActive');
                    $('#prevButton').addClass('nextInactive');
                    $('#firstButton').addClass('lastInactive');
                }
                if (totalPage == 1) {
                    $('#lastButton').removeClass('lastActive');
                    $('#nextButton').removeClass('nextActive');
                    $('#lastButton').addClass('lastInactive');
                    $('#nextButton').addClass('nextInactive');
                }
                if (totalPage != page) {
                    $('#lastButton').removeClass('lastInactive');
                    $('#nextButton').removeClass('nextInactive');
                    $('#lastButton').addClass('lastActive');
                    $('#nextButton').addClass('nextActive');
                }
            },
            "#idInputPage blur": function () {
                var inputPage = $("#idInputPage").val();
                if (inputPage > totalPage) {
                    alert("total page " + totalPage);
                    $("#idInputPage").val(page);
                }
                else {
                    start = inputPage - 1;
                    page = inputPage;
                    $this.requestLimitData();
                    if (inputPage == totalPage) {
                        $('#nextButton').removeClass('nextActive');
                        $('#lastButton').removeClass('lastActive');
                        $('#nextButton').addClass('nextInactive');
                        $('#lastButton').addClass('lastInactive');
                    }
                    if (inputPage == 1) {
                        $('#prevButton').removeClass('nextActive');
                        $('#firstButton').removeClass('lastActive');
                        $('#prevButton').addClass('nextInactive');
                        $('#firstButton').addClass('lastInactive');
                    }
                    if (inputPage != totalPage && inputPage != 1) {
                        if ($('#firstButton').hasClass('firstInactive') && $('#prevButton').hasClass('prevInactive')) {
                            $('#firstButton').removeClass('firstInactive');
                            $('#prevButton').removeClass('prevInactive');
                            $('#firstButton').addClass('firstActive');
                            $('#prevButton').addClass('prevActive');
                        }
                        if ($('#lastButton').hasClass('lastInactive') && $('#nextButton').hasClass('nextInactive')) {
                            $('#nextButton').removeClass('nextInactive');
                            $('#lastButton').removeClass('lastInactive');
                            $('#nextButton').addClass('nextActive');
                            $('#lastButton').addClass('lastActive');
                        }
                    }
                }
            },
            "#limitData change": function () {
                var limitData = $("#limitData").val();
                limit = limitData;
                $this.initPagination();
                $this.RequestNumberOfItem();
            }
        })

	});