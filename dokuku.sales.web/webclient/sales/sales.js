steal(
	'./sales.css', 			// application CSS file
	'./models/models.js', 	// steals all your models
	'./fixtures/fixtures.js', // sets up fixtures for your models
    'jquery',
    'sales/controllers/nav',
    'sales/controllers/setuporganization',
    'sales/controllers/restrictuser',
	function () {					// configure your application

	    $.ajax({
	        type: 'GET',
	        url: '/getuser',
	        dataType: 'json',
            async: false,
	        success: GetUserCallback
	    });
	    $.ajax({
	        type: 'GET',
	        url: '/getorganization',
	        dataType: 'json',
            async: false,
	        success: GetOrganizationCallback
	    });
	    function GetOrganizationCallback(data) {
	        if (data == null) {
	            $.ajax({
	                type: 'GET',
	                url: '/validatesetuporganization',
	                dataType: 'json',
	                async: false,
	                success: ValidateSetupOrganizationCallback
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
	    function ValidateSetupOrganizationCallback(data) {
	        if (data.IsValid)
	            $(document.body).sales_setuporganization();
	        else
	            $(document.body).sales_restrictuser();
	    }
	    $("#LoadingElment").remove();
	})

