using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.item.service
{
    public class InsertItemService:IInsertItemService
    {
        IItemCommand cmd;
        IItemQuery qry;
        Item itm;
        public InsertItemService(IItemCommand command, IItemQuery query)
        {
            cmd = command;
            qry = query;

        }
        public void Insert(Item item)
        {
            itm = item;
            FailIfBarcodeAlreadyExist();
            FailIfCodeAlreadyExist();
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
