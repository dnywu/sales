using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
using Newtonsoft.Json;
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
                return Response.AsJson(this.CustomerReportRepository().CountCustomers(account.OwnerId));
            };
            Get["/LimitCustomers/start/{start}/limit/{limit}"] = p =>
            {
                int start = p.start;
                int limit = p.limit;
                Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                return Response.AsJson(this.CustomerReportRepository().LimitCustomers(account.OwnerId, start, limit));
            };
            Delete["/DeleteCustomer/id/{id}"] = p =>
            {
                try
                {
                    Guid id = p.id;
                    this.CustomerRepository().Delete(id);
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
                Customer customer = JsonConvert.DeserializeObject<Customer>(data);
                try
                {
                    customer._id = Guid.NewGuid();
                    customer.OwnerId = this.Context.CurrentUser.UserName;
                    this.CustomerRepository().Save(customer);
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect(ex.Message);
                }
                return Response.AsJson("OK");
            };
            Get["/GetDataCustomer/id/{id}"] = p =>
            {
                Guid id = p.id;
                return Response.AsJson(this.CustomerReportRepository().GetCustomerById(id));
            };
            Post["/UpdateDataCustomer/data"] = p =>
                {
                    string Data = this.Request.Form.data;
                    Customer item = JsonConvert.DeserializeObject<Customer>(Data);
                    try
                    {
                        this.CustomerReportRepository().UpdateCustomer(item);
                    }
                    catch (Exception ex)
                    {

                        return Response.AsRedirect("/?error=true&message=" + ex.Message);
                    }
                    return Response.AsJson("OK");
                };
        }
    }
}