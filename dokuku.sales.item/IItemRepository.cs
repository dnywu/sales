using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.item
{
    public interface IItemRepository
    {
        void Save(Item item);
        Item Get(Guid id);
        void Delete(Guid id);
        IEnumerable<Item> AllItems();
    }
}