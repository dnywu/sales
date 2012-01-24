using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.item.messages;
using MongoDB.Bson;
using Newtonsoft.Json;
using NServiceBus;
namespace dokuku.sales.item.service
{
    public class ItemService:IItemService
    {
        IItemCommand cmd;
        IItemQuery qry;
        IBus bus;
        public ItemService(IItemCommand command, IItemQuery query, IBus bus)
        {
            cmd = command;
            qry = query;
            this.bus = bus;
        }
        public Item Insert(string jsonItem, string ownerId)
        {
            Item item = JsonConvert.DeserializeObject<Item>(jsonItem);
            item.OwnerId = ownerId;
            item._id = Guid.NewGuid();
            FailIfBarcodeAlreadyExist(item);
            FailIfCodeAlreadyExist(item);

            cmd.Save(item);
            bus.Publish<ItemCreated>(new ItemCreated { Data = item.ToJson() });

            return item;
        }

        public Item Update(string jsonItem, string ownerId)
        {
            Item item = JsonConvert.DeserializeObject<Item>(jsonItem);
            Item self = qry.Get(item._id);
            if (self.Code != item.Code || self.Barcode != item.Barcode)
            {
                FailIfBarcodeAlreadyExist(item);
                FailIfCodeAlreadyExist(item);
            }
            item.OwnerId = ownerId;
            cmd.Update(item);
            bus.Publish(new ItemUpdated { Data = item.ToJson() });
            return item;
        }

        public void Delete(Guid id)
        {
            cmd.Delete(id);
            bus.Publish<ItemDeleted>(new ItemDeleted { Id = id });
        }

        private void FailIfCodeAlreadyExist(Item itm)
        {
            if (qry.FindByBarcode(itm.Barcode, itm.OwnerId)!=null)
            {
                throw new Exception(string.Format("Barang dengan barcode {0} sudah ada", itm.Barcode));
            }
        }

        private void FailIfBarcodeAlreadyExist(Item itm)
        {
            var t = qry.FindByCode(itm.Code, itm.OwnerId);
            if (qry.FindByCode(itm.Code, itm.OwnerId) != null)
            {
                throw new Exception(string.Format("Barang dengan kode {0} sudah ada", itm.Code));
            }
        }
    }
}
