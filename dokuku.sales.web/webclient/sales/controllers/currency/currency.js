steal('jquery/controller',
      'jquery/view/ejs',
       './currency.css',
        'sales/repository/CurrencyandTaxRepository.js')
	.then('./views/listcurrency.ejs', './create/views/AddCurrency.ejs', function ($) {
	    $.Controller('Sales.Controllers.Currency',
{
    defaults: (currandtaxRepo = new CurrencyandTaxRepository())
},

{
    init: function () {
        var currandtaxRepo = new CurrencyandTaxRepository()
        var dataCurrency = currandtaxRepo.getAllCurrency();
        this.element.html(this.view("//sales/controllers/currency/views/listcurrency.ejs"));
        this.requestAllCurrencySuccess(dataCurrency);
    },
    load: function () {
        var currandtaxRepo = new CurrencyandTaxRepository()
        var dataCurrency = currandtaxRepo.getAllCurrency();
        this.element.html(this.view("//sales/controllers/currency/views/listcurrency.ejs"));
        this.requestAllCurrencySuccess(dataCurrency);
    },
    viewAddCurrency: function () {
        this.element.html(this.view("//sales/controllers/currency/create/views/AddCurrency.ejs"));
    },
    "#CurrencySave click": function (el, ev) {
        ev.preventDefault();
        var currandtaxRepo = new CurrencyandTaxRepository()
        var name = $("#inputName").val();
        var symbol = $("#inputSymbol").val();
        var format = $("#inputFormat").val();
        var currency = new Object();
        currency.Name = name;
        currency.Code = symbol;
        currency.Rounding = format;
        if (currandtaxRepo.SaveCurrency(currency)) {
            $("#body").sales_currency("load");
        }
    },
    "#CancelCurrency click": function () {
        $("#body").sales_currency("load");
    },
    requestAllCurrencySuccess: function (data) {
        $("table.dataCurrency tbody").empty();
        $.each(data, function (item) {
            $("table.dataCurrency tbody.BodyDataCurrency").append(
            "<tr class='trDataCurrency' id='trCurrencyList" + item + "' tabindex='" + item + "'>" +
            "<td class='thDataCurrency tdDataCurrency tdDataCurrencyCenter textAlignRight' style='text-align:center'><input type='checkbox' name='SelectAll' class='SelectCustomer' value='" + data[item]._id + "'/></td>" +
            "<td class='tdDataCurrencyCenter tdDataCurrency' id='tdDataCurrency" + item + "'><div class='settingListCurrency' id='settingListCurrency" + item + "' tabindex='" + item + "'><img class='' src='/sales/controllers/tax/images/setting.png'/></div></td>" +
            "<td class='tdDataCurrencyLeft tdDataCurrency'>" +
                "<div width = '100%'>" + data[item].Name + "</div>" +
            "</td>" +
            "<td class='tdDataCurrencyLeft tdDataCurrency'>" +
                "<div width = '100%'>" + data[item].Code + "</div>" +
            "</td>" +
            "<td class='tdDataCurrencyRight tdDataCurrency'>" +
                "<div width = '100%'>" + data[item].Rounding + "</div>" +
            "</td>" +
            "</tr>");
            $("td#tdDataCurrency" + item).append("//sales/controllers/customers/views/contextMenuCustomer.ejs", { index: item }, { id: data[item]._id });
        });
        $('.trDataCurrency:odd').addClass('odd');
    }
})

	});
