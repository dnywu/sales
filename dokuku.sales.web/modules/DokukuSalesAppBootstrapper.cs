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
            container.Register<IUserMapper, UserMapper>();
            AppBootstrapper.Bootstrap();
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
    }
    public static class AppBootstrapper
    {
        public static void Bootstrap()
        {
            // Initialize the static ObjectFactory container
            ObjectFactory.Initialize(x =>
            {
                x.For<IAuthRepository>().TheDefaultIsConcreteType<AuthRepository>();
                x.For<IItemRepository>().TheDefaultIsConcreteType<ItemRepository>();
                x.For<ICustomerRepository>().TheDefaultIsConcreteType<CustomerRepository>();
                x.For<ICustomerReportRepository>().TheDefaultIsConcreteType<CustomerReportRepository>();
                x.For<IOrganizationRepository>().TheDefaultIsConcreteType<OrganizationRepository>();
                x.For<IOrganizationReportRepository>().TheDefaultIsConcreteType<OrganizationReportRepository>();
                x.For<IAuthService>().TheDefaultIsConcreteType<AuthService>();
                x.ForSingletonOf<MongoConfig>().Use<MongoConfig>();
            });
        }
    }
}