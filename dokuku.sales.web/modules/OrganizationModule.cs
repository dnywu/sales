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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
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
                    Image img = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\images\default-logo-organization.png"));
                    MemoryStream imgStream = new MemoryStream();
                    img.Save(imgStream, ImageFormat.Png);
                    byte[] logoData = readImageAndCompress(imgStream);

                    LogoOrganization logoOrganization = new LogoOrganization { _id = this.CurrentAccount().OwnerId, ImageData = logoData, OwnerId = this.CurrentAccount().OwnerId };
                    this.LogoOrganizationCommand().Save(logoOrganization);
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
                byte[] logoData = readImageAndCompress(stream);
                
                LogoOrganization logoOrganization = new LogoOrganization { _id = this.CurrentAccount().OwnerId, ImageData = logoData, OwnerId = this.CurrentAccount().OwnerId };
                this.LogoOrganizationCommand().Save(logoOrganization);
                return Response.AsRedirect("/uploadlogo");
            };

            Get["/logoOrganization"] = p =>
            {
                LogoOrganization logo = this.LogoOrganizationQuery().GetLogo(this.CurrentAccount().OwnerId);
                if (logo == null)
                    return null;
                MemoryStream stream = new MemoryStream(Zip7.Decompress(logo.ImageData));
                return Response.FromStream(stream, "image/png");
            };
        }

        byte[] readImageAndCompress(Stream stream)
        {
            var image = Image.FromStream(stream);
            image = resizeImage(image, new Size(140, 60));
            MemoryStream imgStream = new MemoryStream();
            image.Save(imgStream, ImageFormat.Png);
            var comppressedImg = Zip7.Compress(imgStream.ToArray());
            return comppressedImg;
        }

        private static byte[] readStream(Stream stream)
        {
            byte[] buffer;
            try
            {
                int length = (int)stream.Length; 
                buffer = new byte[length];
                int count;
                int sum = 0;

                while ((count = stream.Read(buffer, sum, length - sum)) > 0)
                {
                    sum += count;
                }
            }
            finally
            {
                stream.Close();
            }
            return buffer;
        }
        static byte[] cropImageFile(byte[] imageFile, int targetW, int targetH, int targetX, int targetY)
        {
            Image imgPhoto = Image.FromStream(new MemoryStream(imageFile));
            Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), targetX, targetY, targetW, targetH, GraphicsUnit.Pixel);

            MemoryStream mm = new MemoryStream();
            bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Png);
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            grPhoto.Dispose();

            return mm.GetBuffer();
        }
        static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
    }
}