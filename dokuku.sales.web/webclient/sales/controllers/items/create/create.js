steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemsCreate.css',
      '//sales/scripts/ModalDialog.js')
.then('./views/init.ejs',
      './views/createTaxDialog.ejs', function ($) {
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
        //        new ModalDialog("Pajak Baru");
        this.createTaxDialog();
        ev.preventDefault();
    },
    createTaxDialog: function () {
        new ModalDialog("Pajak Baru");
        $("#dialogContent").append("//sales/controllers/items/create/views/createTaxDialog.ejs", {});
    },
    "createItemsForm submit": function (el, ev) {
        var form = $("#createItemsForm");
        var err = $("#error");
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
    "createTaxForm submit": function (el, ev) {
        var form = $("#createTaxForm");
        var err = $("#error");
        var defaults = {
            name: $("#taxName").val(),
            percent: $("#percentTax").val()
        };
        err.empty();
        if (defaults.name !== "" && defaults.percent != 0)
            form.submit();
        if (defaults.name == "")
            $('<li>', { 'class': 'name', text: "Nama Pajak harus di isi" }).appendTo(err.show());
        if (defaults.price == "")
            $('<li>', { 'class': 'percenttax', text: "Persentase Pajak harus di diisi" }).appendTo(err.show());
        if (defaults.price <= 0)
            $('<li>', { 'class': 'percenttax', text: "Persentase Pajak harus lebih besar dari nol" }).appendTo(err.show());
        ev.preventDefault();
        return;
    }
})
});