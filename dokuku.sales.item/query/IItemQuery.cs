using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.item
{
    public interface IItemQuery
    {
        Item Get(Guid id);
        long CountItems(string ownerId);
        IEnumerable<Item> LimitItems(string ownerId, int start, int limit);
        IEnumerable<Item> AllItems(string companyId);
        Item GetItemByName(string ownerId, string itemName);
        IEnumerable<Item> Search(string ownerId, String[] keywords);
    }
}