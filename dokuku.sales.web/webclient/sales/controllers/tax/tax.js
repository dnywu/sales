steal('jquery/controller',
        'jquery/view/ejs',
         './tax.css',
        'sales/repository/CurrencyandTaxRepository.js')
	.then('./views/listtax.ejs', './views/addtax.ejs', './views/edittax.ejs', function ($) {
	    $.Controller('Sales.Controllers.Tax',
{
    defaults: (currandtaxRepo = new CurrencyandTaxRepository())
},

{
    init: function () {
        var currandtaxRepo = new CurrencyandTaxRepository()
        var data = currandtaxRepo.getAllTax();
        this.element.html(this.view('//sales/controllers/tax/views/listtax.ejs'));
        this.requestAllTaxSuccess(data);
    },
    load: function () {
        var currandtaxRepo = new CurrencyandTaxRepository()
        var data = currandtaxRepo.getAllTax();
        this.element.html(this.view('//sales/controllers/tax/views/listtax.ejs'));
        this.requestAllTaxSuccess(data);
    },
    viewAddTax: function () {
        this.element.html(this.view('//sales/controllers/tax/views/addtax.ejs'));
    },
    viewEditTax: function (item) {
        this.element.html('//sales/controllers/tax/views/edittax.ejs', item);
    },
    "#TaxSave click": function (el, ev) {
        ev.preventDefault();
        var currandtaxRepo = new CurrencyandTaxRepository()
        var code = $("#inputText_PajakKode").val();
        var name = $("#inputText_PajakName").val();
        var persentase = $("#inputText_Persentage").val();
        var tax = new Object();
        tax.Code = code;
        tax.Name = name;
        tax.Value = persentase;
        if (currandtaxRepo.SaveTax(tax)) {
            $("#body").sales_tax("load");
        }
    },
    "#Canceltax click": function () {
        $("#body").sales_tax("load");
    },
    "#CheckboxAllTax change": function () {
        if ($("#CheckboxAllTax").attr('checked')) {
            $(".SelectTax").attr('checked', 'checked');
        } else {
            $(".SelectTax").removeAttr('checked');
        }
    },
    '#deleteTax click': function () {
        $('.MessageConfirmationTax').show();
    },
    '.ButtonConfirmationTaxYa click': function () {
        $('.SelectTax:checked').each(function (index) {
            $.ajax({
                type: 'Delete',
                url: '/DeleteTax/' + $(this).val(),
                dataType: 'json'
            });
        })
        $('.SelectTax:checked').parent().parent().remove();
        $('.MessageConfirmationTax').hide();
    },
    '.ButtonConfirmationTaxTidak click': function () {
        $('.MessageConfirmationTax').hide();
    },
    'table.dataTax tbody.BodyDataTax tr.trDataTax hover': function (el) {
        var index = el.attr('tabindex');
        $('#settingListTax' + index).show();
        $("tr#trTaxList" + index + " td#tdDataTax" + index + " div.ContextMenuTax").hide();
    },
    'table.dataTax tbody.BodyDataTax tr.trDataTax mouseleave': function (el) {
        var index = el.attr('tabindex');
        $('#settingListTax' + index).hide();
        $("tr#trTaxList" + index + " td#tdDataTax" + index + " div.ContextMenuTax").hide();
    },
    '.settingListTax click': function (el) {
        var index = el.attr('tabindex');
        $("tr#trTaxList" + index + " td#tdDataTax" + index + " div.ContextMenuTax").show();
    },
    '.EditContextMenuTax click': function (el) {
        var id = el.attr('id');
        var currandtaxRepo = new CurrencyandTaxRepository()
        var Tax = currandtaxRepo.GetTaxById(id);
        $('#body').sales_tax('viewEditTax', Tax);
    },
    '#EditTax click': function (el, ev) {
        ev.preventDefault();
        var code = $("#inputText_PajakKode").val();
        var name = $("#inputText_PajakName").val();
        var persentase = $("#inputText_Persentage").val();
        var id = $("#_id").val();
        var tax = new Object();
        tax.Code = code;
        tax.Name = name;
        tax.Value = persentase;
        tax._id = id;
        $.ajax({
            type: 'POST',
            url: '/UpdateTax',
            data: { 'data': JSON.stringify(tax) },
            datatype: 'json',
            success: function (data) { $("#body").sales_tax('load') }
        })
    },
    requestAllTaxSuccess: function (data) {
        $("table.dataTax tbody").empty();
        $.each(data, function (item) {
            $("table.dataTax tbody.BodyDataTax").append(
                        "<tr class='trDataTax' id='trTaxList" + item + "' tabindex='" + item + "'>" +
                        "<td class='thDataTax tdDataTax tdDataTaxCenter textAlignRight' style='text-align:center'><input type='checkbox' class='SelectTax' id='CheckboxTax' value='" + data[item]._id + "'/></td>" +
                        "<td class='tdDataTaxCenter tdDataTax' id='tdDataTax" + item + "'><div class='settingListTax' id='settingListTax" + item + "' tabindex='" + item + "'><img class='' src='/sales/controllers/tax/images/setting.png'/></div></td>" +
                        "<td class='tdDataTaxLeft tdDataTax'>" +
                            "<div class='tax' width = '100%'>" + data[item].Code + "</div>" +
                        "</td>" +
                        "<td class='tdDataTaxLeft tdDataTax'>" +
                            "<div class='tax' width = '100%'>" + data[item].Name + "</div>" +
                        "</td>" +
                        "<td class='tdDataTaxRight tdDataTax'>" +
                            "<div class='tax' width = '100%'>" + data[item].Value + "</div>" +
                        "</td>" +
                        "</tr>");
            $("td#tdDataTax" + item).append("//sales/controllers/tax/views/contextMenuTax.ejs", { index: item }, { id: data[item]._id });
        });
        $('.trDataTax:odd').addClass('odd');
    }
})

	});
