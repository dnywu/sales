using System;
using System.Collections.Generic;
using dokuku.sales.group.model;
namespace dokuku.sales.group.service
{
    public interface IGroupService
    {
        Group Insert(string json,string ownerId);
        Group Update(string json, string ownerId);
        void Delete(Guid id);
    }
}
