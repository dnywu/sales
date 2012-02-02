steal('jquery/controller', 'jquery/view/ejs')
	.then('./views/init.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Paymentreceived
	    */
	    $.Controller('Sales.Controllers.Paymentreceived',
	    /** @Static */
{
defaults: {}
},
	    /** @Prototype */
{
init: function () {
    this.element.html(this.view("//sales/controllers/paymentreceived/views/paymentreceivedlist.ejs"));
},
load: function () {

    this.element.html(this.view("//sales/controllers/paymentreceived/views/paymentreceivedlist.ejs"));

},
'.menuLeft click': function (el) {
    var index = el.attr('tabindex');
    var id = $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice span.spanContextMenuListInvoice").attr("id");
    $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").show();
    this.LoadActionList(id, index);
}

})

	});