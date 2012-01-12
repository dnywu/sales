steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/customers/customer.css')
	.then('./views/listCustomer.ejs', function ($) {

	    $.Controller('sales.Controllers.customers',
        {
            defaults: (jumlahdata = 0, start = 1, limit = 1, page = 1, totalPage = 1, $this = null)
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
                $this.initPagination();
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + start + '/limit/' + limit,
                    dataType: 'json',
                    success: $this.requestAllCustomerSuccess
                });
                $('#idInputPage').val(1);
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
                    $("table.dataCustomer tbody").append(
                        "<tr class='trDataCustomer'>" +
                        "<td class='thDataCustomer tdDataCustomerCenter' style='text-align:center'><input type='checkbox' name='SelectAll' class='SelectCustomer'/></td>" +
                        "<td class='thDataCustomer tdDataCustomerCenter'></td>" +
                        "<td class='tdDataCustomerLeft'>" + data[item].Name + "</td>" +
                        "<td class='tdDataCustomerRight'>Rp. 00</td>" +
                        "<td class='tdDataCustomerRight'>Rp. 00</td>" +
                        "</tr>");
                });
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
            '.prev click': function () {
                //jumlahdata = data;
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                if (isNaN(startPage))
                    startPage = 1;
                else
                    startPage--;

                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + startPage + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    success: $this.requestAllCustomerSuccess
                });
                $('#idInputPage').val(startPage);
            },
            '.next click': function () {
                //jumlahdata = data;
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                if (isNaN(startPage))
                    startPage = 2;
                else
                    startPage++;
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + startPage + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    success: $this.requestAllCustomerSuccess
                });
                $('#idInputPage').val(startPage);
            },
            '.last click': function () {
                //jumlahdata = data;
                $this.initPagination();
                var startPage = parseInt($('#totalPage').text());
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + startPage + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    success: $this.requestAllCustomerSuccess
                });
                $('#idInputPage').val(startPage);
            },
            '.first click': function () {
                //jumlahdata = data;
                $this.initPagination();
                var startPage = 1;
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + startPage + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    success: $this.requestAllCustomerSuccess
                });
                $('#idInputPage').val(startPage);
            },
            '#idInputPage change': function () {
                //jumlahdata = data;
                $this.initPagination();
                var startPage = parseInt($('#idInputPage').val());
                $.ajax({
                    type: 'GET',
                    url: '/LimitCustomers/start/' + startPage + '/limit/' + $('#limitData').val(),
                    dataType: 'json',
                    success: $this.requestAllCustomerSuccess
                });
            }

        })

	});