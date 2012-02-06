//steal/js invoice/scripts/compress.js

load("steal/rhino/rhino.js");
steal('steal/build').then('steal/build/scripts','steal/build/styles',function(){
	steal.build('invoice/scripts/build.html',{to: 'invoice'});
});
