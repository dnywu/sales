steal( 'jquery/controller','jquery/view/ejs',
        'jquery/controller/view','./Home.css'
     )
	.then( './views/home.ejs', function($){
    $.Controller('sales.controllers.home',
    {
	    init : function(){
	        this.element.html(this.view("//sales/controllers/home/views/home.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
	    }
    })
});