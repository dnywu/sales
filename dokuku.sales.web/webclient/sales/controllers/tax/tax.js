steal('jquery/controller',
        'jquery/view/ejs',
        'sales/repository/CurrencyandTaxRepository.js')
	.then('./views/init.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Tax
	    */
	    $.Controller('Sales.Controllers.Tax',
	    /** @Static */
{
defaults: {}
},
	    /** @Prototype */
{
init: function () {
    var CurrencyandTaxRepo = new CurrencyandTaxRepository();
    CurrencyandTaxRepo.getAllTax();
    this.element.html(this.view('//sales/controllers/tax/views/listtax.ejs'));
},
load: function () {
    var CurrencyandTaxRepo = new CurrencyandTaxRepository();
    CurrencyandTaxRepo.getAllTax();
    this.element.html(this.view('//sales/controllers/tax/views/listtax.ejs'));
},
viewAddTax: function () {
    this.element.html(this.view('//sales/controllers/tax/views/addtax.ejs'));
}

})

	});