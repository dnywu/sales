steal('jquery/controller', 'jquery/view/ejs', './currency.css')
	.then('./views/listcurrency.ejs','./create/views/AddCurrency.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Currency
	    */
	    $.Controller('Sales.Controllers.Currency',
	    /** @Static */
    {
    defaults: {}
},
	    /** @Prototype */
    {
            init: function () {
                this.element.html(this.view("//sales/controllers/currency/views/listcurrency.ejs"));
            },
            load: function () {
                this.element.html(this.view("//sales/controllers/currency/views/listcurrency.ejs"));
            },
            viewAddCurrency: function () {
                this.element.html(this.view("//sales/controllers/currency/create/views/AddCurrency.ejs"));
            },
            "#CurrencySave click": function (el, ev) {
                ev.preventDefault();
                var currandtaxRepo = new CurrencyandTaxRepository()
                var name = $("#inputText_PajakName").val();
                var persentase = $("#inputText_Persentage").val();
                var currency = new Object();
                currency.Name = name;
                currency.persentase = persentase;
                if (currandtaxRepo.SaveCurrency(currency)) {
                    alert("data telah disimpan");
                }
            },
            "#CancelCurrency click": function () {
                $("#body").sales_currency("load");
            },
            requestAllCustomerSuccess: function (data) {
                $("table.dataCustomer tbody").empty();
                $.each(data, function (item) {
                    $("table.dataCustomer tbody.BodyDataCustomer").append(
                        "<tr class='trDataCustomer' id='trCustomerList" + item + "' tabindex='" + item + "'>" +
                        "<td class='thDataCustomer tdDataCustomer tdDataCustomerCenter textAlignRight' style='text-align:center'><input type='checkbox' name='SelectAll' class='SelectCustomer' value='" + data[item]._id + "'/></td>" +
                        "<td class='tdDataCustomerCenter tdDataCustomer' id='tdDataCustomer" + item + "'><div class='settingListCustomer' id='settingListCustomer" + item + "' tabindex='" + item + "'><img class='' src='/sales/controllers/tax/images/setting.png'/></div></td>" +
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
            }
})

	});
