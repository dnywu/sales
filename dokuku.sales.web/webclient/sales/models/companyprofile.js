steal('jquery/model', function(){

/**
 * @class Sales.Models.Companyprofile
 * @parent index
 * @inherits jQuery.Model
 * Wraps backend companyprofile services.  
 */
$.Model('Sales.Models.Companyprofile',
/* @Static */
{
	findAll: "/companyprofiles.json",
  	findOne : "/companyprofiles/{id}.json", 
  	create : "/companyprofiles.json",
 	update : "/companyprofiles/{id}.json",
  	destroy : "/companyprofiles/{id}.json"
},
/* @Prototype */
{});

})