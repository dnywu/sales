steal( 'jquery/controller',
       'jquery/view/ejs', 
       './addcurrency.css', 
       'sales/repository/CurrencyandTaxRepository.js')
	.then( './views/AddCurrency.ejs', function($){
$.Controller('Sales.Controllers.Currency.Create',
{
    defaults: (currandtaxRepo = new CurrencyandTaxRepository())
},
{
	init : function(){
	    this.element.html(this.view("//sales/controllers/currency/create/views/AddCurrency.ejs"));
	},
    load : function(){
	    this.element.html(this.view("//sales/controllers/currency/create/views/AddCurrency.ejs"));
	}
//    "#CurrencySave click": function (el, ev) {
//    ev.preventDefault();
//    var currandtaxRepo = new CurrencyandTaxRepository()
//    var name = $("#inputName").val();
//    var symbol = $("#inputSymbol").val();
//    var currency = new Object();
//    currency.Name = name;
//    currency.Code = symbol;
//    if (currandtaxRepo.SaveCurrency(currency)) {
//        $("#body").sales_currency("load");
//    }
//    },
//    "#CancelCurrency click": function () {
//        $("#body").sales_currency("load");
//    }
});
}
});