// map fixtures for this application

steal("jquery/dom/fixture", function(){
	
	$.fixture.make("companyprofile", 5, function(i, companyprofile){
		var descriptions = ["grill fish", "make ice", "cut onions"]
		return {
			name: "companyprofile "+i,
			description: $.fixture.rand( descriptions , 1)[0]
		}
	})
	$.fixture.make("invoices",20, function(i, invoices){
		var descriptions = ["grill fish", "make ice", "cut onions"]
		return {
			name: "invoices "+i,
			description: $.fixture.rand( descriptions , 1)[0]
		}
	})
})