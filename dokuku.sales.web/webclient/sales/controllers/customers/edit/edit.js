steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view')
	.then('./views/editCustomer.ejs', function ($) {

	    $.Controller('sales.Controllers.customers.edit',
        {
            init: function (el, ev, id) {
                var item = this.GetDataCustomer(id);
                this.element.html("//sales/controllers/customers/edit/views/editCustomer.ejs", { 'item': item });
            },
            load: function (id) {
                var item = this.GetDataCustomer(id);
                this.element.html("//sales/controllers/customers/edit/views/editCustomer.ejs", { 'item': item });
            },
            GetDataCustomer: function (id) {
                var item = null;
                $.ajax({
                    type: 'GET',
                    url: '/GetDataCustomer/id/' + id,
                    datatype: 'json',
                    async: false,
                    success: function (data) {
                        item = data;
                    }
                });
                return item;
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
            }
        })
	});


