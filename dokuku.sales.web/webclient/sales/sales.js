steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
    'jquery',
    'sales/scripts/jquery-ui-1.8.11.min.js',
    'sales/styles/jquery-ui-1.8.14.custom.css',
    'sales/controllers/nav',
    'sales/controllers/setuporganization',
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
	            $('body').sales_setuporganization();
	        }
	        else {
	            $('body').sales_nav();
	        }
	    }
	    function GetUserCallback(data) {
	        new Sales.Models.Companyprofile({
	            id: 1,
	            name: data
	        }).save();
	    }
	})
