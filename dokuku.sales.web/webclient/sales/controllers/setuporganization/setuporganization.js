steal('jquery/controller', 'jquery/view/ejs',
		'jquery/controller/view',
        'sales/models',
        'sales/scripts/GetParameter.js',
		'sales/controllers/setuporganization/setuporganization.css'
	)
	.then('./views/setuporganization.ejs', function ($) {
	    $.Controller('sales.Controllers.setuporganization',
	{
	    init: function () {
	        this.element.html(this.view("//sales/controllers/setuporganization/views/setuporganization.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
	    },
	    load: function () {
	        if (getParameterByName('error') == 'true') {
	            alert(getParameterByName('message'));
	            $('<li>', { 'class': 'errserver', text: getParameterByName('message') }).appendTo($("#error").show());
	        }
        },
	    '#name focus': function () {
	        $('.hint').css('display', 'inline');
	    },
	    '#name keypress': function () {
	        $('li.name').remove();
	        if ($("#error").is(':empty'))
	            $("#error").hide();
	    },
	    '#curr change': function () {
	        $('li.curr').remove();
	        if ($("#error").is(':empty'))
	            $("#error").hide();
	    },
	    '#name blur': function () {
	        $('.hint').css('display', 'none');
	    },
	    submit: function (el, ev) {
	        var form = $("#setuporganization");
	        var err = $("#error");
	        var defaults = {
	            name: $("#name").val(),
	            timezone: $("#timezone").val(),
	            curr: $("#curr").val(),
	            starts: $("#starts").val()
	        };
	        err.empty();
	        if (defaults.name !== "" && defaults.curr != 0)
	            form.submit();
	        if (defaults.name == "")
	            $('<li>', { 'class': 'name', text: "Nama Organisasi harus di isi" }).appendTo(err.show());
	        if (defaults.curr == 0)
	            $('<li>', { 'class': 'curr', text: "Mata Uang harus di pilih" }).appendTo(err.show());
	        ev.preventDefault();
	        return;
	    }
	})
	});