using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.model;
using dokuku.sales.config;
using dokuku.sales.organization.report;
namespace dokuku.sales.fixture
{
    [Subject("Creating organization")]
    public class When_create_organization
    {
        private static IOrganizationRepository orgRepo;
        private static IOrganizationReportRepository orgReportRepo;
        private static Organization org;
        static MongoConfig mongo;
        static string email = "oetawan@inforsys.co.id";
        Establish context = () =>
            {
                mongo = new MongoConfig();
                orgRepo = new OrganizationRepository(mongo);
                orgReportRepo = new OrganizationReportRepository(mongo);
            };

        Because of = () =>
            {
                orgRepo.Save(new Organization()
                {
                    _id = email,
                    OwnerId= email,
                    FiscalYearPeriod= 1,
                    Currency= "IDR",
                    Name = "Inforsys"
                });
            };

        It should_create_organization = () =>
            {
                org = orgReportRepo.FindById(email);
                org.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                orgRepo.Delete(org._id);
            };
    }
}