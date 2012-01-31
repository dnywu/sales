steal('jquery/controller', 'sales/controllers/payment/payment.css', 'sales/controllers/invoices/list',
       'sales/scripts/jquery-ui-1.8.11.min.js',
       'sales/styles/jquery-ui-1.8.14.custom.css', 'jquery/view/ejs', 'sales/repository/PaymentRepository.js', 'sales/repository/InvoiceRepository.js')
	.then('./views/init.ejs', './views/recordpayment.ejs', 'sales/controllers/invoices/list/views/listinvoice.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Payment
	    */
	    $.Controller('Sales.Controllers.Payment',
	    /** @Static */
{
defaults: {}
},
	    /** @Prototype */
{
init: function (el, ev, invoice) {
    this.load(invoice);
},
load: function (invoice) {
    var inv = invoice;
    var payRepo = new PaymentRepository();
    var paymentMode = payRepo.GetAllPaymentMode();
    this.element.html(this.view("//sales/controllers/payment/views/recordpayment.ejs", inv));
    this.SetDatePicker();
    this.SetDefaultDate();
},
SetDatePicker: function () {
    var dates = $("#PayDate").datepicker({ dateFormat: 'dd M yy',
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 1,
        onSelect: function (selectedDate) {
            var option = this.id == "PayDate" ? "" : "",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
            if (this.id == "PayDate") {
                var currdate = new Date(date);
                currdate.setDate(currdate.getDate());
                dates.not(this).val($.datepicker.formatDate('dd M yy', currdate));
            }
        }
    });
},
SetDefaultDate: function () {
    var currdate = new Date();
    var dueDate = currdate;
    $("#PayDate").val($.datepicker.formatDate('dd M yy', currdate));
},
'#PayButton click': function () {
    Payment = new Object();
    Payment.AmountReceived = $('#vAmountReceived').val();
    if ($('#valueBankCharges').val() == "") {
        Payment.BankChanges = 0;
    }
    else {
        Payment.BankChanges = $('#valueBankCharges').val();
    }
    Payment.Date = $('#PayDate').val();
    Payment.Reference = $('#vReference').val();
    Payment.Notes = $('#valueNotes').val();
    Payment.OwnerId = $('vEmail').val();
    Payment.PaymentMethod = $('#vPaymentMethod').val();
    Payment.Invoice = $('vInvoice').text().trim();
    Payment.Customer = $('vCustomer').text();
    Payment.CreditAvailable = $('vCreditAvailable').text();
    Payment.Currency = $('Currency').text();
    Payment.Status = $('#Status').val();
    Payment.CustomerId = $('#CustomerId').val();
    Payment.InvoiceId = $('#InvoiceId').val();
    var PaymentRepo = new PaymentRepository();
    // var  invRepo = new InvoiceRepository();  
    var dataRepo = PaymentRepo.SendRecordPayment(Payment);
    // var invoice = invRepo.GetInvoiceById(invId);    
    $('#body').sales_invoices_list('load')
}

})

	});

  