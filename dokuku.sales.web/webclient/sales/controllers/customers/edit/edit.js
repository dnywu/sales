steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view')
	.then('./views/editCustomer.ejs', function ($) {

	    $.Controller('sales.Controllers.customers.edit',
        {
            init: function (el,ev,item) {
                //var item = this.GetDataCustomer(id);
                this.element.html("//sales/controllers/customers/edit/views/editCustomer.ejs", { 'item': item });
                $('#inputSelect_PersonName option[value="' + item.Salutation + '"]').attr('selected', 'selected')
            },
            load: function (item) {
                //var item = this.GetDataCustomer(id);
                this.element.html("//sales/controllers/customers/edit/views/editCustomer.ejs", { 'item': item });
                $('#inputSelect_PersonName option[value="' + item.Salutation + '"]').attr('selected', 'selected')
            },
            '#EditCustomers submit': function (el, ev) {
                ev.preventDefault();
                var form = $('#EditCustomers').formParams();
                var data = JSON.stringify(form);
                $.ajax({
                    type: 'POST',
                    url: '/UpdateDataCustomer/data',
                    data: { 'data': data },
                    datatype: 'json',
                    success: function (data) { $("#body").sales_customers('load') }
                });
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


