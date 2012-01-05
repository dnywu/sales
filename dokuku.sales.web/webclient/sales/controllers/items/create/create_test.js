steal('funcunit').then(function(){

module("Sales.Controllers.Items.Create", { 
	setup: function(){
		S.open("//sales/controllers/items/create/create.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Items.Create Demo","demo text");
});


});