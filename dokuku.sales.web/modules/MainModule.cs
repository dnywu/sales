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
using dokuku.sales.customer;
using dokuku.sales.item;
using dokuku.security;
using Newtonsoft.Json;
namespace dokuku.sales.web.modules
{
    public class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            this.RequiresAuthentication(); 
            IOrganizationRepository orgRepo = new OrganizationRepository();
            ICustomerRepository cusRepo = new CustomerRepository();
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
                        cusRepo.Save(new Customer()
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
                            OwnerId = this.Context.CurrentUser.UserName
                        });
                    }
                    catch (Exception ex)
                    {
                        return Response.AsRedirect("/?error=true&message=" + ex.Message);
                    }
                    return Response.AsJson("OK");
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
                return Response.AsJson("OK");
            };
            Get["/Items"] = p =>
            {
                dokuku.security.AuthRepository.AccountUser userId = AuthRepository.GetAccountByUsername(this.Context.CurrentUser.UserName);
                return Response.AsJson(itemRepo.CountItems(userId.CompanyId));
            };
            Get["/Customers"] = p =>
                {
                    dokuku.security.AuthRepository.AccountUser userId = AuthRepository.GetAccountByUsername(this.Context.CurrentUser.UserName);
                    return Response.AsJson(cusRepo.CountCustomers(userId.CompanyId));
                };
            Get["/LimitCustomers/start/{start}/limit/{limit}"] = p =>
                {
                    int start = p.start;
                    int limit = p.limit;
                    dokuku.security.AuthRepository.AccountUser userId = AuthRepository.GetAccountByUsername(this.Context.CurrentUser.UserName);
                    return Response.AsJson(cusRepo.LimitCustomers(userId.CompanyId, start,limit));
                };
            Get["/LimitItems/start/{start}/limit/{limit}"] = p =>
                {
                    int start = p.start;
                    int limit = p.limit;
                    dokuku.security.AuthRepository.AccountUser userId = AuthRepository.GetAccountByUsername(this.Context.CurrentUser.UserName);
                    return Response.AsJson(itemRepo.LimitItems(userId.CompanyId, start, limit));
                };
        }
    }
}