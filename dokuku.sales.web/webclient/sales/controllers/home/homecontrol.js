steal(  'jquery',
        'jquery/controller',
        'jquery/view/ejs',
        'jquery/controller/view',
        'sales/controllers/home/home.css')
	.then('./views/home.ejs', function($){
    $.Controller('Sales.Controllers.Home.Control',
    {
        onDocument: true
    },
    {
	    init : function(){
            this.element.html(this.view('//sales/controllers/home/views/home.ejs'));
	    },
        load: function () {
            this.element.html(this.view('//sales/controllers/home/views/home.ejs'));
        }
    })

});