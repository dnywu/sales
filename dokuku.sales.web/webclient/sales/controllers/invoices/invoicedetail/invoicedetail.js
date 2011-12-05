steal('jquery/controller', 'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/invoices/invoicedetail/invoicedetail.css')
	.then('./views/invoicedetail.ejs', 'sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs', function ($) {
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
        this.GetStatusInvoice(inv.Status);
    },
    '#newinvoicesByDetails click': function () {
        $("#body").sales_invoices_create("load");
    },
    '#menuItemRightUbah click': function () {
        var id = $(".idIvoDetil").attr("id");
        $('#body').sales_invoices_edit('load', id);
    },
    '#menuItemRightBatal click': function () {
            var message = $("<div>Apakah anda yakin akan membatalkan faktur ini</div>" +
                                    "<div class='ButtonBatalYes'>Ya</div>" +
                                    "<div class='ButtonBatalClose'>Tidak</div>");
            $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
            $(".BodyConfirmMassage").append(message);
    },
    GetStatusInvoice: function (status) {
        var IsStatus = status;
        if (IsStatus != "Draft") {
            $("#menuItemRightSetujui").remove();            
        }
    },
    GetDetailCustomer: function (invoice) {
        $.ajax({
            type: 'GET',
            url: '/GetDataCustomer/' + invoice.CustomerId,
            dataType: 'json',
            async: false,
            success: function (data) {
                invoice.InvoiceDate = new Date(parseInt(invoice.InvoiceDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                invoice.DueDate = new Date(parseInt(invoice.DueDate.replace(/\/Date\((-?\d+)\)\//, '$1')));
                invoice.InvoiceDate = $.datepicker.formatDate('dd M yy', invoice.InvoiceDate);
                invoice.DueDate = $.datepicker.formatDate('dd M yy', invoice.DueDate);
                invoice.Email = data.Email;
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