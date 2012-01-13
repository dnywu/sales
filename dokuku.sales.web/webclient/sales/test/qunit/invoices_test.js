steal("funcunit/qunit", "sales/fixtures", "sales/models/invoices.js", function(){
	module("Model: Sales.Models.Invoices")
	
	test("findAll", function(){
		expect(4);
		stop();
		Sales.Models.Invoices.findAll({}, function(invoices){
			ok(invoices)
	        ok(invoices.length)
	        ok(invoices[0].name)
	        ok(invoices[0].description)
			start();
		});
		
	})
	
	test("create", function(){
		expect(3)
		stop();
		new Sales.Models.Invoices({name: "dry cleaning", description: "take to street corner"}).save(function(invoices){
			ok(invoices);
	        ok(invoices.id);
	        equals(invoices.name,"dry cleaning")
	        invoices.destroy()
			start();
		})
	})
	test("update" , function(){
		expect(2);
		stop();
		new Sales.Models.Invoices({name: "cook dinner", description: "chicken"}).
	            save(function(invoices){
	            	equals(invoices.description,"chicken");
	        		invoices.update({description: "steak"},function(invoices){
	        			equals(invoices.description,"steak");
	        			invoices.destroy();
						start();
	        		})
	            })
	
	});
	test("destroy", function(){
		expect(1);
		stop();
		new Sales.Models.Invoices({name: "mow grass", description: "use riding mower"}).
	            destroy(function(invoices){
	            	ok( true ,"Destroy called" )
					start();
	            })
	})
})