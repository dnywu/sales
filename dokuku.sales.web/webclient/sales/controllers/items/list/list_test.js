steal('funcunit').then(function(){

module("Sales.Controllers.Items.List", { 
	setup: function(){
		S.open("//sales/controllers/items/list/list.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Items.List Demo","demo text");
});


});