namespace dokuku.module
{
    using System;
    using System.Text;
    using dokuku.sales.config;
    using dokuku.sales.currency.report;
    using dokuku.sales.currency.service;
    using dokuku.sales.customer.repository;
    using dokuku.sales.customer.Service;
    using dokuku.sales.invoices.command;
    using dokuku.sales.invoices.query;
    using dokuku.sales.invoices.service;
    using dokuku.sales.item;
    using dokuku.sales.item.service;
    using dokuku.sales.organization.report;
    using dokuku.sales.organization.repository;
    //using dokuku.sales.payment.service;
    using dokuku.sales.paymentmode.query;
    using dokuku.sales.paymentmode.service;
    using dokuku.sales.taxes.query;
    using dokuku.sales.taxes.service;
    using dokuku.security.model;
    using dokuku.security.repository;
    using dokuku.security.service;
    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Bootstrapper;
    using Nancy.Cryptography;
    using NServiceBus;
    using StructureMap;
    using TinyIoC;
    using dokuku.sales.payment.readmodel;

    public class DokukuSalesAppBootstrapper : DefaultNancyBootstrapper
    {
        static IBus bus;
        static bool structureMapBootstrapped=false;

        protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        {
            // We don't call "base" here to prevent auto-discovery of
            // types/dependencies
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container)
        {
            base.ConfigureRequestContainer(container);

            // Here we register our user mapper as a per-request singleton.
            // As this is now per-request we could inject a request scoped
            // database "context" or other request scoped services.
            StartNServiceBus();
            BootstrapStructureMap();
            IAccountRepository accRepo = ObjectFactory.GetInstance<IAccountRepository>();
            container.Register<IUserMapper, UserMapper>(new UserMapper(accRepo));
        }

        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines)
        {
            // At request startup we modify the request pipelines to
            // include forms authentication - passing in our now request
            // scoped user name mapper.
            //
            // The pipelines passed in here are specific to this request,
            // so we can add/remove/update items in them as we please.

            CryptographyConfiguration cryptoConfig = new CryptographyConfiguration(
                    new RijndaelEncryptionProvider(new DokukuKeyGenerator()),
                    new DefaultHmacProvider(new DokukuKeyGenerator()));

            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration(cryptoConfig)
                {
                    RedirectUrl = System.Configuration.ConfigurationManager.AppSettings["LoginUrl"],
                    UserMapper = requestContainer.Resolve<IUserMapper>(),
                };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
            Jsonp.Enable(pipelines);
            pipelines.AfterRequest.AddItemToEndOfPipeline(SetCookieDomain);
        }

        private void SetCookieDomain(Nancy.NancyContext ctx)
        {
            string domainName = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            if (domainName == null || domainName.Trim() == string.Empty)
                return;

            foreach (Nancy.Cookies.INancyCookie cookie in ctx.Response.Cookies)
            {
                cookie.Domain = domainName;
            }
        }

        private void BootstrapStructureMap(){
            if (!structureMapBootstrapped)
            {
                // Initialize the static ObjectFactory container
                ObjectFactory.Initialize(x =>
                {
                    x.For<IAccountRepository>().Use<AccountRepository>();
                    x.For<IItemCommand>().Use<ItemCommand>();
                    x.For<IItemQuery>().Use<ItemQuery>();
                    x.For<ICustomerReportRepository>().Use<CustomerReportRepository>();
                    x.For<IOrganizationRepository>().Use<OrganizationRepository>();
                    x.For<IOrganizationReportRepository>().Use<OrganizationReportRepository>();
                    x.For<IAuthService>().Use<AuthService>();
                    x.ForSingletonOf<MongoConfig>().Use<MongoConfig>();
                    x.For<IInvoicesRepository>().Use<InvoicesRepository>();
                    x.For<IInvoicesQueryRepository>().Use<InvoicesQueryRepository>();
                    x.For<IItemService>().Use<ItemService>();
                    x.For<IInvoiceAutoNumberGenerator>().Use<InvoiceAutoNumberGenerator>();
                    x.For<IInvoiceService>().Use<InvoiceService>();
                    x.ForSingletonOf<IBus>().Use(bus);
                    x.For<ICustomerService>().Use<CustomerService>();
                    x.For<IServiceTax>().Use<ServiceTax>();
                    x.For<ITaxQueryRepository>().Use<TaxQueryRepository>();
                    x.For<ICurrencyService>().Use<CurrencyService>();
                    x.For<ICurrencyQueryRepository>().Use<CurrencyQueryRepository>();
                    x.For<IPaymentModeQuery>().Use<PaymentModeQuery>();
                    x.For<IPaymentModeService>().Use<PaymentModeService>();
                    x.For<ILogoOrganizationQuery>().Use<LogoOrganizationQuery>();
                    x.For<ILogoOrganizationCommand>().Use<LogoOrganizationCommand>();
                    x.For<IPaymentRepository>().Use<PaymentRepository>();
                });

                structureMapBootstrapped = true;
            }
        }
        private void StartNServiceBus()
        {
            if (bus == null)
            {
                bus = Configure.WithWeb()
                    .Log4Net()
                    .DefaultBuilder()
                    .BinarySerializer()
                    .MsmqTransport()
                        .IsTransactional(true)
                        .PurgeOnStartup(false)
                    .MsmqSubscriptionStorage()
                    .UnicastBus()
                        .LoadMessageHandlers()
                        .ImpersonateSender(true)
                    .CreateBus()
                    .Start();
                Configure.Instance.Configurer.ConfigureComponent<InvoiceService>(NServiceBus.ObjectBuilder.ComponentCallModelEnum.Singlecall);
            }
        }
    }
}