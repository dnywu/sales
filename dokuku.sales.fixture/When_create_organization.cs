using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.model;
using dokuku.sales.config;
namespace dokuku.sales.fixture
{
    [Subject("Creating organization")]
    public class When_create_organization
    {
        private static IOrganizationRepository orgRepo;
        private static Organization org;
        private static Guid id;
        static MongoConfig mongo;
        Establish context = () =>
            {
                mongo = new MongoConfig();
                orgRepo = new OrganizationRepository(mongo);
                id = Guid.NewGuid();
            };

        Because of = () =>
            {
                orgRepo.Save(new Organization(id, "oetawan@inforsys.co.id", "Inforsys Indonesia, PT", "IDR", 1));
            };

        It should_create_organization = () =>
            {
                org = orgRepo.Get(id);
                org.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                orgRepo.Delete(org._id);
            };
    }
}