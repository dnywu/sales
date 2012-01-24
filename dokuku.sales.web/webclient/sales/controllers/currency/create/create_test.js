steal('funcunit').then(function(){

module("Sales.Controllers.Currency.Create", { 
	setup: function(){
		S.open("//sales/controllers/currency/create/create.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Currency.Create Demo","demo text");
});


});