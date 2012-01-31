steal('funcunit').then(function(){

module("Sales.Controllers.Paymentreceived", { 
	setup: function(){
		S.open("//sales/controllers/paymentreceived/paymentreceived.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Paymentreceived Demo","demo text");
});


});