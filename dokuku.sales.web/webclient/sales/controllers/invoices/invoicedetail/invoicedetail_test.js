steal('funcunit').then(function(){

module("Sales.Controllers.Invoices.Invoicedetail", { 
	setup: function(){
		S.open("//sales/controllers/invoices/invoicedetail/invoicedetail.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Invoices.Invoicedetail Demo","demo text");
});


});