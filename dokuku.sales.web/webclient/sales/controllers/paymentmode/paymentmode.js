steal('jquery/controller', 'jquery/view/ejs', 'sales/repository/PaymentModeRepository.js', './paymentmode.css')
	.then('./views/init.ejs', './views/listpaymentmode.ejs', './views/editpaymentmode.ejs', './views/addpaymentmode.ejs', function ($) {

	    /**
	    * @class Sales.Controllers.Paymentmode
	    */
	    $.Controller('Sales.Controllers.Paymentmode',
	    /** @Static */
{
defaults: (paymentModeRepo = null)
},
	    /** @Prototype */
{
init: function () {
    paymentModeRepo = new PaymentModeRepository();
    this.element.html(this.view("//sales/controllers/paymentmode/views/listpaymentmode"));
    this.requestAllPaymentModeSuccess(paymentModeRepo.getAllPaymentMode());
},
load: function () {
    paymentModeRepo = new PaymentModeRepository();
    this.element.html(this.view("//sales/controllers/paymentmode/views/listpaymentmode"));
    this.requestAllPaymentModeSuccess(paymentModeRepo.getAllPaymentMode());
},
create: function () {
    this.element.html(this.view("//sales/controllers/paymentmode/views/addpaymentmode"));
},
'#btn-add-PaymentMode click': function (el, ev) {
    ev.preventDefault();
    $("#body").sales_paymentmode('create');
    $('#kode').focus();
},
'#PaymentModeSave click': function () {
    var kode = $("#kode").val();
    var nama = $("#nama").val();
    var paymentMode = new Object();
    paymentMode.Name = nama;
    paymentMode.Code = kode;
    if (paymentModeRepo.createPaymentMode(paymentMode)) {
        $("#body").sales_paymentmode("load");
    }
},
viewEditPaymentMode: function (item) {
    this.element.html("//sales/controllers/paymentmode/views/editpaymentmode.ejs", item);

},
"#CancelPaymentMode click": function () {
    $("#body").sales_paymentmode("load");
},
"#checkboxAllPaymentMode change": function () {
    if ($("#checkboxAllPaymentMode").attr('checked')) {
        $(".checkboxPaymentMode").attr('checked', 'checked');
    } else {
        $(".checkboxPaymentMode").removeAttr('checked');
    }
},
'#deletePaymentMode click': function () {
    $('.ConfirmationPaymentMode').show();
},
'.ButtonConfirmationPaymentModeYa click': function () {
    $('.checkboxPaymentMode:checked').each(function (index) {
        var id = $(this).val();
        paymentModeRepo = new PaymentModeRepository();
        paymentModeRepo.deletePaymentMode(id);
    });
    $('.checkboxPaymentMode:checked').parent().parent().remove();
    $('.ConfirmationPaymentMode').hide();
},
'.ButtonConfirmationPaymentModeTidak click': function () {
    $('.ConfirmationPaymentMode').hide();
},
'table.dataPaymentMode tbody.BodyDataPaymentMode tr.trDataPaymentMode hover': function (el) {
    var index = el.attr('tabindexPaymentMode');
    $('#settingListPaymentMode' + index).show();
    $("tr#trPaymentModeList" + index + " td#tdDataPaymentMode" + index + " div.ContextMenuPaymentMode").hide();
},
'table.dataPaymentMode tbody.BodyDataPaymentMode tr.trDataPaymentMode mouseleave': function (el) {
    var index = el.attr('tabindexPaymentMode');
    $('#settingListPaymentMode' + index).hide();
    $("tr#trPaymentModeList" + index + " td#tdDataPaymentMode" + index + " div.ContextMenuPaymentMode").hide();
},
'.settingPaymentMode click': function (el) {
    var index = el.attr('tabindexPaymentMode');
    $("tr#trPaymentModeList" + index + " td#tdDataPaymentMode" + index + " div.ContextMenuPaymentMode").show();
},
'.EditContextMenuPaymentMode click': function (el) {
    var id = el.attr('id');
    var PaymentMode = paymentModeRepo.getPaymentModeById(id);
    $('#body').sales_paymentmode('viewEditPaymentMode', PaymentMode);
    $('#kode').focus();
},
'#UpdatePaymentMode click': function (el, ev) {
    ev.preventDefault();
    var id = $("#_id").val();
    var nama = $("#nama").val();
    var kode = $("#kode").val();

    var PaymentMode = new Object();
    PaymentMode._id = id;
    PaymentMode.Name = nama;
    PaymentMode.Code = kode;
    var PaymentMode = paymentModeRepo.updatePaymentMode(PaymentMode);
    $("#body").sales_paymentmode('load')
},

requestAllPaymentModeSuccess: function (data) {
    $("table.dataPaymentMode tbody").empty();
    $.each(data, function (item) {
        $("table.dataPaymentMode tbody.BodyDataPaymentMode").append(
            "<tr class='trDataPaymentMode' id='trPaymentModeList" + item + "' tabindexPaymentMode='" + item + "'>" +
            "<td class='thDataPaymentMode tdDataPaymentMode tdDataPaymentModeCenter textAlignRight' style='text-align:center'><input type='checkbox' class='checkboxPaymentMode' id='checkboxPaymentMode'value='" + data[item]._id + "'/></td>" +
            "<td class='tdDataPaymentModeCenter tdDataPaymentMode' id='tdDataPaymentMode" + item + "'><div class='settingPaymentMode' id='settingListPaymentMode" + item + "' tabindexPaymentMode='" + item + "'><img class='' src='/sales/controllers/PaymentMode/images/setting.png'/></div></td>" +
            "<td class='tdDataPaymentModeLeft tdDataPaymentMode'>" +
                "<div width = '100%'>" + data[item].Code + "</div>" +
            "</td>" +
             "<td class='tdDataPaymentModeLeft tdDataPaymentMode'>" +
                "<div width = '100%'>" + data[item].Name + "</div>" +
            "</td>" +
            "</tr>");
        $("td#tdDataPaymentMode" + item).append("//sales/controllers/PaymentMode/views/contextMenuPaymentMode.ejs", { index: item }, { id: data[item]._id });
    });
    $('.trDataPaymentMode:odd').addClass('odd');
}

})

	});