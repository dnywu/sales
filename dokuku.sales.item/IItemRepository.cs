using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.item
{
    public interface IItemRepository
    {
        void Save(Item item);
        void Update(Item item);
        Item Get(Guid id);
        void Delete(Guid id);
        int CountItems(string ownerId);
        IEnumerable<Item> LimitItems(string ownerId, int start, int limit);
        IEnumerable<Item> AllItems(string companyId);
    }
}