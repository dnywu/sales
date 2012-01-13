steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/customers/customer.css')
	.then('./views/listCustomer.ejs', function ($) {

	    $.Controller('sales.Controllers.customers',
        {
            defaults: (jumlahdata = 0, start = 1, page = 1, totalPage = 1, $this = null)
        },
        {
            init: function () {
                $this = this;
                this.element.html(this.view('//sales/controllers/customers/views/listCustomer.ejs'));
                this.RequestAllCustomer();
            },
            load: function () {
                $this = this;
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
                    url: '/LimitCustomers/start/' + (((start - 1) * limit) + 1) + '/limit/' + limit,
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
                var form = $("#AddCustomers");
                ev.preventDefault();
                $.ajax({
                    type: "POST",
                    url: "/customer",
                    data: form.serialize(),
                    dataType: "json",
                    success: function (data) {
                        if (data == "OK") {
                            $("#body").sales_customers('load');
                        }
                    }
                });

            },
            '.BtnTambahCustomer click': function () {
                this.element.html(this.view('//sales/controllers/customers/views/addcustomer.ejs'));
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
                        "<tr class='trDataCustomer'>" +
                        "<td class='thDataCustomer tdDataCustomerCenter' style='text-align:center'><input type='checkbox' name='SelectAll' class='SelectCustomer'/></td>" +
                        "<td class='thDataCustomer tdDataCustomerCenter'></td>" +
                        "<td class='tdDataCustomerLeft'>" + data[item].Name + "</td>" +
                        "<td class='tdDataCustomerRight'>Rp. 00</td>" +
                        "<td class='tdDataCustomerRight'>Rp. 00</td>" +
                        "</tr>");
                });
                $('.trDataCustomer:odd').addClass('odd');
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
            ChangePage: function () {
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + (((startPage - 1) * $('#limitData').val()) + 1) + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    beforSend: $this.LoadingListCustomer,
                    success: $this.requestAllCustomerSuccess
                });
                limit = $('#limitData').val();
                $this.initPagination();
                $this.CheckButtonPaging();
            }
        })

	});