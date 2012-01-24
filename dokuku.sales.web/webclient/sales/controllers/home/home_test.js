steal('funcunit').then(function(){

module("Sales.Controllers.Home", { 
	setup: function(){
		S.open("//sales/controllers/home/home.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Home Demo","demo text");
});


});