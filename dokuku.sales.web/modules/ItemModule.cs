using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.item;

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
                    this.ItemCommand().Delete(id);
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
                    string itemName = (string)this.Request.Form.itemName;
                    string itemDesc = (string)this.Request.Form.description;
                    decimal itemPrice = (decimal)this.Request.Form.itemPrice;
                    string taxName = (string)this.Request.Form.tax;
                    decimal taxValue = 0;
                    Guid id = Guid.NewGuid();
                    string owner = this.Context.CurrentUser.UserName;
                    if (taxName == "PPn")
                    {
                        taxValue = 0.1m;
                    }
                    this.ItemCommand().Save(new Item()
                    {
                        _id = id,
                        OwnerId = owner,
                        Name = itemName,
                        Description = itemDesc,
                        Rate = itemPrice,
                        Tax = new Tax() { Name = taxName, Value = taxValue }
                    }
                    );
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect("/?error=true&message=" + ex.Message);
                }
                return Response.AsJson("OK");
            };
            Get["/Items/_id/{id}"] = p =>
            {
                Guid id = p.id;
                var item = this.ItemQuery().Get(id);
                return Response.AsJson(item);
            };
        }
    }
}