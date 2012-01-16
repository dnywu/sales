using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.organization.model;
namespace dokuku.sales.organization.report
{
    public interface IOrganizationReportRepository
    {
        Organization FindByOwnerId(string email);
    }
}