using System;
using dokuku.sales.organization.model;
namespace dokuku.sales.organization.repository
{
    public interface ILogoOrganizationCommand
    {
        void Save(LogoOrganization logo);
        void Delete(string id);
    }
}
