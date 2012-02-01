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
        'sales/controllers/payment',
        'sales/controllers/paymentreceived',
        'sales/controllers/setuporganization/settingorganization.js',
        'sales/controllers/paymentmode'
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
            '#paymentmode click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_paymentmode('load');
                $("#kode").focus();
            },
            '#paymentreceived click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_paymentreceived('load');
            },
            '#settingorganization click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_settingorganization('load');
            },
            CustomerSubMenu: function () {
                var submenu = $('#subtabs');
                var 
                    container = $('<div>', { 'class': 'container' }),
                    ul = $('<ul>', { 'class': 'ulsubtabs' }),
                    customers = $('<li>', { 'class': 'bold lisubtabs', id: 'customers', text: 'Pelanggan' }),
                    emailhistory = $('<li>', { 'class': 'lisubtabs', id: 'emailhistory', text: 'Berkas Email' });
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
                    invoices = $('<li>', { 'class': 'lisubtabs bold', id: 'invoices', text: 'Faktur' }),
                    recurringinvoices = $('<li>', { 'class': 'lisubtabs', id: 'recurringinvoices', text: 'Faktur Terjadwal' }),
                    creditnotes = $('<li>', { 'class': 'lisubtabs', id: 'creditnotes', text: 'Nota Kredit' }),
                    paymentreceived = $('<li>', { 'class': 'lisubtabs', id: 'paymentreceived', text: 'Terima Pembayaran' });
                items = $('<li>', { 'class': 'lisubtabs', id: 'items', text: 'Barang' });
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
                    setupautonumbering = $('<li>', { 'class': 'bold lisubtabs', id: 'setupautonumbering', text: 'Penomoran Otomatis' });
                currencyandtax = $('<li>', { 'class': 'lisubtabs', id: 'currencyandtax', text: 'Mata Uang & Pajak' });
                settingOrganization = $('<li>', { 'class': 'lisubtabs', id: 'settingorganization', text: 'Informasi Perusahaan' });
                paymentmode = $('<li>', { 'class': 'lisubtabs', id: 'paymentmode', text: 'Jenis Pembayaran' });
                $("#subtabs").empty();
                container.appendTo(submenu);
                ul.appendTo(container);
                setupautonumbering.appendTo(ul);
                currencyandtax.insertAfter(setupautonumbering);
                settingOrganization.insertAfter(currencyandtax);
                paymentmode.insertAfter(currencyandtax);
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