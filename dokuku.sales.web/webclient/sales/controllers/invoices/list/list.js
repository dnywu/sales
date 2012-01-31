steal('jquery/controller',
	   'jquery/view/ejs',
	   'jquery/controller/view',
	   'sales/models',
       'sales/scripts/stringformat.js',
       'sales/controllers/invoices/create',
       'sales/controllers/invoices/edit',
       'sales/controllers/invoices/invoicedetail',
       'sales/repository/InvoiceRepository.js',
       'sales/repository/PaymentRepository.js',
       './listinvoice.css',
       './DeleteConfirmBox.css')
.then('./views/listinvoice.ejs',
       './views/invoices.ejs',
       './views/confirmDeleteInvoice.ejs', './views/ConfirmWithNote.ejs', 'sales/controllers/payment/views/recordpayment.ejs',
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
                defaults: (jumlahdata = 0, start = 1, page = 1, totalPage = 1, $this = null, inv = null, invRepo = null)
            },
            {
                init: function () {
                    $this = this;
                    inv = new Invoice();
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

                    this.HideList(invoice.Status, index);
                },
                HideList: function (Status, index) {
                    if (Status == "Draft") {
                        this.HideActionList("fffft", index);
                    } else if (Status == "Belum Bayar") {
                        this.HideActionList("ftfft", index);
                    } else if (Status == "Belum Lunas") {
                        this.HideActionList("ttftt", index);
                    } else if (Status == "Sudah Lunas") {
                        this.HideActionList("tttft", index);
                    } else if (Status == "Batal") {
                        this.HideActionList("ttttt", index);
                    }
                },
                HideActionList: function (srcPattern, index) {
                    var str = srcPattern;

                    if (str.substring(0, 1) == "t") {
                        this.Menu("div#actionEdit", index);
                    }

                    if (str.substring(1, 2) == "t") {
                        this.Menu("div#actionApprove", index);
                    }

                    if (str.substring(3, 4) == "t") {
                        this.Menu("div#actionCancel", index);
                    }

                    if (str.substring(4, 5) == "t") {
                        this.Menu("div#actionForceCancel", index);
                    }
                },
                Menu: function (Name, index) {
                    var result;
                    var Menu = "tr#trbodyDataInvoice" + index + " td#tdDataInvoice" + index + " div.ContextMenuInvoice";

                    result = $(Menu + " " + Name).remove();
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
                        //sales_payment('load');
                        this.load();
                    } else {
                        $("#errorListInv").text(result.message).show("slow");
                    }
                },
                '.RecordPaymentContextMenuInvoive click': function (el) {
                    var Pay = new PaymentRepository();
                    var id = el.attr('id');
                    var invoice = invRepo.GetInvoiceById(id);
                    result = Pay.PaymentByIdInvoice(id);
                    //                    if (result.error == false) {
                    $('#body').sales_payment('load', invoice);
                    //                    } else {
                    //                        $("#errorListInv").text(result.message).show("slow");
                    //                    }
                },
                '.invNo click': function (el, ev) {
                    var invoiceId = $("#invoiceId_" + el.attr("id")).val();
                    var invoice = invRepo.GetInvoiceById(invoiceId);
                    if (invoice != null)
                        $("#body").sales_invoices_invoicedetail('load', invoice);

                },
                '#deleteinvoice click': function () {
                    var checkList = $this.IsCheckListNull();
                    $(".BodyConfirmMassage").remove();
                    if (checkList != 0) {
                        var message = $("<div class='deleteConfirmMessage'>Apakah anda yakin akan menghapus faktur ini</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm Yes'>Ya</div>" +
                                    "<div class='ButtonConfirm No' id='Close'>Tidak</div></div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
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
                    $(".BodyConfirmMassage").remove();
                    if (checkList != 0) {
                        var message = $("<div>Apakah anda yakin akan menyetujui faktur ini</div>" +
                                    "<div class='buttonDIV'><div class='ButtonConfirm ApproveYes'>Ya</div>" +
                                    "<div class='ButtonConfirm ApproveNo' id='Close'>Tidak</div></div>");
                        $("#body").append(this.view("//sales/controllers/invoices/list/views/confirmDeleteInvoice.ejs"));
                        $(".BodyConfirmMassage").append(message);
                    }
                },
                '.ApproveYes click': function () {
                    var result;
                    $(".selectInvoice:checked").each(function (index) {
                        var index = $(this).attr("id");
                        var no = $("#invoiceId_" + index).val();
                        result = inv.ApproveInvoiceByID(no);

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
                }


            });
       });