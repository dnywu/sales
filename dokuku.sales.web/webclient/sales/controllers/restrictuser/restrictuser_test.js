steal('funcunit').then(function(){

module("Sales.Controllers.Restrictuser", { 
	setup: function(){
		S.open("//sales/controllers/restrictuser/restrictuser.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Sales.Controllers.Restrictuser Demo","demo text");
});


});