steal("funcunit/qunit", "sales/fixtures", "sales/models/companyprofile.js", function(){
	module("Model: Sales.Models.Companyprofile")
	
	test("findAll", function(){
		expect(4);
		stop();
		Sales.Models.Companyprofile.findAll({}, function(companyprofiles){
			ok(companyprofiles)
	        ok(companyprofiles.length)
	        ok(companyprofiles[0].name)
	        ok(companyprofiles[0].description)
			start();
		});
		
	})
	
	test("create", function(){
		expect(3)
		stop();
		new Sales.Models.Companyprofile({name: "dry cleaning", description: "take to street corner"}).save(function(companyprofile){
			ok(companyprofile);
	        ok(companyprofile.id);
	        equals(companyprofile.name,"dry cleaning")
	        companyprofile.destroy()
			start();
		})
	})
	test("update" , function(){
		expect(2);
		stop();
		new Sales.Models.Companyprofile({name: "cook dinner", description: "chicken"}).
	            save(function(companyprofile){
	            	equals(companyprofile.description,"chicken");
	        		companyprofile.update({description: "steak"},function(companyprofile){
	        			equals(companyprofile.description,"steak");
	        			companyprofile.destroy();
						start();
	        		})
	            })
	
	});
	test("destroy", function(){
		expect(1);
		stop();
		new Sales.Models.Companyprofile({name: "mow grass", description: "use riding mower"}).
	            destroy(function(companyprofile){
	            	ok( true ,"Destroy called" )
					start();
	            })
	})
})