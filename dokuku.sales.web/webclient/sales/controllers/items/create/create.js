steal('jquery/controller',
      'jquery/view/ejs',
      'jquery/controller/view')
.then('./views/init.ejs', function ($) {
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
    },
    createTaxDialog: function () {
        alert("bikin pajak baru");
    }
})
});