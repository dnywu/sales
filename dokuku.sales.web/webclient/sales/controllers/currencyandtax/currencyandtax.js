steal('jquery/controller', 'jquery/view/ejs',
 'sales/controllers/currencyandtax/currencyandtax.css',
 'sales/controllers/tax',
 'sales/controllers/currency'
 )
	.then('./views/listcurrencyandtax.ejs', function ($) {
$.Controller('Sales.Controllers.Currencyandtax',
{
defaults: {}
},

{
init: function () {
    this.element.html(this.view('//sales/controllers/currencyandtax/views/listcurrencyandtax.ejs'));

},
load: function () {
    this.element.html(this.view('//sales/controllers/currencyandtax/views/listcurrencyandtax.ejs'));
},
'#img-currency click': function () {
    this.ClearContain();
    $("#body").sales_currency('load');
},
'#currencyLink click': function () {
    this.ClearContain();
    $("#body").sales_currency('load');

},
'#img-tax click': function () {
    this.ClearContain();
    $("#body").sales_tax('load');
},
'#taxLink click': function () {
    this.ClearContain();
    $("#body").sales_tax('load');
},
'#btn-add-tax click': function () {
    this.ClearContain();
    $("#body").sales_tax('viewAddTax');
},
'#btn-add-currency click': function () {
    this.ClearContain();
    $("#body").sales_currency('viewAddCurrency');
},
ClearContain: function () {
    $("#body").empty();
}

})
	});