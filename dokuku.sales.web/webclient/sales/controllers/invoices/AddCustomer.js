﻿steal('jquery/class', 'sales/repository/CustomerRepository.js', function () {
    $.Class('AddCustomer',
{
    defaults: ($this = null, custRepo = null)
},
{
    init: function () {
        $this = this;
        custRepo = new CustomerRepository();
    },
    TriggerEvent: function () {
        $("#moreFieldCustomer").click(this.MoreFieldAddCust);
        $("#btnCancelAddCust").click(this.CloseAddCustDialog);
        $("#createCust").click(this.AddCust);
    },
    MoreFieldAddCust: function () {
        $("tr#trmoreFieldCustomer").remove();
        $("table.hiddenTable").show();
    },
    CloseAddCustDialog: function () {
        $(".ModalDialog").remove();
    },
    AddCust: function (el, ev) {
        var name = $("#CustName").val();
        if (name == "") {
            $("#errorAddCust").text("Nama Pelanggan harus di isi").show();
            return;
        }
        $.ajax({
            type: 'POST',
            url: '/customer/data',
            data: { 'data': JSON.stringify($("#formAddCustDialog").formParams()) },
            async: false,
            success: function (data) {
                if (data == null) {
                    $("#errorAddCust").text(data).show();
                    return;
                }
                $this.CloseAddCustDialog();
                var customer = custRepo.GetCustomerByName(data.Name);
                if (customer != null) {
                    $("#keteranganSelectCust").empty();
                    $("#CustomerId").val(data._id);
                    $("#selectcust").val(data.Name);
                    $("#currency").text(customer.Currency).show();
                    $("#selectcust").change();
                }
            }
        });
    }
})
});