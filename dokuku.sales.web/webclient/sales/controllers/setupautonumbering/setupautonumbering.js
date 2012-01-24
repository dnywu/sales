steal('jquery/controller', 
      'jquery/view/ejs',
      './autonumbering.css')
	.then('./views/setupautonumbering.ejs', function ($) {
    $.Controller('Sales.Controllers.Setupautonumbering',
    {
    defaults: {}
    },

    {
        init: function () {
            this.element.html(this.view('//sales/controllers/setupautonumbering/views/setupautonumbering.ejs'));
        },

        load: function () {
            this.element.html(this.view('//sales/controllers/setupautonumbering/views/setupautonumbering.ejs'));
        }
    })

});