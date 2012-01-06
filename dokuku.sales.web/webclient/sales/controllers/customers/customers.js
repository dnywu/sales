steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/customers/customer.css')
	.then('./views/customer.ejs', function ($) {

	    $.Controller('sales.Controllers.customers',
        {
            init: function () {
                this.element.html(this.view('//sales/controllers/customers/views/customer.ejs'));
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
                }
            },
            '#addCustomField click': function () {
                $("#CustomField").show();
                $("#DivCustomField").hide();
            },
            '#copyField click': function () {
                $('#inputTextarea_NewCust_BillingAddress').val($('#inputTextarea_BillingAddress').val());
                $('#inputText_NewCust_City').val($('#inputText_City').val());
                $('#inputText_NewCust_StateProvince').val($('#inputText_StateProvince').val());
                $('#inputText_NewCust_ZIPPostalCode').val($('#inputText_ZIPPostalCode').val());
                $('#inputText_NewCust_Fax').val($('#inputText_Fax').val());
            }
        })

	});