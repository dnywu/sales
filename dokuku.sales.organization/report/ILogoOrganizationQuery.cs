using System;
using dokuku.sales.organization.model;
namespace dokuku.sales.organization.report
{
    public interface ILogoOrganizationQuery
    {
        LogoOrganization GetLogo(string id);
    }
}
