steal('funcunit').then(function(){

module("Sales.Controllers.Currencyandtax", { 
	setup: function(){
		S.open("//sales/controllers/currencyandtax/currencyandtax.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Currencyandtax Demo","demo text");
});


});