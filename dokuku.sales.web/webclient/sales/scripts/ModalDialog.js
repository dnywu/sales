steal('jquery/class', function () {
    $.Class('ModalDialog',
{
},
{
    init: function (header) {
        this.initElement(header);
    },
    initElement: function (title) {
        var 
            modalDialog = $('<div>', { 'class': 'ModalDialog' }),
            dialogOverlay = $('<div>', { 'class': 'DialogOverlay' }),
            dialogDetail = $('<div>', { 'class': 'Detail' }),
            dialogBox = $("<table class='DialogBox'><tbody><tr style='height:34px;'><td id='dialogHeaderPlace'></td></tr><tr><td id='dialogContent'></td></tr></tbody></table>"),
            header = $('<div id="DialogHeader"><label id="dialogTitle"></label><input id="btnClose" type="button" value="X" /></div>');

        modalDialog.appendTo('body');
        dialogOverlay.appendTo('.ModalDialog');
        dialogDetail.insertAfter(dialogOverlay);
        dialogBox.appendTo('.Detail');
        header.appendTo('#dialogHeaderPlace');
        $("#dialogTitle").text(title);
        $("#btnClose").click(this.DestroyModalDialog);
    },
    DestroyModalDialog: function () {
        $(".ModalDialog").remove();
    }
})
});
