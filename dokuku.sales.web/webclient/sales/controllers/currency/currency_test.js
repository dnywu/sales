steal('funcunit').then(function(){

module("Sales.Controllers.Currency", { 
	setup: function(){
		S.open("//sales/controllers/currency/currency.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Currency Demo","demo text");
});


});