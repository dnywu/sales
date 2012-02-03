steal('funcunit').then(function(){

module("Sales.Controllers.Paymentterms", { 
	setup: function(){
		S.open("//sales/controllers/paymentterms/paymentterms.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Paymentterms Demo","demo text");
});


});