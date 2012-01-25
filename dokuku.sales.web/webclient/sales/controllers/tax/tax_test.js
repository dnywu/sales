steal('funcunit').then(function(){

module("Sales.Controllers.Tax", { 
	setup: function(){
		S.open("//sales/controllers/tax/tax.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Tax Demo","demo text");
});


});