using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.IO;
using dokuku.sales.organization.model;
using dokuku.sales.config;
using dokuku.sales.organization.repository;
using dokuku.sales.organization.report;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace dokuku.sales.organization.fixture
{
    public class When_saving_logo_organization_to_mongo
    {
        const string Default_Logo_Path = @"..\..\..\dokuku.sales.web\webclient\sales\controllers\nav\images\uknown-person.gif";
        static LogoOrganization logo;
        static ILogoOrganizationCommand logoOrganizationCmd;
        static ILogoOrganizationQuery logoOrganizationQry;
        static MongoConfig mongoConfig;
        const string OWNERID = "akusendiri";
        static BitmapImage image;
        static Image gif;

        Establish init = () => 
        {
            mongoConfig = new MongoConfig();
            logoOrganizationCmd = new LogoOrganizationCommand(mongoConfig);
            logoOrganizationQry = new LogoOrganizationQuery(mongoConfig);
        };

        Because of = () => 
        {
            logo = new LogoOrganization {_id = OWNERID, ImageData = ReadFile(Default_Logo_Path), OwnerId = OWNERID };
        };

        It should_be_save_on_mongo = () =>
            {
                logoOrganizationCmd.Save(logo);
                logo = logoOrganizationQry.GetLogo(OWNERID);
                logo.ShouldNotBeNull();
                image = ImageFromBuffer(logo.ImageData);
                gif = Bitmap.FromStream(new MemoryStream(logo.ImageData));
            };

        Cleanup collection = () => 
        {
            logoOrganizationCmd.Delete(OWNERID);
        };

        static BitmapImage ImageFromBuffer(Byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
        static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
    }
}
