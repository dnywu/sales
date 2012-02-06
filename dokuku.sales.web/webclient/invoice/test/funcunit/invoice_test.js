steal("funcunit", function(){
	module("invoice test", { 
		setup: function(){
			S.open("//invoice/invoice.html");
		}
	});
	
	test("Copy Test", function(){
		equals(S("h1").text(), "Welcome to JavaScriptMVC 3.2!","welcome text");
	});
})