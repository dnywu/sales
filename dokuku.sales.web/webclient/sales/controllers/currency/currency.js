steal('jquery/controller',
      'jquery/view/ejs',
       './currency.css',
        'sales/repository/CurrencyandTaxRepository.js')
	.then('./views/listcurrency.ejs', './create/views/AddCurrency.ejs', './views/EditCurrency.ejs', function ($) {
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
    viewEditCurrency: function () {
        this.element.html(this.view("//sales/controllers/currency/views/EditCurrency.ejs"));
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
    "#checkboxAllCurrency change": function () {
        if ($("#checkboxAllCurrency").attr('checked')) {
            $(".checkboxCurrency").attr('checked', 'checked');
        } else {
            $(".checkboxCurrency").removeAttr('checked');
        }
    },
    '#deleteCurrency click': function () {
        $('.ConfirmationCurrency').show();
    },
    '.ButtonConfirmationCurrencyYa click': function () {
        $('.checkboxCurrency:checked').each(function (index) {
            $.ajax({
                type: 'Delete',
                url: '/DeleteCurrency/' + $(this).val(),
                dataType: 'json'
            });
        })
        $('.checkboxCurrency:checked').parent().parent().remove();
        $('.ConfirmationCurrency').hide();
    },
    '.ButtonConfirmationCurrencyTidak click': function () {
        $('.ConfirmationCurrency').hide();
    },
    'table.dataCurrency tbody.BodyDataCurrency tr.trDataCurrency hover': function (el) {
        var index = el.attr('tabindexCurrency');
        $('#settingListCurrency' + index).show();
        $("tr#trCurrencyList" + index + " td#tdDataCurrency" + index + " div.ContextMenuCurrency").hide();
    },
    'table.dataCurrency tbody.BodyDataCurrency tr.trDataCurrency mouseleave': function (el) {
        var index = el.attr('tabindexCurrency');
        $('#settingListCurrency' + index).hide();
        $("tr#trCurrencyList" + index + " td#tdDataCurrency" + index + " div.ContextMenuCurrency").hide();
    },
    '.settingCurrency click': function (el) {
        var index = el.attr('tabindexCurrency');
        $("tr#trCurrencyList" + index + " td#tdDataCurrency" + index + " div.ContextMenuCurrency").show();
    },
    '.EditContextMenuCurrency click': function (el) {
        var id = el.attr('id');
        var currency = currandtaxRepo.GetCurrencyById(id);
        $('#body').sales_currency('viewEditCurrency');
    },
         '#EditCurrency click': function (el, ev) {
                ev.preventDefault();
                var id = el.attr('id');
                var name = $("#editName").val();
                var symbol = $("#editSymbol").val();
                var format = $("#editFormat").val();
                var currency = new Object();
                currency._id
                currency.Name = name;
                currency.Code = symbol;
                currency.Rounding = format;
                $.ajax({
                    type: 'POST',
                    url: '/UpdateDataCurrency',
                    data: { 'data': JSON.stringify(currency) },
                    datatype: 'json',
                    success: function (data) { $("#body").sales_currency('load') }
                })
                    },

    requestAllCurrencySuccess: function (data) {
        $("table.dataCurrency tbody").empty();
        $.each(data, function (item) {
            $("table.dataCurrency tbody.BodyDataCurrency").append(
            "<tr class='trDataCurrency' id='trCurrencyList" + item + "' tabindexCurrency='" + item + "'>" +
            "<td class='thDataCurrency tdDataCurrency tdDataCurrencyCenter textAlignRight' style='text-align:center'><input type='checkbox' class='checkboxCurrency' id='checkboxCurrency'value='" + data[item]._id + "'/></td>" +
            "<td class='tdDataCurrencyCenter tdDataCurrency' id='tdDataCurrency" + item + "'><div class='settingCurrency' id='settingListCurrency" + item + "' tabindexCurrency='" + item + "'><img class='' src='/sales/controllers/currency/images/setting.png'/></div></td>" +
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
            $("td#tdDataCurrency" + item).append("//sales/controllers/currency/views/contextMenuCurrency.ejs", { index: item }, { id: data[item]._id });
        });
        $('.trDataCurrency:odd').addClass('odd');
    }
})

	});
