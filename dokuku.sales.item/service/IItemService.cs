using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.item.service
{
    public interface IItemService
    {
        Item Insert(string item, string ownerId);
        Item Update(string item, string ownerId);
        void Delete(Guid id);
    }
}
