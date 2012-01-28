steal('funcunit').then(function(){

module("Sales.Controllers.Payment", { 
	setup: function(){
		S.open("//sales/controllers/payment/payment.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Payment Demo","demo text");
});


});