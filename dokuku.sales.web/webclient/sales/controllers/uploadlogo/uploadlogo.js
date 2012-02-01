steal('jquery/controller', 'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/uploadlogo/uploadlogo.css'
     )
	.then('./views/init.ejs', function ($) {
	    $.Controller('sales.Controllers.uploadlogo',
        {
            init: function () {
                this.load();
            },
            load: function () {
                this.element.html(this.view("//sales/controllers/uploadlogo/views/init.ejs"));
            }
        })
	});