using System;
using System.Collections.Generic;
using dokuku.sales.group.model;
namespace dokuku.sales.group.query
{
    public interface IGroupQuery
    {
        Group Get(Guid id, string ownerId);
        Group FindByName(string name, string ownerId);
        Group FindByNameAndId(string name, Guid id, string ownerId);
        Group [] FindAll(string ownerId);
    }
}
