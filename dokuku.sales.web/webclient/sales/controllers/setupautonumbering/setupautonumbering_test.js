steal('funcunit').then(function(){

module("Sales.Controllers.Setupautonumbering", { 
	setup: function(){
		S.open("//sales/controllers/setupautonumbering/setupautonumbering.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Setupautonumbering Demo","demo text");
});


});