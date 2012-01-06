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
        this.validationCheck();

    },
    validationCheck: function () {
        
    }
})
});