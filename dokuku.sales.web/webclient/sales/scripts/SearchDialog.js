steal('jquery/class','sales/styles/SearchDialog.css', function () {
    $.Class('SearchDialog',
{
},
{
    init: function () {
        this.initElement();
    },
    initElement: function () {
        var 
            searchDialog = $('<div>', { 'class': 'SearchDialog' }),
            inputField = $('<input>', { type: 'text' });

        searchDialog.appendTo('body');
        inputField.appendTo(searchDialog);
        searchDialog.show();
    },
    DestroySearchDialog: function () {
        $(".SearchDialog").remove();
    }
})
});
