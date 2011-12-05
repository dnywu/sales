steal('jquery/controller', 'jquery/view/ejs',
      'jquery/controller/view',
      'sales/controllers/invoices/list/DeleteConfirmBox.css',
      'sales/controllers/invoices/invoicedetail/invoicedetail.css')
	.then('./views/invoicedetail.ejs', 'sales/controllers/invoices/list/views/ConfirmWithNote.ejs', function ($) {
	    $.Controller('sales.Controllers.invoices.invoicedetail',
{
    defaults: ($this = null, invo = null, invRepo = null)
},
{
    init: function (el, ev, invoice) {
        this.load(invoice);
        $this = this;
        invo = new Invoice();
        invRepo = new InvoiceRepository();
    },
    load: function (invoice) {
        var inv = this.GetDetailCustomer(invoice);
        invo = new Invoice();
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
                                    "<div>Catatan: <textarea name='NoteCancel' id='NoteCancel' class='NoteCancelTxtArea'></textarea></div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm CancelYes'>Ya</div>" +
                                    "<div class='ButtonConfirm CancelNo' id='Close'>Tidak</div></div>");
            $("#body").append(this.view("//sales/controllers/invoices/list/views/ConfirmWithNote.ejs"));
            $(".BodyConfirmMassage").append(message);
        },
        '.CancelYes click': function () {
            var result;
            var Note = $("#NoteCancel").val().trim();

            if (Note.length < 1) {
                $("#errorCancelInv").text("Catatan Batal harus diisi").show();
                return false;
            }

            $(".selectInvoice:checked").each(function (index) {
                var index = $(this).attr("id");
                var no = $("#invoiceId_" + index).val();
                result = inv.CancelInvoiceByID(no, Note);

                if (result.error == true) {
                    $(".BodyConfirmMassage").empty();
                    var message = $("<div class='deleteConfirmMessage'>" + result.message + "</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm Close' id='Close'>Tutup Pesan</div></div>");
                    $(".BodyConfirmMassage").append(message);
                    return false;
                }
            });

            if (result.error == false) {
                $(".DeleteConfirmation").remove();
                $this.load();
            }
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