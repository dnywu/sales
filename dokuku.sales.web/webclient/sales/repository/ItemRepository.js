steal('jquery/class', function () {
    $.Class('ItemRepository',
    {
},
    {
        init: function () {
        },
        GetItemByName: function (partName) {
            var part;
            try {
                $.ajax({
                    type: 'GET',
                    url: '/getItemByName/' + partName,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        part = data;
                    }
                });
            } catch (ex) {
                console.log(ex);
            }
            return part;
        },
        SearchItem: function (key) {
            var part;
            try {
                $.ajax({
                    type: 'GET',
                    url: '/searchItem/keyword/' + key,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        part = data;
                    }
                });
            } catch (ex) {
                console.log(ex);
            }
            return part;
        }
    })
});