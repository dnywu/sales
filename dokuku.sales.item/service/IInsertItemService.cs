using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.item.service
{
    public interface IInsertItemService
    {
        void Insert(Item item);

        void Update(Item item);
    }
}
