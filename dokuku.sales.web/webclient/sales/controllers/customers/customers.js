steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/customers/customer.css')
	.then('./views/listCustomer.ejs', function ($) {

	    $.Controller('sales.Controllers.customers',
        {
            onDocument: true
        },
        {
            init: function () {
                this.element.html(this.view('//sales/controllers/customers/views/listCustomer.ejs'));
                this.RequestAllCustomer();
            },
            load: function () {
                this.element.html(this.view('//sales/controllers/customers/views/listCustomer.ejs'));
                this.RequestAllCustomer();
            },
            RequestAllCustomer: function () {
                $.ajax({
                    type: 'GET',
                    url: '/Customers',
                    dataType: 'json',
                    success: this.requestAllCustomerSuccess
                });
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
            }
        })

	});