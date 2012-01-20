steal("funcunit/qunit", "sales/fixtures", "sales/models/currency.js", function(){
	module("Model: Sales.models.currency")
	
	test("findAll", function(){
		expect(4);
		stop();
		Sales.models.currency.findAll({}, function(currencies){
			ok(currencies)
	        ok(currencies.length)
	        ok(currencies[0].name)
	        ok(currencies[0].description)
			start();
		});
		
	})
	
	test("create", function(){
		expect(3)
		stop();
		new Sales.models.currency({name: "dry cleaning", description: "take to street corner"}).save(function(currency){
			ok(currency);
	        ok(currency.id);
	        equals(currency.name,"dry cleaning")
	        currency.destroy()
			start();
		})
	})
	test("update" , function(){
		expect(2);
		stop();
		new Sales.models.currency({name: "cook dinner", description: "chicken"}).
	            save(function(currency){
	            	equals(currency.description,"chicken");
	        		currency.update({description: "steak"},function(currency){
	        			equals(currency.description,"steak");
	        			currency.destroy();
						start();
	        		})
	            })
	
	});
	test("destroy", function(){
		expect(1);
		stop();
		new Sales.models.currency({name: "mow grass", description: "use riding mower"}).
	            destroy(function(currency){
	            	ok( true ,"Destroy called" )
					start();
	            })
	})
})