using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
namespace dokuku.sales.fixture
{
    [Subject("Creating organization")]
    public class When_create_organization
    {
        private static IOrganizationRepository orgRepo;
        private static Organization org;

        Establish context = () =>
            {
                orgRepo = new OrganizationRepository(); 
            };

        Because of = () =>
            {
                org = orgRepo.Create(new Organization("oetawan@inforsys.co.id", "Inforsys Indonesia, PT", "IDR", 1));
            };

        It should_create_organization = () =>
            {
                org.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                orgRepo.Delete(org);
            };
    }
}