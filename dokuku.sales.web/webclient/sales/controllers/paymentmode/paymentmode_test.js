steal('funcunit').then(function(){

module("Sales.Controllers.Paymentmode", { 
	setup: function(){
		S.open("//sales/controllers/paymentmode/paymentmode.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Paymentmode Demo","demo text");
});


});