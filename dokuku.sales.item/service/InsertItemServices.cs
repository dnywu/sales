using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using dokuku.sales.item.messages;
namespace dokuku.sales.item.service
{
    public class InsertItemService:IInsertItemService
    {
        IItemCommand cmd;
        IItemQuery qry;
        Item itm;
        IBus bus;
        public InsertItemService(IItemCommand command, IItemQuery query, IBus bus)
        {
            cmd = command;
            qry = query;
            this.bus = bus;
        }
        public void Insert(Item item)
        {
            itm = item;
            FailIfBarcodeAlreadyExist();
            FailIfCodeAlreadyExist();
            cmd.Save(item);
            bus.Publish(new ItemCreated { Id = item._id });
        }

        public void Update(Item item)
        {
            itm = item;
            Item self = qry.Get(itm._id);
            if (self.Code != itm.Code || self.Barcode != itm.Barcode)
            {
                FailIfBarcodeAlreadyExist();
                FailIfCodeAlreadyExist();
            }
            cmd.Save(item);
        }

        private void FailIfCodeAlreadyExist()
        {
            if (qry.FindByBarcode(itm.Barcode, itm.OwnerId)!=null)
            {
                throw new Exception(string.Format("Barang dengan barcode {0} sudah ada", itm.Barcode));
            }
            
        }

        private void FailIfBarcodeAlreadyExist()
        {
            if (qry.FindByCode(itm.Code, itm.OwnerId) != null)
            {
                throw new Exception(string.Format("Barang dengan kode {0} sudah ada", itm.Code));
            }
        }
    }
}
