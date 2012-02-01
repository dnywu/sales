steal('jquery/controller', 'jquery/view/ejs', 'sales/repository/PaymentReceivedRepository.js')
	.then('./views/init.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Paymentreceived
	    */
	    $.Controller('Sales.Controllers.Paymentreceived',
	    /** @Static */
{
defaults: (jumlahdata = 0, start = 1, page = 1, totalPage = 1, $this = null, inv = null, payReceivedRepo = null, invId = 0, Pay = null)
},
	    /** @Prototype */
{
init: function () {
    $this = this;
    payReceivedRepo = new PaymentReceivedRepository();
    this.load();
},
load: function () {
    var PaymentReceivedData = payReceivedRepo.getAllPaymentReceived();
    this.element.html(this.view("//sales/controllers/paymentreceived/views/paymentreceivedlist.ejs"));
    var LimitInvoices = this.LimitGetPaymentReceived(PaymentReceivedData);
},
LimitGetPaymentReceived: function (data) {
    jumlahdata = data;
    limit = $('#limitDataInvoice').val();
    var startPageInvoice = (start - 1) * limit;
    var dataPaymentReceived = payReceivedRepo.getDataPaymentReceivedByLimit(startPageInvoice, limit);
    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', dataPaymentReceived));
    $this.initPagination();
    $('#idInputPageInvoice').val(1);
    $this.CheckButtonPaging();
},
initPagination: function () {
    totalPage = Math.ceil(jumlahdata / limit);
    $('#totalPageInvoice').text(totalPage);
},
ChangePage: function () {
        $this.initPagination();
        var startPage = parseInt($('#idInputPageInvoice').val());
        var startPageInvoice = (startPage - 1) * $('#limitDataInvoice').val();
        var limitInvoice = $('#limitDataInvoice').val();
        var invoice = inv.GetDataInvoice(startPageInvoice, limitInvoice);
        this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoice));
        limit = $('#limitDataInvoice').val();
        var startPage = parseInt($('#idInputPageInvoice').val(startPage));
        $this.initPagination();
        $this.CheckButtonPaging();
 },
   CheckButtonPaging: function () {
        var startPage = parseInt($('#idInputPageInvoice').val());
        if (isNaN(startPage) || startPage <= 1) {
            $('.DivPrevInvoice').hide();
            $('.disablePrevInvoice').show();
        } else {
            $('.DivPrevInvoice').show();
            $('.disablePrevInvoice').hide();
        }
        var totalPage = parseInt($('#totalPageInvoice').text());
        if (totalPage <= 1 || totalPage <= startPage) {
            $('.DivNextInvoice').hide();
            $('.disableNextInvoice').show();
        } else {
            $('.DivNextInvoice').show();
            $('.disableNextInvoice').hide();
        }
    },
'.prevInvoice click': function () {
    $this.initPagination();
    var startPage = parseInt($('#idInputPageInvoice').val());
    if (isNaN(startPage))
        startPage = 1;
    else
        startPage--;
    $('#idInputPageInvoice').val(startPage);
    $this.ChangePage();
},
'.nextInvoice click': function () {
    $this.initPagination();
    var startPage = parseInt($('#idInputPageInvoice').val());
    if (isNaN(startPage))
        startPage = 2;
    else
        startPage++;
    $('#idInputPageInvoice').val(startPage);
    $this.ChangePage();
},
'.lastInvoice click': function () {
    $('#idInputPageInvoice').val(parseInt($('#totalPageInvoice').text()));
    $this.ChangePage();
},
'.firstInvoice click': function () {
    $this.initPagination();
    $('#idInputPageInvoice').val(1);
    $this.ChangePage();
},
'#idInputPageInvoice change': function () {
        $this.ChangePage();
},
'#limitDataInvoice change': function () {
        $this.ChangePage();
},
'table#tblListInvoice tbody.bodyDataInvoice tr.trbodyDataInvoice hover': function (el) {
        var index = el.attr('tabindex');
        $('#settingListInvoice' + index).show();
        $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").hide();
 },
 'table#tblListInvoice tbody.bodyDataInvoice tr.trbodyDataInvoice mouseleave': function (el) {
        var index = el.attr('tabindex');
        $('#settingListInvoice' + index).hide();
        $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").hide();
 },
 '.settingListInvoice click': function (el) {
        var index = el.attr('tabindex');
        var id = $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice span.spanContextMenuListInvoice").attr("id");
        $("tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice").show();
        this.LoadActionList(id, index);
 },
 LoadActionList: function (id, index) {
     var paymentId = id;
        var invoice = payReceivedRepo.getPaymentReceivedById(paymentId);
       // inv.HideList(invoice.Status, index);
 }

})

	});