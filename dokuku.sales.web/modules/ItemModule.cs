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
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                return Response.AsJson(this.ItemQuery().AllItems(account.OwnerId));
            };
            Get["/CountItem"] = p =>
            {
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                return Response.AsJson(this.ItemQuery().CountItems(account.OwnerId));
            };

            Get["/LimitItems/start/{start}/limit/{limit}"] = p =>
            {
                int start = p.start;
                int limit = p.limit;
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                return Response.AsJson(this.ItemQuery().LimitItems(account.OwnerId, start, limit));
            };

            Delete["/deleteItem/_id/{id}"] = p =>
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
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                string itemName = p.itemName.ToString();
                var item = this.ItemQuery().GetItemByName(account.OwnerId, itemName);
                return Response.AsJson(item);
            };
            Post["/createnewitem"] = p =>
            {
                try
                {
                    string data = this.Request.Form.data;
                    Item item = this.ItemService().Insert(data, this.Context.CurrentUser.UserName);
                    return Response.AsJson(item);
                }
                catch (Exception ex)
                {
                    return Response.AsJson(new { error = true, message = ex.Message });
                }
            };
            Get["/Items/_id/{id}"] = p =>
            {
                Guid id = p.id;
                var item = this.ItemQuery().Get(id);
                return Response.AsJson(item);
            };
            Post["/editItem"] = p =>
            {
                try
                {
                    Item item = this.ItemService().Update(this.Request.Form.data, this.Context.CurrentUser.UserName);
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
                string owner = this.Context.CurrentUser.UserName;
                return Response.AsJson(this.ItemQuery().IsCodeAlreadyExist(code, owner));
            };
            Get["/isBarcodeIsExist/{barcode}"] = p =>
            {
                string barcode = p.barcode;
                string owner = this.Context.CurrentUser.UserName;
                return Response.AsJson(this.ItemQuery().IsBarcodeAlreadyExist(barcode, owner));
            };
            Get["/searchItem/keyword/{keyword}"] = p =>
            {
                string keyWords = p.keyword;
                string owner = this.Context.CurrentUser.UserName;
                IList<Item> items=new List<Item>();
                IEnumerable<ItemReports> itemReports = this.ItemQuery().Search(owner, new string[] { keyWords });
                foreach (ItemReports item in itemReports)
                {
                    items.Add(this.ItemQuery().Get(item._id));
                }
                return Response.AsJson(items);
            };
        }
    }
}