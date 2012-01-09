steal('funcunit').then(function(){

module("Sales.Controllers.Customers", { 
	setup: function(){
		S.open("//sales/controllers/customers/customers.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Customers Demo","demo text");
});


});