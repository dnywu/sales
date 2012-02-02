using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using dokuku.sales.organization.model;
using dokuku.security.model;
using Nancy;
using Nancy.Security;
using Newtonsoft.Json;
using System.Drawing;
namespace dokuku.sales.web.modules
{
    public class OrganizationModule : Nancy.NancyModule
    {
        public OrganizationModule()
        {
            this.RequiresClaims(new string[1] { Account.OWNER });

            Post["/setuporganization"] = p =>
            {
                try
                {
                    string name = (string)this.Request.Form.name;
                    string timezone = (string)this.Request.Form.timezone;
                    string curr = (string)this.Request.Form.curr;
                    int starts = (int)this.Request.Form.starts;
                    Account acc = this.CurrentAccount();
                    this.OrganizationRepository().Save(new Organization()
                    {
                        _id = acc.OwnerId,
                        OwnerId = acc.OwnerId,
                        Name = name,
                        Currency = curr,
                        FiscalYearPeriod = starts
                    });
                }
                catch (Exception ex)
                {
                    return Response.AsRedirect("/?error=true&message=" + ex.Message);
                }
                return Response.AsRedirect("/");
            };

            Post["/settingorganization"] = p =>
                {
                    try
                    {
                        var test = this.Request.Form.Organization;
                        Organization org = JsonConvert.DeserializeObject<Organization>(this.Request.Form.Organization);
                        org._id = this.CurrentAccount().OwnerId;
                        this.OrganizationRepository().Save(org);
                    }
                    catch (Exception ex)
                    {
                        return Response.AsJson(new { error = false, message = ex.Message });
                    }
                    return Response.AsJson("success");
                };

            Post["/uploadlogoorg"] = p =>
            {
                Stream stream = this.Request.Files.FirstOrDefault().Value;
                byte[] logoData = ReadFile(stream);
                long size = logoData.LongLength / 1024;
                if (size > 1024)
                {
                    return Response.AsRedirect("/?error=true");
                }
                LogoOrganization logoOrganization = new LogoOrganization { _id = this.CurrentAccount().OwnerId, ImageData = logoData, OwnerId = this.CurrentAccount().OwnerId };
                this.LogoOrganizationCommand().Save(logoOrganization);
                return Response.AsRedirect("/uploadlogo");
            };

            Get["/logoOrganization"] = p =>
            {
                LogoOrganization logo = this.LogoOrganizationQuery().GetLogo(this.CurrentAccount().OwnerId);
                if (logo == null)
                    return null;
                MemoryStream stream = new MemoryStream(logo.ImageData);
                return Response.FromStream(stream, "image/png");
            };
        }

        byte[] ReadFile(Stream stream)
        {
            byte[] buffer;
            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)stream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = stream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                stream.Close();
            }
            return buffer;
        }
    }
}