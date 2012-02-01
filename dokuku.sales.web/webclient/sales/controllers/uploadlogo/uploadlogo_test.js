steal('funcunit').then(function(){

module("Sales.Controllers.Uploadlogo", { 
	setup: function(){
		S.open("//sales/controllers/uploadlogo/uploadlogo.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Uploadlogo Demo","demo text");
});


});