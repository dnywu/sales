// load('invoice/scripts/crawl.js')

load('steal/rhino/rhino.js')

steal('steal/html/crawl', function(){
  steal.html.crawl("invoice/invoice.html","invoice/out")
});
