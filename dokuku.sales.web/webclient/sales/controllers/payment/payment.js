steal('jquery/controller', 'sales/controllers/payment/payment.css',
       'sales/scripts/jquery-ui-1.8.11.min.js',
       'sales/styles/jquery-ui-1.8.14.custom.css', 'jquery/view/ejs', 'sales/repository/PaymentRepository.js')
	.then('./views/init.ejs', './views/recordpayment.ejs', function ($) {

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
init: function () {
    this.element.html(this.view("//sales/controllers/payment/views/recordpayment.ejs"));
    this.SetDatePicker();
    this.SetDefaultDate();
},
load: function () {
    this.element.html(this.view("//sales/controllers/payment/views/recordpayment.ejs"));
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
    Payment.BankChanges = $('#vBankChanges').val();
    Payment.Date= $('#PayDate').val();
    Payment.Reference= $('#vReference').val();
    Payment.Notes=$('#vNotes').val();
    Payment.Email=$('#vEmail').val();
    Payment.PaymentMethod=$('#vPaymentMethod').select().val();
    Payment.Invoice= $('vInvoice').text();
    Payment.Customer=$('vCustomer').text();
    Payment.CreditAvailable=$('vCreditAvailable').text();

    var PaymentRepo = new PaymentRepository();
    var dataRepo = PaymentRepo.PaymentSave(Payment);
    }

})

	});

  