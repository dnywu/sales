namespace dokuku.module
{
    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Bootstrapper;
    using Nancy.Cryptography;
    using TinyIoC;
    using System.Text;
    using System;
    using StructureMap;
    using dokuku.security.repository;
    using dokuku.security.service;
    using dokuku.security.model;
    using dokuku.sales.config;
    using dokuku.sales.item;
    using dokuku.sales.customer.repository;
    using dokuku.sales.organization.repository;
    using dokuku.sales.organization.report;
    using dokuku.sales.invoices.command;
    using dokuku.sales.invoices.query;
    using dokuku.sales.item.service;
    using dokuku.sales.customer.service;

    public class DokukuSalesAppBootstrapper : DefaultNancyBootstrapper
    {
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
            // Initialize the static ObjectFactory container
            ObjectFactory.Initialize(x =>
            {
                x.For<IAccountRepository>().Use<AccountRepository>();
                x.For<IItemCommand>().Use<ItemCommand>();
                x.For<IItemQuery>().Use<ItemQuery>();
                x.For<ICustomerRepository>().Use<CustomerRepository>();
                x.For<ICustomerReportRepository>().Use<CustomerReportRepository>();
                x.For<IOrganizationRepository>().Use<OrganizationRepository>();
                x.For<IOrganizationReportRepository>().Use<OrganizationReportRepository>();
                x.For<IAuthService>().Use<AuthService>();
                x.ForSingletonOf<MongoConfig>().Use<MongoConfig>();
                x.For<IInvoicesRepository>().Use<InvoicesRepository>();
                x.For<IInvoicesQueryRepository>().Use<InvoicesQueryRepository>();
                x.For<IInsertItemService>().Use<InsertItemService>();
            });
        }
    }
}