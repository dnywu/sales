using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.item;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using dokuku.sales.item.model;

namespace dokuku.sales.web.modules
{
    public class ItemModule : Nancy.NancyModule
    {
        public ItemModule()
        {
            this.RequiresAuthentication();
            Get["/Items"] = p =>
            {
                return Response.AsJson(this.ItemQuery().AllItems(this.CurrentAccount().OwnerId));
            };
            Get["/CountItem"] = p =>
            {
                return Response.AsJson(this.ItemQuery().CountItems(this.CurrentAccount().OwnerId));
            };

            Get["/LimitItems/start/{start}/limit/{limit}"] = p =>
            {
                int start = p.start;
                int limit = p.limit;
                return Response.AsJson(this.ItemQuery().LimitItems(this.CurrentAccount().OwnerId, start, limit));
            };

            Delete["/deleteItem/{id}"] = p =>
            {
                try
                {
                    Guid id = p.id;
                    this.ItemService().Delete(id);
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect("/?error=true&message=" + ex.Message);
                }
                return Response.AsJson("OK");
            };
            Get["/getItemByName/{itemName}"] = p =>
            {
                string itemName = p.itemName.ToString();
                var item = this.ItemQuery().GetItemByName(this.CurrentAccount().OwnerId, itemName);
                return Response.AsJson(item);
            };
            Post["/createnewitem"] = p =>
            {
                try
                {
                    string data = this.Request.Form.data;
                    Item item = this.ItemService().Insert(data, this.CurrentAccount().OwnerId);
                    return Response.AsJson(item);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Get["/Items/{id}"] = p =>
            {
                Guid id = p.id;
                var item = this.ItemQuery().Get(id);
                return Response.AsJson(item);
            };
            Post["/editItem"] = p =>
            {
                try
                {
                    Item item = this.ItemService().Update(this.Request.Form.data, this.CurrentAccount().OwnerId);
                    return Response.AsJson(item);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
                
            };
            Get["/isCodeIsExist/{code}"] = p =>
            {
                string code = p.code;
                return Response.AsJson(this.ItemQuery().IsCodeAlreadyExist(code, this.CurrentAccount().OwnerId));
            };
            Get["/isBarcodeIsExist/{barcode}"] = p =>
            {
                string barcode = p.barcode;                
                return Response.AsJson(this.ItemQuery().IsBarcodeAlreadyExist(barcode, this.CurrentAccount().OwnerId));
            };
            Get["/searchItem/{keyword}"] = p =>
            {
                string keyWords = p.keyword;
                IList<Item> items=new List<Item>();
                IEnumerable<ItemReports> itemReports = this.ItemQuery().Search(this.CurrentAccount().OwnerId, new string[] { keyWords });
                foreach (ItemReports item in itemReports)
                {
                    items.Add(this.ItemQuery().Get(item._id));
                }
                return Response.AsJson(items);
            };
        }
    }
}