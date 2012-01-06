namespace dokuku.security
{
    using Nancy;
    using Nancy.Authentication.Forms;
    using Nancy.Bootstrapper;
    using Nancy.Cryptography;
    using TinyIoC;
    using System.Text;
    using System;

    public class FormsAuthBootstrapper : DefaultNancyBootstrapper
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
            container.Register<IUserMapper, AuthRepository>();
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
}