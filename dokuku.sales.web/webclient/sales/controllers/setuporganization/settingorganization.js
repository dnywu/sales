steal('jquery/controller', 'jquery/view/ejs',
		'jquery/controller/view',
        'sales/controllers/setuporganization/settingorganization.css'
	)
	.then('./views/settingorganization.ejs', function ($) {
	    $.Controller('sales.Controllers.settingorganization',
        {
            defaults: ($this = null)
        },
	    {
	        init: function () {
	            $this = this;
	            this.load();
	        },
	        load: function () {
	            var organization = this.GetOrganization();
	            this.element.html(this.view("//sales/controllers/setuporganization/views/settingorganization.ejs", organization));
	            this.selectFiscalPeriod(organization.FiscalYearPeriod);
	        },
	        GetOrganization: function () {
	            var org;
	            $.ajax({
	                type: 'GET',
	                url: '/getorganization',
	                async: false,
	                dataType: 'json',
	                success: function (data) {
	                    org = data;
	                }
	            });
	            return org;
	        },
	        selectFiscalPeriod: function (period) {
	            $('.periode').each(function (index) {
	                if ($(this).val() == period) {
	                    $(this).attr('selected', true);
	                }
	            });
	        },
	        '#submitSettingOrganization click': function (el, ev) {
	            var org = $("#settingOrg").formParams();
	            var data = JSON.stringify(org);
	            ev.preventDefault();
	            $.ajax({
	                type: 'POST',
	                url: '/settingorganization',
	                async: false,
	                data: { Organization: data },
	                dataType: 'json',
	                success: function (data) {
	                    if (data.error) {
	                        return alert(data.message);
	                    }
	                    return alert("Sukses Ubah Informasi Perusahaan");
	                }
	            });
	        },
	        '#uploadLogo click': function () {
	            window.open("/uploadlogo", "Upload Logo Perusahaan Anda", null, null);
	        }
	    })
	});