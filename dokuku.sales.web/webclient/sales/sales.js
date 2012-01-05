steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
    'sales/controllers/items/create',
    'sales/controllers/setuporganization',
    'sales/controllers/home',
	function () {					// configure your application
	    var invoice = 0;
	    if (invoice > 0) {
	        $('body').empty();
	        $('body').sales_setuporganization();
	    }
	    else {
	        $('body').sales_home();
	        $('#section').sales_items_create();
	    }
	})