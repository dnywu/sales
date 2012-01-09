using Nancy;
using Nancy.Security;
using Nancy.ViewEngines.Razor;
using System.Dynamic;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using Common.Logging;
using dokuku.sales.web.models;
using dokuku.sales.organization;
using dokuku.sales.item;
namespace dokuku.sales.web.modules
{
    public class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            this.RequiresAuthentication(); 
            IOrganizationRepository orgRepo = new OrganizationRepository();
            IItemRepository itemRepo = new ItemRepository();
            Get["/"] = p =>
                {
                    return View["webclient/sales/index"];
                };
            Get["/getorganization"] = p =>
                {
                    
                    return Response.AsJson(orgRepo.FindByOwnerId(this.Context.CurrentUser.UserName));
                };
            Get["/getuser"] = p =>
                {
                    return Response.AsJson(this.Context.CurrentUser.UserName);
                };
            Post["/setuporganization"] = p =>
                {
                    try
                    {
                        string name = (string)this.Request.Form.name;
                        string timezone = (string)this.Request.Form.timezone;
                        string curr = (string)this.Request.Form.curr;
                        int starts = (int)this.Request.Form.starts;
                        Guid id = Guid.NewGuid();
                        string owner = this.Context.CurrentUser.UserName;
                        orgRepo.Save(new Organization(id, owner, name, curr, starts));
                    }
                    catch (Exception ex)
                    {
                        return Response.AsRedirect("/?error=true&message=" + ex.Message);
                    }
                    return Response.AsRedirect("/");
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
                        itemRepo.Save(new Item()
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
                    return Response.AsRedirect("/");
                };
        }
    }
}