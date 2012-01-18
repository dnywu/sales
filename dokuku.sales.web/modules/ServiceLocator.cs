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
using dokuku.sales.customer.service;

namespace dokuku.sales.web.modules
{
    public static class ServiceLocator
    {
        internal static IAuthService GetAuthenticationService(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IAuthService>();
        }
        public static ICustomerRepository CustomerRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<ICustomerRepository>();
        }
        public static ICustomerReportRepository CustomerReportRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<ICustomerReportRepository>();
        }
        public static IOrganizationRepository OrganizationRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IOrganizationRepository>();
        }
        public static IOrganizationReportRepository OrganizationReportRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IOrganizationReportRepository>();
        }
        public static IItemCommand ItemCommand(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IItemCommand>();
        }
        public static IItemQuery ItemQuery(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IItemQuery>();
        }
        public static IAccountRepository AccountRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IAccountRepository>();
        }
        public static IInvoicesRepository InvoicesRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoicesRepository>();
        }
        public static IInvoicesQueryRepository InvoicesQueryRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoicesQueryRepository>();
        }
        public static IInsertItemService InsertItemService(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInsertItemService>();
        }
        public static IInvoiceService InvoiceService(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IInvoiceService>();
        }
    }
}