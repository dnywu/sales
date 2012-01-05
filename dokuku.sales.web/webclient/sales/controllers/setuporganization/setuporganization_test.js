steal('funcunit').then(function(){

module("Invoice.Controllers.Setuporganization", { 
	setup: function(){
		S.open("//invoice/controllers/setuporganization/setuporganization.html");
	}
});

test("Text Test", function(){
	equals(S("h1").text(), "Invoice.Controllers.Setuporganization Demo","demo text");
});


});