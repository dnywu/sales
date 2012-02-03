using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Spec;
using dokuku.sales.payment.commands;
using dokuku.sales.payment.events;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.CommandService.Infrastructure;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs;
using Ncqrs.Config.StructureMap;
using Ncqrs.Eventing.Storage;
namespace dokuku.sales.payment.fixture
{
    public static class SetUp
    {
        private static bool initialized = false;
        public static void SetupInvoicePaymentFixture(this BaseTestFixture btf)
        {
            if (!initialized)
            {
                NcqrsEnvironment.Configure(new StructureMapConfiguration(
                   x =>
                   {
                       x.For<ICommandService>().Use(InitCommandService());
                   }
                ));
                initialized = true;
            }
        }

        private static ICommandService InitCommandService()
        {
            var service = new CommandService();
            service.RegisterExecutorsInAssembly(typeof(CreateInvoicePayment).Assembly);
            service.AddInterceptor(new ThrowOnExceptionInterceptor());
            return service;
        }
    }
}