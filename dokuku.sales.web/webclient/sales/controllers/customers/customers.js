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
            submit: function (el, ev) {
                var form = $("#setuporganization");
                var err = $("#error");
                var defaults = {
                    name: $("#name").val(),
                    timezone: $("#timezone").val(),
                    curr: $("#curr").val(),
                    starts: $("#starts").val()
                };
                err.empty();
                if (defaults.name !== "" && defaults.curr != 0)
                    form.submit();
                if (defaults.name == "")
                    $('<li>', { 'class': 'name', text: "Nama Organisasi harus di isi" }).appendTo(err.show());
                if (defaults.curr == 0)
                    $('<li>', { 'class': 'curr', text: "Mata Uang harus di pilih" }).appendTo(err.show());
                ev.preventDefault();
                return;
            }
        })

	});