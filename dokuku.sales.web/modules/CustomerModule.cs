using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
using Newtonsoft.Json;
using System.Linq;
using dokuku.sales.item;
namespace dokuku.sales.web.modules
{
    public class CustomerModule : Nancy.NancyModule
    {
        public CustomerModule()
        {
            this.RequiresAuthentication();
            Get["/Customers"] = p =>
            {
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                int count = this.CustomerReportRepository().CountCustomers(account.OwnerId);
                return Response.AsJson(count);
            };
            Get["/LimitCustomers/start/{start}/limit/{limit}"] = p =>
            {
                int start = p.start;
                int limit = p.limit;
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                Customer[] result = this.CustomerReportRepository().LimitCustomers(account.OwnerId, start, limit).ToArray();
                return Response.AsJson(result);
            };
            Delete["/DeleteCustomer/id/{id}"] = p =>
            {
                try
                {
                    Guid id = p.id;
                    this.CustomerService().DeleteCustomer(id);
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect("/?error=true&message=" + ex.Message);
                }
                return Response.AsJson("OK");
            };
            Get["/getCustomerByCustomerName/{custName}"] = p =>
            {
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                string custName = p.custName.ToString();
                var a = this.CustomerReportRepository().GetByCustName(account.OwnerId, custName);
                return Response.AsJson(a);
            };
            Post["/customer/data"] = p =>
            {
                string data = this.Request.Form.data;
                try
                {
                    Customer cust = this.CustomerService().SaveCustomer(data,this.Context.CurrentUser.UserName);
                    return Response.AsJson(cust);
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect(ex.Message);
                }
            };
            Get["/GetDataCustomer/id/{id}"] = p =>
            {
                Guid id = p.id;
                return Response.AsJson(this.CustomerReportRepository().GetCustomerById(id));
            };
            Post["/UpdateDataCustomer/data"] = p =>
                {
                    string Data = this.Request.Form.data;
                    try
                    {
                        this.CustomerService().UpdateCustomer(Data);
                    }
                    catch (Exception ex)
                    {

                        return Response.AsRedirect("/?error=true&message=" + ex.Message);
                    }
                    return Response.AsJson("OK");
                };
            Get["/SearchCustomer/key/{key}"] = p =>
                {
                    string key = p.key;
                    Account ownerId = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                    return Response.AsJson(this.CustomerReportRepository().Search(ownerId.OwnerId,new string[]{key}));
                };
        }
    }
}