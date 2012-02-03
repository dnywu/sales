steal('jquery',
        'jquery/controller',
        'jquery/view/ejs',
        'jquery/controller/view',
        'sales/models/models.js',
        'sales/fixtures/fixtures.js',
        'sales/controllers/nav/nav.js',
        'sales/controllers/nav/navtab.js',
        'sales/controllers/nav/navsubtab.js',
        'sales/scripts/stringformat.js',
        'sales/controllers/restrictuser',
        'sales/controllers/setuporganization',
        'sales/controllers/setupautonumbering/setupautonumberingControl.js',
        'sales/controllers/setuporganization/setuporganization.js',
        'sales/controllers/setuporganization/settingorganization.js',
        'sales/controllers/currencyandtax/currencyandtax.js',
        'sales/controllers/paymentmode/paymentmode.js',
        'sales/sales.css',
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
	            $('#subtabs .container').sales_navsubtab(); //register controller to this ids
	            $('#subtabs .container').sales_navsubtab("SettingSubMenu"); //load subitem without binding event attached
	            $('#subtabs .container').sales_navsubtab(); //used for rebind event onclick, do not delete if know what u do
	            $("#body").sales_setupautonumberingcontrol('load');
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