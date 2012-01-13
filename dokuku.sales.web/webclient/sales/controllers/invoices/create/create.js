steal('jquery/controller',
       'jquery/view/ejs',
	   'jquery/dom/form_params',
	   'jquery/controller/view',
       './createinvoices.css',
	   'sales/models')
	.then('./views/createinvoices.ejs', function ($) {
	    $.Controller('Sales.Invoices.Create',
        {
            defaults: (custid = 0, tabIndexTr = 0)
        },
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
                this.CreateListItem(3);
            },
            load: function () {
                this.element.html(this.view("//sales/controllers/invoices/create/views/createinvoices.ejs"));
                this.CreateListItem(3);
            },
            '#selectcust blur': function (el, ev) {
                $("#keteranganSelectCust").empty();
                var custName = el.val();
                if (custName == "") {
                    $("#keteranganSelectCust").text("Pelanggan harus di pilih");
                    $("#selectcust").focus().select();
                }
                else {
                    $.ajax({
                        type: 'GET',
                        url: '/getCustomerByCustomerName/' + custName,
                        dataType: 'json',
                        success: this.GetCustomerCallBack
                    });
                }
            },
            GetCustomerCallBack: function (data) {
                if (data == null) {
                    $("#keteranganSelectCust").text("Pelanggan ini tidak ditemukan");
                    $("#selectcust").focus().select();
                }
                else {
                    $("#keteranganSelectCust").text(data.Currency);
                    custid = data._id;
                }
            },
            '#addItemRow click': function () {
                this.CreateListItem(1);
            },
            '#itemInvoice tbody tr hover': function (el) {
                var index = el.attr('tabindex');
                $("#deleteItem" + index).show();
            },
            "#itemInvoice tbody tr mouseleave": function (el) {
                var index = el.attr("tabindex");
                $("#deleteItem" + index).hide();
            },
            CreateListItem: function (count) {
                while (count > 0) {
                    $("#itemInvoice tbody").append("<tr tabindex='" + tabIndexTr + "'><td><input type='text'></input></td>" +
                                      "<td><textarea></textarea></td>" +
                                      "<td><input type='text'></input></td>" +
                                      "<td><input type='text'></input></td>" +
                                      "<td><input type='text'></input></td>" +
                                      "<td><select name='tax'>" +
                                      "<option>None</option>" +
                                      "</select><td><td></td><td valign='middle'><div class='clsDeleteItem' id='deleteItem" + tabIndexTr + "'>X</div></td></tr>");
                    count--;
                    tabIndexTr++;
                }
            }
        })
	});