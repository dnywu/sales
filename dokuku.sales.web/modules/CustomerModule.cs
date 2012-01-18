using System;
using Nancy;
using Nancy.Security;
using dokuku.security.model;
using dokuku.sales.customer.model;
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
                return Response.AsJson(this.CustomerReportRepository().GetByCustName(account.OwnerId, custName));
            };
            Post["/customer"] = p =>
            {
                try
                {
                    this.CustomerRepository().Save(new Customer()
                    {
                        Name = (string)this.Request.Form.CustomerName,
                        Currency = (string)this.Request.Form.CustomerCcy,
                        Term = (string)this.Request.Form.term,
                        BillingAddress = (string)this.Request.Form.billingAddress,
                        City = (string)this.Request.Form.city,
                        Province = (string)this.Request.Form.province,
                        PostalCode = (string)this.Request.Form.Zip,
                        State = (string)this.Request.Form.country,
                        Fax = (string)this.Request.Form.fax,
                        ShipmentAddress = (string)this.Request.Form.shipmentAddress,
                        ShipmentCity = (string)this.Request.Form.shipmentCity,
                        ShipmentCountry = (string)this.Request.Form.shipmentCountry,
                        ShipmentFax = (string)this.Request.Form.shipmentFax,
                        ShipmentStateProvince = (string)this.Request.Form.shipmentStateProvince,
                        ShipmentZIPPostalCode = (string)this.Request.Form.shipmentZIPPostalCode,
                        FirstName =  (string)this.Request.Form.firstName,
                        LastName = (string)this.Request.Form.lastName,
                        Salutation = (string)this.Request.Form.salutation,
                        Phone = (string)this.Request.Form.telephone,
                        MobilePhone = (string)this.Request.Form.cellular,
                        Email = (string)this.Request.Form.email,
                        AddFieldCustID1 = (string)this.Request.Form.add_fieldCustID1,
                        AddFieldCustID2 = (string)this.Request.Form.add_fieldCustID2,
                        AddFieldCustID3 = (string)this.Request.Form.add_fieldCustID3,
                        AddValueCustID1 = (string)this.Request.Form.add_valueCustID1,
                        AddValueCustID2 = (string)this.Request.Form.add_valueCustID2,
                        AddValueCustID3 = (string)this.Request.Form.add_valueCustID3,
                        _id = Guid.NewGuid(),
                        OwnerId = this.Context.CurrentUser.UserName
                    });
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