using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace dokuku.sales.invoices.model
{
    public static class DateFormatter
    {
        public static string AsString(this DateTime d)
        {
            return d.ToString("dd MMM yyyy");
        }
    }
}