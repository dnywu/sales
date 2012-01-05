using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.organization
{
    public interface IOrganizationRepository
    {
        void Save(Organization org);
        void Delete(Guid id);
        Organization Get(Guid id);
        Organization FindByOwnerId(string email);
    }
}