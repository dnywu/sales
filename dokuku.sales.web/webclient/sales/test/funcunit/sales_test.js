steal("funcunit", function(){
	module("sales test", { 
		setup: function(){
			S.open("//sales/sales.html");
		}
	});
	
	test("Copy Test", function(){
		equals(S("h1").text(), "Welcome to JavaScriptMVC 3.2!","welcome text");
	});
})