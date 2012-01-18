using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public class InvoiceAutoNumberConfig
    {
        public InvoiceAutoNumberConfig(string id, AutoNumberMode mode, string prefix, string companyId)
        {
            this._id = id;
            this.Mode = mode;
            this.Prefix = prefix;
            this.CompanyId = companyId;
        }
        public string _id { get; set; }
        public AutoNumberMode Mode { get; private set; }
        public string Prefix { get; private set; }
        public string CompanyId { get; private set; }
    }

    public enum AutoNumberMode
    {
        Default,
        Yearly,
        Monthly
    }
}