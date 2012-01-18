steal('jquery/class', function () {
    $.Class('AddCustomer',
{
},
{
    init: function () {
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
        alert(JSON.stringify($("#formAddCustDialog").formParams()));
    }
})
});