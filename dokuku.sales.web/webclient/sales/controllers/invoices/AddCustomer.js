steal('jquery/class', function () {
    $.Class('AddCustomer',
{
    defaults: ($this = null)
},
{
    init: function () {
        $this = this;
    },
    TriggerEvent: function () {
        $("#moreFieldCustomer").click(this.MoreFieldAddCust);
        $("#btnCancelAddCust").click(this.CloseAddCustDialog);
        $("#createCust").click(this.AddCust);

    }, MoreFieldAddCust: function () {
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
                }
                $this.CloseAddCustDialog();
                $("#CustomerId").val(data._id);
                $("#selectcust").val(data.Name);
            }
        });
    }
})
});