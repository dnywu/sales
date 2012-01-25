steal('jquery/controller',
        'jquery/view/ejs',
        'sales/repository/CurrencyandTaxRepository.js')
	.then('./views/init.ejs','./views/addtax.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Tax
	    */
	    $.Controller('Sales.Controllers.Tax',
	    /** @Static */
{
defaults: (currandtaxRepo = new CurrencyandTaxRepository())
},
	    /** @Prototype */
{
init: function () {
    var currandtaxRepo = new CurrencyandTaxRepository()
    currandtaxRepo.getAllTax();
    this.element.html(this.view('//sales/controllers/tax/views/listtax.ejs'));
},
load: function () {
    var currandtaxRepo = new CurrencyandTaxRepository()
    currandtaxRepo.getAllTax();
    this.element.html(this.view('//sales/controllers/tax/views/listtax.ejs'));
},
viewAddTax: function () {
    this.element.html(this.view('//sales/controllers/tax/views/addtax.ejs'));
},
"#TaxSave click": function (el, ev) {
    ev.preventDefault();
    var currandtaxRepo = new CurrencyandTaxRepository()
    var name = $("#inputText_PajakName").val();
    var persentase = $("#inputText_Persentage").val();
    var tax = new Object();
    tax.Name = name;
    tax.persentase = persentase;
    if (currandtaxRepo.SaveTax(tax)) {
        alert("data telah disimpan");
    }
},
"#Canceltax click": function () {
    $("#body").sales_tax("load");
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