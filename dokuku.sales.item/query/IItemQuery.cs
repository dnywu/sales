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
        Item FindByBarcode(string barcode, string owner);
        Item FindByCode(string code, string owner);
        IEnumerable<Item> Search(string ownerId, String[] keywords);
        bool IsCodeAlreadyExist(string code, string owner);
        bool IsBarcodeAlreadyExist(string barcode, string owner);
    }
}