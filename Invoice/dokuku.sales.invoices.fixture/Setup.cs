using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Spec;
using dokuku.sales.invoices.commands;
using dokuku.sales.invoices.events;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.CommandService.Infrastructure;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs;
using Ncqrs.Config.StructureMap;
using dokuku.sales.invoice.services;
using dokuku.sales.invoices.domain;
using dokuku.sales.config;
using dokuku.sales.invoices.repository;
using dokuku.sales.invoices.repository.impl;
namespace dokuku.sales.invoices.fixture
{
    public static class Setup
    {
        private static bool initialized = false;
        public static void SetupInvoiceFixture(this BaseTestFixture btf)
        {
            if (!initialized)
            {
                NcqrsEnvironment.Configure(new StructureMapConfiguration(
                   x =>
                   {
                       x.For<ICommandService>().Use(InitCommandService());
                       x.For<ICustomerRepository>().Use(new CustomerRepository());
                   }
                ));
                initialized = true;
            }
        }

        private static ICommandService InitCommandService()
        {
            var service = new CommandService();
            service.RegisterExecutorsInAssembly(typeof(CreateInvoice).Assembly);
            service.RegisterExecutor(new CreateInvoiceService());
            service.RegisterExecutor(new AddInvoiceItemService());
            service.AddInterceptor(new ThrowOnExceptionInterceptor());
            return service;
        }
    }
}