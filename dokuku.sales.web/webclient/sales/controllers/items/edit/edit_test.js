steal('funcunit').then(function(){

module("Sales.Controllers.Items.Edit", { 
	setup: function(){
		S.open("//sales/controllers/items/edit/edit.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Items.Edit Demo","demo text");
});


});