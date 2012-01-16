using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.item
{
    public interface IItemCommand
    {
        void Save(Item item);
        void Delete(Guid id);
    }
}