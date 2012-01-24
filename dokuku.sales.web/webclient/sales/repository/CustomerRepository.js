steal('jquery/class', function () {
    $.Class('CustomerRepository',
{
},
{
    init: function () {
    },
    GetCustomerByName: function (custName) {
        var customer = null;
        $.ajax({
            type: 'GET',
            url: '/getCustomerByCustomerName/' + custName,
            dataType: 'json',
            async: false,
            success: function (data) {
                customer = data;
            }
        });
        return customer;
    },
    GetCustomerById: function (id) {
        var customer = null;
        $.ajax({
            type: 'GET',
            url: '/GetDataCustomer/id/' + id,
            dataType: 'json',
            async: false,
            success: function (data) {
                customer = data;
            }
        });
        return customer;
    }
})
});