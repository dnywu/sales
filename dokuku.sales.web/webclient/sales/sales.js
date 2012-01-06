steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
    'sales/controllers/items/create',
    'sales/controllers/home',
    './style/ModalDialog.css',
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
	            $('body').empty();
	            $('body').sales_setuporganization();
	        }
	        else {
	            $('body').sales_home();
	            $('#section').sales_items_create();
	        }
	    }
	    function GetUserCallback(data) {
	        new Sales.Models.Companyprofile({
	            id: '1',
	            name: data
	        }).save();
	    }
	})