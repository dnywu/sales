steal('jquery/controller', 'jquery/view/ejs', 'sales/repository/paymentTermsRepository.js', './paymentterms.css')
	.then('./views/init.ejs', './views/listpaymentterms.ejs', './views/editpaymentterms.ejs', './views/addpaymentterms.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.PaymentTerms
	    */
	    $.Controller('Sales.Controllers.Paymentterms',
	    /** @Static */
{
defaults: (PaymentTermsRepo = null)
},
	    /** @Prototype */
{
init: function () {
    PaymentTermsRepo = new PaymentTermsRepository();
    this.element.html(this.view("//sales/controllers/PaymentTerms/views/listpaymentterms"));
    this.requestAllPaymentTermsSuccess(PaymentTermsRepo.getAllPaymentTerms());
},
load: function () {
    PaymentTermsRepo = new PaymentTermsRepository();
    this.element.html(this.view("//sales/controllers/PaymentTerms/views/listpaymentterms"));
    this.requestAllPaymentTermsSuccess(PaymentTermsRepo.getAllPaymentTerms());
},
create: function () {
    this.element.html(this.view("//sales/controllers/PaymentTerms/views/addpaymentterms"));
},
'#btn-add-PaymentTerms click': function (el, ev) {
    ev.preventDefault();
    $("#body").sales_paymentterms('create');
    $('#kode').focus();
},
'#PaymentTermsSave click': function () {
    var kode = $("#kode").val();
    var nama = $("#nama").val();
    var jangkawaktu = $("#jangka-waktu").val();
    var PaymentTerms = new Object();
    PaymentTerms.Name = nama;
    PaymentTerms.Code = kode;
    PaymentTerms.Term = jangkawaktu;
    if (PaymentTermsRepo.createPaymentTerms(PaymentTerms)) {
        $("#body").sales_paymentterms("load");
    }
},
viewEditPaymentTerms: function (item) {
    this.element.html("//sales/controllers/PaymentTerms/views/editpaymentterms.ejs", item);
    this.viewSelected(item);
},
viewSelected: function (item) {
    var value = $('#nama option:selected').val();
    if (value != item.Name) {
        $('#nama option:selected').val(item.Name);
    }
},
"#CancelPaymentTerms click": function () {
    $("#body").sales_paymentterms("load");
},
"#checkboxAllPaymentTerms change": function () {
    if ($("#checkboxAllPaymentTerms").attr('checked')) {
        $(".checkboxPaymentTerms").attr('checked', 'checked');
    } else {
        $(".checkboxPaymentTerms").removeAttr('checked');
    }
},
'#deletePaymentTerms click': function () {
    $('.ConfirmationPaymentTerms').show();
},
'.ButtonConfirmationPaymentTermsYa click': function () {
    $('.checkboxPaymentTerms:checked').each(function (index) {
        var id = $(this).val();
        PaymentTermsRepo = new PaymentTermsRepository();
        PaymentTermsRepo.deletePaymentTerms(id);
    });
    $('.checkboxPaymentTerms:checked').parent().parent().remove();
    $('.ConfirmationPaymentTerms').hide();
},
'#nama change': function (el, ev) {
    if ($('#nama').val() == "default") {
        $('#jangka-waktu').attr('disabled', 'disabled');
        $('#label-jk-waktu').css('color', 'gray');
        $('#ket').empty();
    }
    else {
        $("#ket").text($("#nama").val());
        $('#jangka-waktu').removeAttr('disabled', 'disabled');
        $('#label-jk-waktu').css('color', '#9C0000');
    }

},
'.ButtonConfirmationPaymentTermsTidak click': function () {
    $('.ConfirmationPaymentTerms').hide();
},
'table.dataPaymentTerms tbody.BodyDataPaymentTerms tr.trDataPaymentTerms hover': function (el) {
    var index = el.attr('tabindexPaymentTerms');
    $('#settingListPaymentTerms' + index).show();
    $("tr#trPaymentTermsList" + index + " td#tdDataPaymentTerms" + index + " div.ContextMenuPaymentTerms").hide();
},
'table.dataPaymentTerms tbody.BodyDataPaymentTerms tr.trDataPaymentTerms mouseleave': function (el) {
    var index = el.attr('tabindexPaymentTerms');
    $('#settingListPaymentTerms' + index).hide();
    $("tr#trPaymentTermsList" + index + " td#tdDataPaymentTerms" + index + " div.ContextMenuPaymentTerms").hide();
},
'.settingPaymentTerms click': function (el) {
    var index = el.attr('tabindexPaymentTerms');
    $("tr#trPaymentTermsList" + index + " td#tdDataPaymentTerms" + index + " div.ContextMenuPaymentTerms").show();
},
'.EditContextMenuPaymentTerms click': function (el) {
    var id = el.attr('id');
    var PaymentTerms = PaymentTermsRepo.getPaymentTermsById(id);
    $('#body').sales_paymentterms('viewEditPaymentTerms', PaymentTerms);
    $('#kode').focus();
},
'#UpdatePaymentTerms click': function (el, ev) {
    ev.preventDefault();
    var id = $("#_id").val();
    var nama = $("#nama").val();
    var kode = $("#kode").val();
    var jangkawaktu = $("#jangka-waktu").val();

    var PaymentTerms = new Object();
    PaymentTerms._id = id;
    PaymentTerms.Name = nama;
    PaymentTerms.Code = kode;
    PaymentTerms.Term = jangkawaktu;
    var PaymentTerms = PaymentTermsRepo.updatePaymentTerms(PaymentTerms);
    $("#body").sales_paymentterms('load')
},

requestAllPaymentTermsSuccess: function (data) {
    $("table.dataPaymentTerms tbody").empty();
    $.each(data, function (item) {
        $("table.dataPaymentTerms tbody.BodyDataPaymentTerms").append(
            "<tr class='trDataPaymentTerms' id='trPaymentTermsList" + item + "' tabindexPaymentTerms='" + item + "'>" +
            "<td class='thDataPaymentTerms tdDataPaymentTerms tdDataPaymentTermsCenter textAlignRight' style='text-align:center'><input type='checkbox' class='checkboxPaymentTerms' id='checkboxPaymentTerms'value='" + data[item]._id + "'/></td>" +
            "<td class='tdDataPaymentTermsCenter tdDataPaymentTerms' id='tdDataPaymentTerms" + item + "'><div class='settingPaymentTerms' id='settingListPaymentTerms" + item + "' tabindexPaymentTerms='" + item + "'><img class='' src='/sales/controllers/PaymentTerms/images/setting.png'/></div></td>" +
            "<td class='tdDataPaymentTermsLeft tdDataPaymentTerms'>" +
                "<div width = '100%'>" + data[item].Code + "</div>" +
            "</td>" +
             "<td class='tdDataPaymentTermsLeft tdDataPaymentTerms'>" +
                "<div width = '100%'>" + data[item].Term + " " + data[item].Name + "</div>" +
            "</td>" +
            "</tr>");
        $("td#tdDataPaymentTerms" + item).append("//sales/controllers/PaymentTerms/views/contextMenuPaymentTerms.ejs", { index: item }, { id: data[item]._id });
    });
    $('.trDataPaymentTerms:odd').addClass('odd');
}

})

	});