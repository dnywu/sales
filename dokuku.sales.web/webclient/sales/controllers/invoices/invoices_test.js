steal('funcunit').then(function(){

module("Sales.Controllers.Invoices", { 
	setup: function(){
		S.open("//sales/controllers/invoices/invoices.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Invoices Demo","demo text");
});


});