steal('jquery/controller', 'jquery/view/ejs',
        'jquery/controller/view',
        'sales/models',
        './Nav.css',
        './NavSubMenu.css',
        'sales/controllers/items/list',
        'sales/controllers/customers',
        'sales/controllers/home',
        'sales/controllers/invoices/list',
        'sales/controllers/setupautonumbering',
        'sales/controllers/currencyandtax',
        'sales/controllers/payment'
     )
	.then('./views/nav.ejs', function ($) {
	    $.Controller('sales.controllers.nav',
        {
            onDocument: true
        },
        {
            init: function () {
                this.element.html(this.view("//sales/controllers/nav/views/nav.ejs", Sales.Models.Companyprofile.findOne({ id: '1' })));
            },
            '#HomeLink click': function (el) {
                this.ClearContain();
                $("#subtabs").empty();
                this.SetActivePage(el);
                $("#body").sales_home('load');
            },
            '#CustomerLink click': function (el) {
                this.ClearContain();
                this.CustomerSubMenu();
                this.SetActivePage(el);
                $("#body").sales_customers('load');
            },
            '#InvoiceLink click': function (el) {
                this.ClearContain();
                this.InvoiceSubMenu();
                this.SetActivePage(el);
                $("#body").sales_invoices_list('load');
            },
            '#invoices click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_invoices_list('load');
            },
            '#customers click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_customers('load');
            },
            '#emailhistory click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_customers('load');
            },
            '#items click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_items_list('load');
            },
            '#SettingLink click': function (el) {
                this.ClearContain();
                this.SettingSubMenu();
                this.SetActivePage(el);
                $("#body").sales_setupautonumbering('load');
            },
            '#setupautonumbering click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_setupautonumbering('load');
            },
            '#currencyandtax click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_currencyandtax('load');
            },
            '#paymentreceived click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_payment('load');
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
            SettingSubMenu: function () {
                var submenu = $('#subtabs');
                var 
                    container = $('<div>', { 'class': 'container' }),
                    ul = $('<ul>', { 'class': 'ulsubtabs' }),
                    setupautonumbering = $('<li>', { 'class': 'bold lisubtabs', id: 'setupautonumbering', text: 'Auto Numbering' });
                currencyandtax = $('<li>', { 'class': 'lisubtabs', id: 'currencyandtax', text: 'Mata Uang & Pajak' });
                $("#subtabs").empty();
                container.appendTo(submenu);
                ul.appendTo(container);
                setupautonumbering.appendTo(ul);
                currencyandtax.insertAfter(setupautonumbering);
            },
            ClearContain: function () {
                $("#body").empty();
            },
            SetBoldActivePage: function (el) {
                $("#subtabs li").removeClass('bold');
                el.addClass('bold');
            },
            SetActivePage: function (el) {
                $("ul.ultabs li").removeClass('active');
                el.addClass('active');
            }
        })
	});