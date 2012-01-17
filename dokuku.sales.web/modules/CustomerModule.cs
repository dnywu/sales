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
                string CustomerName = (string)this.Request.Form.CustomerName;
                string CustomerCcy = (string)this.Request.Form.CustomerCcy;
                string Term = (string)this.Request.Form.term;
                string BillingAddress = (string)this.Request.Form.billingAddress;
                string City = (string)this.Request.Form.city;
                string Province = (string)this.Request.Form.province;
                string zip = (string)this.Request.Form.Zip;
                string Country = (string)this.Request.Form.country;
                string Fax = (string)this.Request.Form.fax;
                string ShipmentAddress = (string)this.Request.Form.shipmentAddress;
                string ShipmentCity = (string)this.Request.Form.shipmentCity;
                string ShipmentStateProvince = (string)this.Request.Form.shipmentStateProvince;
                string ShipmentZIPPostalCode = (string)this.Request.Form.shipmentZIPPostalCode;
                string ShipmentCountry = (string)this.Request.Form.shipmentCountry;
                string ShipmentFax = (string)this.Request.Form.shipmentFax;
                string Salutation = (string)this.Request.Form.salutation;
                string FirstName = (string)this.Request.Form.firstName;
                string LastName = (string)this.Request.Form.lastName;
                string Cellular = (string)this.Request.Form.cellular;
                string Telephone = (string)this.Request.Form.telephone;
                string Email = (string)this.Request.Form.email;
                string AddFieldCustID1 = (string)this.Request.Form.add_fieldCustID1;
                string AddValueCustID1 = (string)this.Request.Form.add_valueCustID1;
                string AddFieldCustID2 = (string)this.Request.Form.add_fieldCustID2;
                string AddValueCustID2 = (string)this.Request.Form.add_valueCustID2;
                string AddFieldCustID3 = (string)this.Request.Form.add_fieldCustID3;
                string AddValueCustID3 = (string)this.Request.Form.add_valueCustID3;
                try
                {
                    Account account = this.AccountRepository().FindAccountByName(this.Context.CurrentUser.UserName);
                    this.CustomerRepository().Save(new Customer()
                    {
                        Name = CustomerName,
                        Currency = CustomerCcy,
                        Term = Term,
                        BillingAddress = BillingAddress,
                        City = City,
                        Province = Province,
                        PostalCode = zip,
                        State = Country,
                        Fax = Fax,
                        ShipmentAddress = ShipmentAddress,
                        ShipmentCity = ShipmentCity,
                        ShipmentCountry = ShipmentCountry,
                        ShipmentFax = ShipmentFax,
                        ShipmentStateProvince = ShipmentStateProvince,
                        ShipmentZIPPostalCode = ShipmentZIPPostalCode,
                        FirstName = FirstName,
                        LastName = LastName,
                        Salutation = Salutation,
                        Phone = Telephone,
                        MobilePhone = Cellular,
                        Email = Email,
                        AddFieldCustID1 = AddFieldCustID1,
                        AddFieldCustID2 = AddFieldCustID2,
                        AddFieldCustID3 = AddFieldCustID3,
                        AddValueCustID1 = AddValueCustID1,
                        AddValueCustID2 = AddValueCustID2,
                        AddValueCustID3 = AddValueCustID3,
                        _id = Guid.NewGuid(),
                        OwnerId = account.OwnerId
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