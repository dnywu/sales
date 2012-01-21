steal('funcunit').then(function(){

module("Sales.Controllers.Invoices.Edit", { 
	setup: function(){
		S.open("//sales/controllers/invoices/edit/edit.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Invoices.Edit Demo","demo text");
});


});