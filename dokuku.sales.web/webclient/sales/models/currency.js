steal('jquery/model', function(){

/**
 * @class Sales.models.currency
 * @parent index
 * @inherits jQuery.Model
 * Wraps backend currency services.  
 */
$.Model('Sales.Models.Currency',
/* @Static */
{
	findAll: "/currencies.json",
  	findOne : "/currencies/{id}.json", 
  	create : "/currencies.json",
 	update : "/currencies/{id}.json",
  	destroy : "/currencies/{id}.json"
},
/* @Prototype */
{});

})