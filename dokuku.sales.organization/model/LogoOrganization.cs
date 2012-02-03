using System;

namespace dokuku.sales.organization.model
{
    public class LogoOrganization
    {
        public string _id { get; set; }
        public string OwnerId { get; set; }
        public byte[] ImageData { get; set; }
    }
}
