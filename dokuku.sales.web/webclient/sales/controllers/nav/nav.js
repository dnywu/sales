steal('jquery/controller', 'jquery/view/ejs',
        'jquery/controller/view',
        './Nav.css',
        './NavSubMenu.css',
        'sales/controllers/items/create',
        'sales/controllers/customers',
        'sales/controllers/home'
     )
	.then('./views/nav.ejs', function ($) {
	    $.Controller('sales.controllers.nav',
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/nav/views/nav.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
            },
            '#HomeLink click': function () {
                this.ClearContain();
                $("#subtabs").empty();
                $("#body").sales_home('load');
            },
            '#CustomerLink click': function () {
                this.ClearContain();
                this.CustomerSubMenu();
                $("#body").sales_customers('load');
            },
            '#InvoiceLink click': function () {
                this.ClearContain();
                this.InvoiceSubMenu();
                $("#body").sales_items_create('load');
            },
            '#customers click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_items_create('load');
            },
            '#emailhistory click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_items_create('load');
            },
            CustomerSubMenu: function () {
                var submenu = $('#subtabs');
                var 
                    container = $('<div>', { 'class': 'container' }),
                    ul = $('<ul>', { 'class': 'ulsubtabs' }),
                    customers = $('<li>', { 'class': 'bold lisubtabs', id: 'customers', text: 'Customers' }),
                    emailhistory = $('<li>', { 'class': 'lisubtabs', id: 'emailhistory', text: 'Email History' });
                $("#subtabs").empty();
                container.appendTo(submenu);
                ul.appendTo(container);
                customers.appendTo(ul);
                emailhistory.insertAfter(customers);
            },
            InvoiceSubMenu: function () {
                var submenu = $('#subtabs');
                var 
                    container = $('<div>', { 'class': 'container' }),
                    ul = $('<ul>', { 'class': 'ulsubtabs' }),
                    invoices = $('<li>', { 'class': 'lisubtabs bold', id: 'invoices', text: 'Invoices' }),
                    recurringinvoices = $('<li>', { 'class': 'lisubtabs', id: 'recurringinvoices', text: 'Recurring Invoices' }),
                    creditnotes = $('<li>', { 'class': 'lisubtabs', id: 'creditnotes', text: 'Credit Notes' }),
                    paymentreceived = $('<li>', { 'class': 'lisubtabs', id: 'paymentreceived', text: 'Payment Received' });
                items = $('<li>', { 'class': 'lisubtabs', id: 'items', text: 'Items' });
                $("#subtabs").empty();
                container.appendTo(submenu);
                ul.appendTo(container);
                invoices.appendTo(ul);
                recurringinvoices.insertAfter(invoices);
                creditnotes.insertAfter(recurringinvoices);
                paymentreceived.insertAfter(creditnotes);
                items.insertAfter(paymentreceived);
            },
            ClearContain: function () {
                $("#body").empty();
            },
            SetBoldActivePage: function (el) {
                $("#subtabs li").removeClass('bold');
                el.addClass('bold');
            }
        })
	});