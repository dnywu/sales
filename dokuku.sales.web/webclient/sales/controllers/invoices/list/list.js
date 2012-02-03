steal( './listinvoice.css',
       './DeleteConfirmBox.css',
       'sales/styles/jquery-ui-1.8.14.custom.css',
       'jquery',
       'jquery/controller',
	   'jquery/view/ejs',
	   'jquery/controller/view',
	   'sales/models',
       'sales/scripts/stringformat.js',
'sales/controllers/invoices/create',
/*'sales/controllers/invoices/edit',*/
       'sales/controllers/invoices/InvoiceClass.js',
       'sales/controllers/invoices/invoicedetail',
       'sales/repository/InvoiceRepository.js',
       'sales/repository/InvoicePaymentRepository.js',
       'sales/scripts/jquery-ui-1.8.11.min.js')
.then('./views/listinvoice.ejs',
       './views/invoices.ejs',
       './views/confirmDeleteInvoice.ejs',
       function ($) {

           $.Controller('Sales.Controllers.Invoices.List',
            {
                /* defaults: ($this = null,
                inv = null,
                invRepo = null,
                jumlahdata = 0,
                start = 1,
                page = 1,
                totalPage = 1) */
                defaults: (jumlahdata = 0, start = 1, page = 1, totalPage = 1, $this = null, inv = null, invRepo = null, invId = 0, Pay = null)
            },
            {
                init: function () {
                    $this = this;
                    inv = new Invoiceclass();
                    invRepo = new InvoiceRepository();
                    this.load();
                },
                load: function () {
                    $this = this;
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs'));
                    var invoices = invRepo.GetAllInvoice();
                    var LimitInvoices = this.LimitGetInvoice(invoices);
                },
                LimitGetInvoice: function (data) {
                    jumlahdata = data;
                    limit = $('#limitDataInvoice').val();
                    var startPageInvoice = (start - 1) * limit;
                    var invoice = inv.GetDataInvoice(startPageInvoice, limit);
                    this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoice));
                    $this.initPagination();
                    $('#idInputPageInvoice').val(1);
                    $this.CheckButtonPaging();
                },

                initPagination: function () {
                    totalPage = Math.ceil(jumlahdata / limit);
                    $('#totalPageInvoice').text(totalPage);
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
                '#SearchInvoice keypress': function (el, ev) {
                    if (ev.keyCode == "13") {
                        var invoices = inv.SearchInvoice();
                        this.element.html(this.view('//sales/controllers/invoices/list/views/listinvoice.ejs', invoices));
                    }
                },
                '#SearchInvoice focus': function () {
                    $(".DivSearch").attr("style", "background:#FFFFFF; border-color:#3BB9FF");
                    $("#SearchInvoice").attr("style", "outline:none; background:#FFFFFF");
                },
                '#SearchInvoice blur': function () {
                    $(".DivSearch").attr("style", "background:#F3F3F3");
                    $("#SearchInvoice").attr("style", "background:#F3F3F3");
                },
                '#selectall change': function () {
                    if ($("#selectall").attr('checked')) {
                        $(".selectInvoice").attr('checked', 'checked');
                    } else {
                        $(".selectInvoice").removeAttr('checked');
                    }
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
                    var invoiceId = id;
                    var invoice = invRepo.GetInvoiceById(invoiceId);
                    inv.HideList(invoice.Status, index);
                },
                '#newinvoices click': function () {
                    $("#body").sales_invoices_create("load");
                },
                '.EditContextMenuInvoive click': function (el) {
                    var id = el.attr('id');
                    $('#body').sales_invoices_edit('load', id);
                },
                '.ApproveContextMenuInvoive click': function (el) {
                    var id = el.attr('id');
                    result = inv.ApproveInvoiceByID(id);
                    if (result.error == false) {
                        $this.load();
                    } else {
                        $("#errorListInv").text(result.message).show("slow");
                    }
                },
                '.RecordPaymentContextMenuInvoive click': function (el) {
                    invId = el.attr('id');
                    if (invId != 0) {

                        this.setInvoiceId(invId)
                        $("#body").sales_payment();
                    }
                    var message = $("<div class='deleteConfirmMessage'>Faktur ini akan dirubah dari draft ke open...?</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm YesPayment'>Ya</div>" +
                                    "<div class='ButtonConfirm No' id='Close'>Tidak</div><input type='hidden' value=" + invId + " id='inv-id'/></div>");
                    $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
                    $(".BodyConfirmMassage").append(message);


                },
                setInvoiceId: function (id) {
                    this.invId = id;
                },
                getInvoiceId: function () {
                    return this.invId;
                },
                '.invNo click': function (el, ev) {
                    var invoiceId = $("#invoiceId_" + el.attr("id")).val();
                    var invoice = invRepo.GetInvoiceById(invoiceId);

                    if (invoice != null) {
                        $.each(invoice.Items, function (i) {
                            invoice.Items[i].Rate = String.format("{0:C}", invoice.Items[i].Rate); //String.format("{0:C}", parseFloat(invoice.Items[i].Rate)); // invoice.Items[i].Rate;
                            invoice.Items[i].Amount = String.format("{0:C}", invoice.Items[i].Amount); //String.format("{0:C}", parseFloat(invoice.Items[i].Amount)); // invoice.Items[i].Rate;
                            invoice.Items[i].TaxAmount = String.format("{0:C}", invoice.Items[i].TaxAmount); //String.format("{0:C}", parseFloat(invoice.Items[i].TaxAmount));

                            //$.each(invoice.Items[i].Tax, function (n) {
                                //invoice.Items[i].Tax.Code = invoice.Items[i].Tax.Code;
                                //invoice.Items[i].Tax.Value = invoice.Items[i].Tax.Value;
                                invoice.Items[i].Tax.Amount = String.format("{0:C}", invoice.Items[i].Tax.Amount); //String.format("{0:C}", parseFloat(invoice.Items[i].Tax.Amount)); //invoice.Items[i].Tax.Amount;
                            //});
                        });

                        $.each(invoice.TaxSummary, function (i) {
                            //invoice.TaxSummary[i].Code = invoice.TaxSummary[i].Code; // invoice.Items[i].Rate;
                            invoice.TaxSummary[i].Amount = String.format("{0:C}", parseFloat(this.Amount)); //String.format("{0:C}", parseFloat(invoice.TaxSummary[0].Amount)); //invoice.TaxSummary[i].Amount); // invoice.Items[i].Rate;
                        });

                        invoice.SubTotal = String.format("{0:C}", parseFloat(invoice.SubTotal));
                        invoice.Total = String.format("{0:C}", parseFloat(invoice.Total));
                        //Total All Tax Amount
                        $("#body").sales_invoices_invoicedetail('load', invoice);
                    }
                },
                '#deleteinvoice click': function () {
                    var checkList = $this.IsCheckListNull();
                    if (checkList != 0) {
                        var message = $("<div class='deleteConfirmMessage'>Apakah anda yakin akan menghapus pelanggan ini</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm Yes'>Ya</div>" +
                                    "<div class='ButtonConfirm No' id='Close'>Tidak</div></div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
                },

                ".YesPayment click": function () {
                    var id = $('#inv-id').val();
                    if (id != " ") {

                        $('#body').sales_payment('init', id);
                        $('#vAmountReceived').focus();
                    }
                },
                "#confirmPaymentNo click": function () {
                    $('.ModalDialog').remove();
                },
                '.Yes click': function () {
                    var result;
                    $(".selectInvoice:checked").each(function (index) {
                        var index = $(this).attr("id");
                        var no = $("#invoiceId_" + index).val();
                        result = inv.DeleteInvoice(no);

                        if (result.error == true) {
                            $(".BodyConfirmMassage").empty();
                            var message = $("<div class='deleteConfirmMessage'>" + result.message + "</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm Close' id='Close'>Tutup Pesan</div></div>");
                            $(".BodyConfirmMassage").append(message);
                            return false;
                        }
                    });

                    if (result == "OK") {
                        $(".DeleteConfirmation").remove();
                        $this.load();
                    }
                },
                '#Close click': function () {
                    $(".DeleteConfirmation").remove();
                },
                IsCheckListNull: function () {
                    var countChecked = 0;
                    $(".selectInvoice:checked").each(function (index) {
                        countChecked++;
                    });
                    if (countChecked == 0) {
                        return 0;
                    }
                    return countChecked;
                },
                '#approveinvoice click': function () {
                    var checkList = $this.IsCheckListNull();
                    if (checkList != 0) {
                        var message = $("<div>Apakah anda yakin akan menerbitkan faktur ini</div>" +
                                    "<div class='ButtonApproveYes'>Ya</div>" +
                                    "<div class='ButtonConfirmClose'>Tidak</div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
                },
                '.ButtonApproveYes click': function () {
                    var result;
                    $(".selectInvoice:checked").each(function (index) {
                        var index = $(this).attr("id");
                        var no = $("#invoiceId_" + index).val();
                        result = inv.ApproveInvoiceByID(no);
                        if (result.error == true) {
                            $(".BodyConfirmMassage").empty();
                            var message = $("<div>" + result.message + "</div>" +
                                    "<div class='ButtonConfirmClose'>Tutup Pesan</div>");
                            $(".BodyConfirmMassage").append(message);
                            return false;
                        }
                    });

                    if (result.error == false) {
                        $(".DeleteConfirmation").remove();
                        $this.load();
                    }
                },
                '.CancelContextMenuInvoive click': function (el) {
                    var id = el.attr('id');
                    $(".BodyConfirmMassage").remove();

                    var message = $("<div>Apakah anda yakin akan membatalkan faktur ini</div>" +
                                    "<div><input type='hidden' id='invoID' value='" + id + "'></div>" +
                                    "<div>Note: <textarea name='NoteCancel' id='NoteCancel' class='NoteCancelTxtArea'></textarea></div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm CancelOneYes'>Ya</div>" +
                                    "<div class='ButtonConfirm CancelNo' id='Close'>Tidak</div></div>");
                    $("#body").append(this.view("//sales/controllers/invoices/list/views/ConfirmWithNote.ejs"));
                    $(".BodyConfirmMassage").append(message);
                },
                '.CancelOneYes click': function () {
                    var result;
                    var Note = $("#NoteCancel").val().trim();
                    var no = $("#invoID").val();

                    if (Note.length < 1) {
                        $("#errorCancelInv").text("Catatan Batal harus diisi").show();
                        return false;
                    }

                    result = inv.CancelInvoiceByID(no, Note);

                    if (result.error == true) {
                        $(".BodyConfirmMassage").empty();
                        var message = $("<div class='deleteConfirmMessage'>" + result.message + "</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm Close' id='Close'>Tutup Pesan</div></div>");
                        $(".BodyConfirmMassage").append(message);
                        return false;
                    } else {
                        $(".DeleteConfirmation").remove();
                        $this.load();
                    }
                },
                '#cancelinvoice click': function () {
                    var checkList = $this.IsCheckListNull();
                    $(".BodyConfirmMassage").remove();
                    if (checkList != 0) {
                        var message = $("<div>Apakah anda yakin akan membatalkan faktur ini</div>" +
                                    "<div><br><br>Catatan: <textarea name='NoteCancel' id='NoteCancel' class='NoteCancelTxtArea'></textarea></div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm CancelYes'>Ya</div>" +
                                    "<div class='ButtonConfirm CancelNo' id='Close'>Tidak</div></div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/ConfirmWithNote.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
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
                '#forceCancelinvoice click': function () {
                    var checkList = $this.IsCheckListNull();
                    $(".BodyConfirmMassage").remove();
                    if (checkList != 0) {
                        var message = $("<div>Apakah anda yakin akan membatalkan paksa faktur ini</div>" +
                                    "<div><br><br>Catatan: <textarea name='NoteCancel' id='NoteCancel' class='NoteCancelTxtArea'></textarea></div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm forceCancelYes'>Ya</div>" +
                                    "<div class='ButtonConfirm forceCancelNo' id='Close'>Tidak</div></div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/ConfirmWithNote.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
                },
                '.forceCancelYes click': function () {
                    var result;
                    var Note = $("#NoteCancel").val().trim();

                    if (Note.length < 1) {
                        $("#errorCancelInv").text("Catatan Batal harus diisi").show();
                        return false;
                    }

                    $(".selectInvoice:checked").each(function (index) {
                        var index = $(this).attr("id");
                        var no = $("#invoiceId_" + index).val();
                        result = inv.ForceCancelInvoiceByID(no);

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
                '.ForceCancelContextMenuInvoive click': function (el) {
                    var id = el.attr('id');
                    $(".BodyConfirmMassage").remove();

                    var message = $("<div>Apakah anda yakin akan membatalkan faktur ini</div>" +
                                    "<div><input type='hidden' id='invoID' value='" + id + "'></div>" +
                                    "<div>Note: <textarea name='NoteCancel' id='NoteCancel' class='NoteCancelTxtArea'></textarea></div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm ForceCancelOneYes'>Ya</div>" +
                                    "<div class='ButtonConfirm CancelNo' id='Close'>Tidak</div></div>");
                    $("#body").append(this.view("//sales/controllers/invoices/list/views/ConfirmWithNote.ejs"));
                    $(".BodyConfirmMassage").append(message);
                },
                '.ForceCancelOneYes click': function () {
                    var result;
                    var Note = $("#NoteCancel").val().trim();
                    var no = $("#invoID").val();

                    if (Note.length < 1) {
                        $("#errorCancelInv").text("Catatan Batal harus diisi").show();
                        return false;
                    }

                    result = inv.ForceCancelInvoiceByID(no, Note);

                    if (result.error == true) {
                        $(".BodyConfirmMassage").empty();
                        var message = $("<div class='deleteConfirmMessage'>" + result.message + "</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm Close' id='Close'>Tutup Pesan</div></div>");
                        $(".BodyConfirmMassage").append(message);
                        return false;
                    } else {
                        $(".DeleteConfirmation").remove();
                        $this.load();
                    }
                }
    });
});
