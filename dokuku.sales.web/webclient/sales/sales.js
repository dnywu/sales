steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
    'sales/controllers/nav',
    'sales/controllers/setuporganization',
    'sales/controllers/restrictuser',
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
	            $.ajax({
	                type: 'GET',
	                url: '/allowsetuporg',
	                dataType: 'json',
	                async: false,
	                success: GetRoleCallback
	            });
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
	    function GetRoleCallback(data) {
	        if (data.isAllowed === true)
	            $(document.body).sales_setuporganization();
	        else
	            $(document.body).sales_restrictuser();
	    }
	})