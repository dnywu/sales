steal('jquery/class', function () {
    $.Class('AddCustomer',
{
},
{
    init: function () {
        new ModalDialog("Tambah Pelanggan Baru");
        $("#dialogContent").html(this.view("//sales/controllers/invoices/create/views/AddCustomer.ejs"));
        this.TriggerAddCustEvent();
    }
})
});