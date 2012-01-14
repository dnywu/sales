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
        public static IItemRepository ItemRepository(this Nancy.NancyModule mod)
        {
            return ObjectFactory.GetInstance<IItemRepository>();
        }
    }
}