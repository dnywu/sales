steal('funcunit').then(function(){

module("Sales.Controllers.Customers.Edit", { 
	setup: function(){
		S.open("//sales/controllers/customers/edit/edit.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Customers.Edit Demo","demo text");
});


});