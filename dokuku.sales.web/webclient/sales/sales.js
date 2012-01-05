steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
    'sales/controllers/nav',
    'sales/controllers/items/create',
	function () {					// configure your application
	    $('#section').sales_items_create()
	})