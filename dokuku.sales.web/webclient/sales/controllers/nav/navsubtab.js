steal('jquery/controller',
        'jquery/view/ejs',
        'jquery/controller/view',
        'sales/models'
     )
	.then(function ($) {
	    $.Controller('sales.controllers.navsubtab',
        {
            init: function () {
            },
            empty: function () {
                this.element.html("");
            },
            home: function () {
                this.element.html("");
            },
            invoices: function () {
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
            customers: function () {
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
            '#invoices click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_invoices_list('load');
            },
            '#customers click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_customerscontrol('load');
            },
            '#emailhistory click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_customerscontrol('load');
            },
            '#items click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_items_list('load');
            },
            '#setupautonumbering click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_setupautonumberingcontrol('load');
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
                $("#body").sales_paymentreceived();
            },
            '#settingorganization click': function (el) {
                this.ClearContain();
                this.SetBoldActivePage(el);
                $("#body").sales_settingorganization('load');
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