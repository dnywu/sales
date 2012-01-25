steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './autonumbering.css')
	.then('./views/setupautonumbering.ejs', function ($) {
	    $.Controller('Sales.Controllers.Setupautonumbering',
        {
            init: function (el, ev) {
                var item = this.getAutoNumber();
                this.element.html('//sales/controllers/setupautonumbering/views/setupautonumbering.ejs', { 'item': item });
                $('input:radio[name=Mode value="' + item.mode + '"]').attr('checked', 'checked')
            },

            load: function () {
                var item = this.getAutoNumber();
                this.element.html('//sales/controllers/setupautonumbering/views/setupautonumbering.ejs', { 'item': item });
                $('input:radio[name=Mode value="' + item.mode + '"]').attr('checked', 'checked')
            },
            getAutoNumber: function () {
                var item = null;
                $.ajax({
                    type: 'GET',
                    url: '/InvoiceAutoNumber',
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        item = data;
                    }
                });
                return item;
            },
            '#cancelAutoNumber click': function () {
                $("#input-prefix").val('');
                $("#input-prefix").focus();
            },
            '#autoNumberForm submit': function (el, ev) {
                var form = $('#autoNumberForm').formParams();
                var prefix = $('#input-prefix').val();
                var mode = $('input:radio[name=Mode]:checked').val();

                var item = new Object;
                item.prefix = prefix;
                item.mode = mode;

                $.ajax({
                    type: 'POST',
                    url: '/SetupInvoiceAutoNumber',
                    data: { 'data': JSON.stringify(item) },
                    datatype: 'json',
                    success: function (data) {
                        $("#body").sales_setupautonumbering("load");
                    }
                });
                ev.preventDefault();
            }
        })

	});