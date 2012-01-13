﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using dokuku.sales.organization;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.model;
using dokuku.sales.organization.report;
namespace dokuku.sales.fixture
{
    [Subject("Find organization by owner id")]
    public class When_find_organization_by_owner_id
    {
        private static IOrganizationRepository orgRepo;
        private static IOrganizationReportRepository orgReportRepo;
        private static Organization org;
        private static Guid id;

        Establish context = () =>
            {
                orgRepo = new OrganizationRepository();
                orgReportRepo = new OrganizationReportRepository();
                id = Guid.NewGuid();
            };

        Because of = () =>
            {
                orgRepo.Save(new Organization(id, "oetawan@inforsys.co.id", "Inforsys Indonesia, PT", "IDR", 1));
            };

        It should_return_organization = () =>
            {
                Organization org = orgReportRepo.FindByOwnerId("oetawan@inforsys.co.id");
                org.ShouldNotBeNull();
            };

        Cleanup cleanup = () =>
            {
                orgRepo.Delete(id);
            };
    }
}