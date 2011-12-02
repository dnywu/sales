using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.organization
{
    public interface IOrganizationRepository
    {
        Organization Create(Organization org);
        void Delete(Organization org);
    }
}