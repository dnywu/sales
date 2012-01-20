steal('jquery/controller', 'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/invoices/invoicedetail/invoicedetail.css')
	.then('./views/invoicedetail.ejs', function ($) {
	    $.Controller('sales.Controllers.invoices.invoicedetail',
{
    defaults: {}
},
{
    init: function (el, ev, invoice) {
        this.load(invoice);
    },
    load: function (invoice) {
        var inv = this.GetDetailCustomer(invoice);
        this.element.html(this.view("//sales/controllers/invoices/invoicedetail/views/invoicedetail.ejs", inv));
        this.SetCurrencyToView();
    },
    GetDetailCustomer: function (invoice) {
        $.ajax({
            type: 'GET',
            url: '/GetDataCustomer/id/' + invoice.CustomerId,
            dataType: 'json',
            async: false,
            success: function (data) {
                invoice.InvoiceDate = new Date(parseInt(invoice.InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                invoice.DueDate = new Date(parseInt(invoice.DueDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                invoice.InvoiceDate = $.datepicker.formatDate('dd M yy', invoice.InvoiceDate);
                invoice.DueDate = $.datepicker.formatDate('dd M yy', invoice.DueDate);
                invoice.BillingAddress = data.BillingAddress;
                invoice.City = data.City;
                invoice.Province = data.Province;
                invoice.PostalCode = data.PostalCode;
                invoice.State = data.State;
            }
        });
        return invoice;
    },
    SetCurrencyToView: function () {
        Sales.Models.Currency.findOne({ id: '1' }, function (data) {
            $(".ccy").text(data.curr);
        });
    }
})

	});