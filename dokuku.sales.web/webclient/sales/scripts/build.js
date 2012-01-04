//steal/js sales/scripts/compress.js

load("steal/rhino/rhino.js");
steal('steal/build').then('steal/build/scripts','steal/build/styles',function(){
	steal.build('sales/scripts/build.html',{to: 'sales'});
});
