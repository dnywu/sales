//js invoice/scripts/doc.js

load('steal/rhino/rhino.js');
steal("documentjs").then(function(){
	DocumentJS('invoice/invoice.html', {
		markdown : ['invoice']
	});
});