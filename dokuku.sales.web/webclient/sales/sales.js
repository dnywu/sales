steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
<<<<<<< HEAD
    'sales/controllers/home',
    'sales/controllers/setuporganization',
=======
    'sales/controllers/items/create',
    'sales/controllers/setuporganization',
    'sales/controllers/customers',
    'sales/controllers/home',
>>>>>>> origin/master
	function () {					// configure your application
	    $.ajax({
	        type: 'GET',
	        url: '/getuser',
	        dataType: 'json',
	        success: GetUserCallback
	    });
	    $.ajax({
	        type: 'GET',
	        url: '/getorganization',
	        dataType: 'json',
	        success: GetOrganizationCallback
	    });
	    function GetOrganizationCallback(data) {
	        if (data == null) {
<<<<<<< HEAD
	            $('body').sales_setuporganization();
	        }
	        else {
	            $('body').sales_home();
	            //$('#section').sales_items_create();
=======
	            $('body').empty();
	            $('body').sales_customers();
//	            $('body').sales_setuporganization();
	        }
	        else {
	            $('body').sales_home();
	            $('#section').sales_customers();
>>>>>>> origin/master
	        }
	    }
	    function GetUserCallback(data) {
	        new Sales.Models.Companyprofile({
	            id: '1',
	            name: data
	        }).save();
	    }
	})