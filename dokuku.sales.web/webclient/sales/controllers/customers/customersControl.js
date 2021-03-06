steal('jquery',
      'jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/customers/edit',
      'sales/controllers/customers/customer.css',
      'sales/repository/CustomerRepository.js',
      'sales/controllers/invoices/create')
	.then('./views/listCustomer.ejs', './views/addcustomer.ejs', function ($) {

	    $.Controller('sales.Controllers.customerscontrol',
        {
            defaults: (jumlahdata = 0, start = 1, page = 1, totalPage = 1, $this = null, custRepo = null)
        },
        {
            init: function () {
                $this = this;
                custRepo = new CustomerRepository();
                this.element.html(this.view('//sales/controllers/customers/views/listCustomer.ejs'));
                this.RequestAllCustomer();
            },
            load: function () {
                $this = this;
                custRepo = new CustomerRepository();
                this.element.html(this.view('//sales/controllers/customers/views/listCustomer.ejs'));
                this.RequestAllCustomer();
            },
            RequestAllCustomer: function () {
                $.ajax({
                    type: 'GET',
                    url: '/Customers',
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
                    url: '/LimitCustomers/start/' + (((start - 1) * limit)) + '/limit/' + limit,
                    dataType: 'json',
                    ajaxStart: $this.LoadingListCustomer,
                    success: $this.requestAllCustomerSuccess
                });
                $('#idInputPage').val(1);
                $this.CheckButtonPaging();
            },
            LoadingListCustomer: function () {
                alert('Tunggu Cuy');
            },
            initPagination: function () {
                totalPage = Math.ceil(jumlahdata / limit);
                $('#totalPage').text(totalPage);
            },
            '#AddCustomers submit': function (el, ev) {
                var form = $("#AddCustomers").formParams();
                var data = JSON.stringify(form);
                ev.preventDefault();
                $.ajax({
                    type: "POST",
                    url: "/customer/data",
                    data: { 'data': data },
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            $("#body").sales_customerscontrol('load');
                        }
                    }
                });
            },
            '.BtnTambahCustomer click': function () {
                $("#body").html(this.view('//sales/controllers/customers/views/addcustomer.ejs'));
            },
            '#inputText_CustomerName focus': function () {
                $('.hint_namaPelanggan').css('display', 'inline');
            },
            '#inputText_CustomerName blur': function () {
                $('.hint_namaPelanggan').css('display', 'none');
            },
            '#seperateShipping change': function () {
                if ($('#seperateShipping').attr('checked')) {
                    $("#address").show();
                }
                else {
                    $("#address").hide();
                    $('#inputTextarea_NewCust_ShipmentAddress').val('');
                    $('#inputText_NewCust_City').val('');
                    $('#inputText_NewCust_StateProvince').val('');
                    $('#inputText_NewCust_ZIPPostalCode').val('');
                    $('#inputText_NewCust_Fax').val('');
                }
            },
            '#addCustomField click': function () {
                $("#CustomField").show();
                $("#DivCustomField").hide();
            },
            '#copyField click': function () {
                $('#inputTextarea_NewCust_ShipmentAddress').val($('#inputTextarea_BillingAddress').val());
                $('#inputText_NewCust_City').val($('#inputText_City').val());
                $('#inputText_NewCust_StateProvince').val($('#inputText_StateProvince').val());
                $('#inputText_NewCust_ZIPPostalCode').val($('#inputText_ZIPPostalCode').val());
                $('#inputText_NewCust_Fax').val($('#inputText_Fax').val());
            },
            requestAllCustomerSuccess: function (data) {
                $("table.dataCustomer tbody").empty();
                $.each(data, function (item) {
                    $("table.dataCustomer tbody.BodyDataCustomer").append(
                        "<tr class='trDataCustomer' id='trCustomerList" + item + "' tabindex='" + item + "'>" +
                        "<td class='thDataCustomer tdDataCustomer tdDataCustomerCenter textAlignRight' style='text-align:center'><input type='checkbox' name='SelectAll' class='SelectCustomer' value='" + data[item]._id + "'/></td>" +
                        "<td class='tdDataCustomerCenter tdDataCustomer' id='tdDataCustomer" + item + "'><div class='settingListCustomer' id='settingListCustomer" + item + "' tabindex='" + item + "'><img class='' src='/sales/images/setting.png'/></div></td>" +
                        "<td class='tdDataCustomerLeft tdDataCustomer'>" +
                            "<div class='nameCompany' width = '100%'>" + data[item].Name + "</div>" +
                            "<div class='atributDataCustomer' width = '100%'>" + data[item].BillingAddress + "</div>" +
                            "<div class='atributDataCustomer' width = '100%'>" + data[item].Email + "</div>" +
                        "</td>" +
                        "<td class='tdDataCustomerRight tdDataCustomer'>Rp. 00</td>" +
                        "<td class='tdDataCustomerRight tdDataCustomer'>Rp. 00</td>" +
                        "</tr>");
                    $("td#tdDataCustomer" + item).append("//sales/controllers/customers/views/contextMenuCustomer.ejs", { index: item }, { id: data[item]._id });
                });
                $('.trDataCustomer:odd').addClass('odd');
            },
            '#hapus click': function () {
                $('.MessageConfirmation').show();
            },
            '.ButtonConfirmationMassageYa click': function () {
                $('.SelectCustomer:checked').each(function (index) {
                    $.ajax({
                        type: 'Delete',
                        url: '/DeleteCustomer/' + $(this).val(),
                        dataType: 'json'
                    });
                })
                $('.MessageConfirmation').hide();
                $this.ChangePage();
            },
            '.ButtonConfirmationMassageTidak click': function () {
                $('.MessageConfirmation').hide();
            },
            'table.dataCustomer tbody.BodyDataCustomer tr.trDataCustomer hover': function (el) {
                var index = el.attr('tabindex');
                $('#settingListCustomer' + index).show();
                $("tr#trCustomerList" + index + " td#tdDataCustomer" + index + " div.ContextMenuCustomer").hide();
            },
            'table.dataCustomer tbody.BodyDataCustomer tr.trDataCustomer mouseleave': function (el) {
                var index = el.attr('tabindex');
                $('#settingListCustomer' + index).hide();
                $("tr#trCustomerList" + index + " td#tdDataCustomer" + index + " div.ContextMenuCustomer").hide();
            },
            '.EditContextMenuCustomer click': function (el) {
                var id = el.attr('id');
                var customer = custRepo.GetCustomerById(id);
                $('#body').sales_customers_edit('load',customer);
            },
            '.CreatInvoiceContextMenuCustomer click': function (el) {
                var id = el.attr('id');
                var customer = custRepo.GetCustomerById(id);
                $('#body').sales_invoices_create('load', customer);
            },
            '.settingListCustomer click': function (el) {
                var index = el.attr('tabindex');
                $("tr#trCustomerList" + index + " td#tdDataCustomer" + index + " div.ContextMenuCustomer").show();
            },
            '.SelectAllCustomer change': function () {
                if ($('.SelectAllCustomer').attr('checked')) {
                    $('.SelectCustomer').attr('checked', 'checked')
                }
                else {
                    $('.SelectCustomer').removeAttr('checked')
                }
            },
            '.SelectCustomer change': function () {
                if ($('.SelectCustomer:checked').length == $('.SelectCustomer').length) {
                    $('.SelectAllCustomer').attr('checked', 'checked')
                }
                else {
                    $('.SelectAllCustomer').removeAttr('checked')
                }
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
            '#allCustomer click': function () {
                $('.Pagging').show();
                $this.ChangePage();
            },
            ChangePage: function () {
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + (((startPage - 1) * $('#limitData').val())) + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    beforSend: $this.LoadingListCustomer,
                    success: $this.requestAllCustomerSuccess
                });
                limit = $('#limitData').val();
                $this.initPagination();
                $this.CheckButtonPaging();
            },
            '#SearchCustomer keypress': function (el, ev) {
                if (ev.keyCode == "13") {
                    var key = $('#SearchCustomer').val();
                    $.ajax({
                        type: 'GET',
                        url: '/SearchCustomer/' + key,
                        dataType: 'json',
                        failure: $('#ListSearchCustomer').hide(),
                        success: $this.requestAllCustomerSuccess
                    });
                }
            },
            '#SearchCustomer focus': function () {
                $('.Pagging').hide();
                $(".DivSearch").attr("style", "background:#FFFFFF; border-color:#3BB9FF");
                $("#SearchCustomer").attr("style", "outline:none; background:#FFFFFF");
            },
            '#SearchCustomer blur': function () {
                $(".DivSearch").attr("style", "background:#F3F3F3");
                $("#SearchCustomer").attr("style", "background:#F3F3F3");
            },
            "input keypress": function (el, ev) {
                if (el.not($(":button"))) {
                    if (ev.keyCode == 13) {
                        var iname = el.val();
                        if (iname !== 'Simpan') {
                            var fields = el.parents('form:eq(0),body').find('button, input, textarea, select');
                            var index = fields.index(el);
                            if (index > -1 && (index + 1) < fields.length) {
                                fields.eq(index + 1).focus();
                            }
                            return false;
                        }
                    }
                }
            },
            "select keypress": function (el, ev) {
                if (el.not($(":button"))) {
                    if (ev.keyCode == 13) {
                        var iname = el.val();
                        if (iname !== 'Simpan') {
                            var fields = el.parents('form:eq(0),body').find('button, input, textarea, select');
                            var index = fields.index(el);
                            if (index > -1 && (index + 1) < fields.length) {
                                fields.eq(index + 1).focus();
                            }
                            return false;
                        }
                    }
                }
            }
        })

	});