﻿steal('jquery/class', 'sales/controllers/invoices/Invoice.js', function () {
    $.Class('AddItem',
{
    defaults: ($this = null, index = null, inv = null)
},
{
    init: function (i) {
        $this = this;
        index = i;
        inv = new Invoice();
    },
    TriggerEvent: function () {
        $("#btnCancelAddItem").click(this.CloseAddItemDialog);
        $("#createItem").click(this.AddItem);
    },
    CloseAddItemDialog: function () {
        $(".ModalDialog").remove();
    },
    AddItem: function () {
        var name = $("#itemName").val();
        var harga = $("#rate").val();
        var description = $("#decription").val();
        var taxName = $("#tax").text();
        var taxValue = $("#tax").val();
        if (name == "") {
            $("#errorAddItem").text("Nama Barang harus di isi").show();
            return;
        }
        if (harga == "") {
            $("#errorAddItem").text("Harga Barang harus di isi").show();
            return;
        }
        var item = new Object;
        item.Name = name;
        item.Rate = harga;
        item.Description = description;
        item.Tax = new Object();
        item.Tax.Name = taxName.trim();
        item.Tax.Value = taxValue;

        $.ajax({
            type: 'POST',
            url: '/createnewitem',
            data: { 'data': JSON.stringify(item) },
            async: false,
            success: function (data) {
                if (data == null) {
                    $("#errorAddCust").text(data).show();
                    return;
                }
                $this.CloseAddItemDialog();
                inv.ShowListItem(data, index);
                $this.GetSubTotal();
                $this.GetTotal();
                $("#additem_" + index).hide();
            }
        });
    },
    GetSubTotal: function () {
        $("#subtotal").text(inv.CalculateSubTotal);
    },
    GetTotal: function () {
        $("#total").text(inv.CalculateTotal);
    }
})
});