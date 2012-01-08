steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemsCreate.css',
      '//sales/scripts/ModalDialog.js')
.then('./views/createTaxDialog.ejs',
      './views/init.ejs', function ($) {
          $.Controller('Sales.Items.Create',
{
    defaults: {}
},
{
    init: function () {
        $('#section').empty();
        this.element.html(this.view("//sales/controllers/items/create/views/init.ejs"));
    },
    "#createTaxLink click": function (el, ev) {
        this.createTaxDialog();
        ev.preventDefault();
        this.triggerNewEvent();
    },
    createTaxDialog: function () {
        new ModalDialog("Pajak Baru");
        $("#dialogContent").html(this.view("//sales/controllers/items/create/views/createTaxDialog.ejs"));
    },
    triggerNewEvent: function () {
        $(".submitTax").click(this.submitTax);
    },
    "#createItemsForm submit": function (el, ev) {
        var form = $("#createItemsForm");
        var err = $("#errorCreateItemDiv");
        var defaults = {
            name: $("#itemName").val(),
            description: $("#description").val(),
            price: $("#itemPrice").val(),
            tax: $("#tax").val()
        };
        err.empty();
        if (defaults.name !== "" && defaults.price != 0)
            form.submit();
        if (defaults.name == "")
            $('<li>', { 'class': 'name', text: "Nama Barang harus di isi" }).appendTo(err.show());
        if (defaults.price == "")
            $('<li>', { 'class': 'price', text: "Harga harus di diisi" }).appendTo(err.show());
        ev.preventDefault();
        return;
    },
    submitTax: function (el, ev) {
        var form = $("#taxForm");
        var err = $("#errorCreateTaxDiv");
        var defaults = {
            name: $("#taxName").val(),
            percent: $("#percentTax").val()
        };
        err.empty();
        if (defaults.name !== "" && defaults.percent != 0)
            form.submit();
        if (defaults.name == "")
            $('<li>', { 'class': 'name', text: "Nama Pajak harus di isi" }).appendTo(err.show());
        if (defaults.percent == "")
            $('<li>', { 'class': 'percenttax', text: "Persentase Pajak harus di diisi" }).appendTo(err.show());
        if (defaults.percent <= 0)
            $('<li>', { 'class': 'percenttax', text: "Persentase Pajak harus lebih besar dari nol" }).appendTo(err.show());
        ev.preventDefault();
        return;
    },
    "#itemName keypress": function () {
        $('li.name').remove();
        if ($("#error").is(':empty'))
            $("#error").hide();
    },
    "#itemPrice keypress": function () {
        $('li.price').remove();
        if ($("#error").is(':empty'))
            $("#error").hide();
    },
    "#taxName keypress": function () {
        alert("test");
        $('li.name').remove();
        if ($("#error").is(':empty'))
            $("#error").hide();
    },
    "#percentTax keypress": function () {
        $('li.percenttax').remove();
        if ($("#error").is(':empty'))
            $("#error").hide();
    }
})
      });