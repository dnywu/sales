using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.organization.model;
namespace dokuku.sales.organization.repository
{
    public interface IOrganizationRepository
    {
        void Save(Organization org);
        void Delete(Guid id);
        Organization Get(Guid id);
        Organization FindByOwnerId(string email);
    }
}