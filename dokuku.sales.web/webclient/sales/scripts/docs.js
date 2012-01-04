//js sales/scripts/doc.js

load('steal/rhino/rhino.js');
steal("documentjs").then(function(){
	DocumentJS('sales/sales.html', {
		markdown : ['sales']
	});
});