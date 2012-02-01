using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.model;
using dokuku.sales.organization.report;
using dokuku.sales.config;
namespace dokuku.sales.fixture
{
    [Subject("Find organization by owner id")]
    public class When_find_organization_by_owner_id
    {
        private static IOrganizationRepository orgRepo;
        private static IOrganizationReportRepository orgReportRepo;
        private static Organization org;
        private static Guid id;
        static MongoConfig mongo;
        static string email = "oetawan@inforsys.co.id";
        Establish context = () =>
            {
                mongo = new MongoConfig();
                orgRepo = new OrganizationRepository(mongo);
                orgReportRepo = new OrganizationReportRepository(mongo);
                id = Guid.NewGuid();
            };

        Because of = () =>
            {
                orgRepo.Save(new Organization()
                {
                    _id = email,
                    OwnerId = email,
                    FiscalYearPeriod = 1,
                    Currency = "IDR",
                    Name = "Inforsys"
                });
            };

        It should_return_organization = () =>
            {
                Organization org = orgReportRepo.FindByOwnerId(email);
                org.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                orgRepo.Delete(email);
            };
    }
}