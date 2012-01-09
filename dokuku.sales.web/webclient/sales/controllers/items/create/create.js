steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view',
      './ItemsCreate.css',
      'sales/scripts/ModalDialog.js',
      'sales/styles/ModalDialog.css')
.then('./views/createTaxDialog.ejs',
      './views/init.ejs', function ($) {
          $.Controller('sales.Controllers.items.create',
        {
            onDocument: true
        },
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/items/create/views/init.ejs"));
            },
            load: function () {
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
                $("#cancelCreateTax").click(this.cancelCreateTaxClick);
                $("#taxName").keypress(this.taxNameKeypress);
                $("#percentTax").keypress(this.percentTaxKeypress);
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
                if (defaults.description.length>500)
                    $('<li>', { 'class': 'description', text: "Deskripsi barang tidak boleh lebih dari 500 karakter" }).appendTo(err.show());
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
                if ($("#errorCreateItemDiv").is(':empty'))
                    $("#errorCreateItemDiv").hide();
            },
            taxNameKeypress: function () {
                $('li.name').remove();
                if ($("#errorCreateTaxDiv").is(':empty'))
                    $("#errorCreateTaxDiv").hide();
            },
            percentTaxKeypress: function () {
                $('li.percenttax').remove();
                if ($("#errorCreateTaxDiv").is(':empty'))
                    $("#errorCreateTaxDiv").hide();
            },
            cancelCreateTaxClick: function () {
                $("#taxName").val("");
                $("#percentTax").val("");
                $(".ModalDialog").remove();
            },
            "#cancelCreateItem click": function () {
                $("#itemName").val("");
                $("#description").val("");
                $("#itemPrice").val("");
                $("#tax").val("");
            }
        })
});