steal('jquery/model', function(){

/**
 * @class Sales.Models.Invoices
 * @parent index
 * @inherits jQuery.Model
 * Wraps backend invoices services.  
 */
$.Model('Sales.Models.Invoices',
/* @Static */
{
	findAll: "/invoices.json",
  	findOne : "/invoices/{id}.json", 
  	create : "/invoices.json",
 	update : "/invoices/{id}.json",
  	destroy : "/invoices/{id}.json"
},
/* @Prototype */
{});

})