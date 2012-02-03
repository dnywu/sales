steal(
	'./sales.css',
	'./models/models.js',
	'./fixtures/fixtures.js',
    'jquery',
    'sales/controllers/nav',
    'sales/controllers/nav/navtab.js',
    'sales/controllers/nav/navsubtab.js',
    'sales/controllers/setuporganization',
    'sales/controllers/restrictuser',
	function () {

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
	            new Sales.Models.Currency({
	                id: '1',
	                curr: data.Currency
	            }).save();
	            $('#LoadingElment').remove();
	            $('#header').sales_nav();
	            $('#tabs .containertabs').sales_navtab();
	            $('#subtabs .container').sales_navsubtab();
	            $('#subtabs .container').sales_navsubtab("empty");
	        }
	    }
	    function GetUserCallback(data) {
	        new Sales.Models.Companyprofile({
	            id: '1',
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