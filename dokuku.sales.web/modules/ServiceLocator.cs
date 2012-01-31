using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using dokuku.security.service;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.report;
using dokuku.sales.customer.repository;
using dokuku.sales.item;
using dokuku.security.repository;
using dokuku.sales.invoices.command;
using dokuku.sales.invoices.query;
using dokuku.sales.item.service;
using dokuku.sales.invoices.service;
using dokuku.sales.customer.Service;
//using dokuku.sales.payment.service;
using Nancy;
using dokuku.sales.taxes.service;
using dokuku.sales.taxes.query;
using dokuku.sales.currency.service;
using dokuku.sales.currency.report;
using dokuku.sales.paymentmode.query;
using dokuku.sales.paymentmode.service;

namespace dokuku.sales.web.modules
{
    public static class ServiceLocator
    {
        internal static IAuthService GetAuthenticationService(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IAuthService>();
        }
        public static ICustomerReportRepository CustomerReportRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<ICustomerReportRepository>();
        }
        public static IOrganizationRepository OrganizationRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IOrganizationRepository>();
        }
        public static IOrganizationReportRepository OrganizationReportRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IOrganizationReportRepository>();
        }
        public static IItemCommand ItemCommand(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IItemCommand>();
        }
        public static IItemQuery ItemQuery(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IItemQuery>();
        }
        public static IAccountRepository AccountRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IAccountRepository>();
        }
        public static IInvoicesRepository InvoicesRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoicesRepository>();
        }
        public static IInvoicesQueryRepository InvoicesQueryRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoicesQueryRepository>();
        }
        public static IItemService ItemService(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IItemService>();
        }
        public static IInvoiceService InvoiceService(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoiceService>();
        }
        public static ICustomerService CustomerService(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<ICustomerService>();
        }

        public static IInvoiceAutoNumberGenerator InvoiceAutoNumberGenerator(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoiceAutoNumberGenerator>();
        }
        public static IServiceTax ServiceTax(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<IServiceTax>();
        }
        public static ITaxQueryRepository TaxQueryRepository(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<ITaxQueryRepository>();
        }
        public static ICurrencyService CurrencyService(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<ICurrencyService>();
        }
        public static ICurrencyQueryRepository CurencyQueryRepo(this NancyModule mod)
        {
            return ObjectFactory.GetInstance<ICurrencyQueryRepository>();
        }
        public static IPaymentModeQuery PaymentModeQuery(this NancyModule module)
        {
            return ObjectFactory.GetInstance<IPaymentModeQuery>();
        }
        public static IPaymentModeService PaymentModeService(this NancyModule module)
        {
            return ObjectFactory.GetInstance<IPaymentModeService>();
        }
    }
}