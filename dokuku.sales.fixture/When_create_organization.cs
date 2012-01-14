using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.model;
namespace dokuku.sales.fixture
{
    [Subject("Creating organization")]
    public class When_create_organization
    {
        private static IOrganizationRepository orgRepo;
        private static Organization org;
        private static Guid id;

        Establish context = () =>
            {
                orgRepo = new OrganizationRepository();
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