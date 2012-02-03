using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;
using Antlr3.ST;
using dokuku.sales.customer.model;
using dokuku.sales.organization.model;
using System.Drawing;

namespace dokuku.sales.invoices.viewtemplating
{
    public class DefaultTemplate
    {
        const string TEMPLATE = @"<table width='100%'  border='0' cellspacing='0' cellpadding='0' style='font-size:11px; font-family:Tahoma;'>
	<tbody>
		<tr>
			<td style='padding-left:10px'>
				<div width='100%' style='font-weight:bold; font-size:18px'>$organization.Name$</div>
				<div style='width:65%; font-size:14px'>$organization.Address$</div>
				<div width='100%' style='font-size:14px'>Telp.$organization.Phone$; Fax.$organization.Fax$</div>
			</td>	
			<td style='text-align:center;'> <img style='width:140px;height:100px;float:right;' src='data:image/gif;base64,$logodata$'> </td>
		</tr>
		<tr>
			<td colspan ='2'><hr size='1' color='black'></td>
		</tr>
		<tr>
			<td colspan='2' align='center'><div style='font-weight:bold; font-size:18px; letter-spacing:2px; margin-top: 10px; margin-bottom: 15px;'>FAKTUR</div></td>
		</tr>
		<tr>
			<td width='55%' style='padding-left:10px'>
				<table width='100%' style='font-size:14px; font-family:Tahoma;'>
					<tr valign='top'>
						<td width='20%'>Kepada</td>
						<td align='right'>:</td>
						<td>
							<div>$cutomer.Name$</div>
							<div>$customer.BillingAddress$</div>
							<div>$customer.City$ , Telp. $customer.Phone$</div>
							<div>Fax. $MobilePhone$</div>
						</td>
					</tr>
					<tr><td colspan='3'></td></tr>
					<tr><td colspan='3'></td></tr>
					<tr><td colspan='3'></td></tr>
					<tr>
						<td>Attn</td>
						<td align='right'>:</td>
						<td>$customer.Salutation$ $customer.FirstName$ $customer.LastName$</td>
					</tr>
				</table>
			</td>
			<td width='45%' style='padding-left:30px'>
				<table width='100%' style='font-size:14px; font-family:Tahoma;'>
					<tr>
						<td width='40%'>Nomor Faktur</td>
						<td align='right'>:</td>
						<td width='58%'>$invoices.InvoiceNo$</td>
					</tr>
					<tr>
						<td>Tanggal</td>
						<td align='right'>:</td>
						<td>$InvoiceDate$</td>
					</tr>
					<tr>
						<td>Termin</td>
						<td align='right'>:</td>
						<td>$invoices.Terms.Name$</td>
					</tr>
					<tr>
						<td>Jatuh Tempo</td>
						<td align='right'>:</td>
						<td>$DueDate$</td>
					</tr>
					<tr>
						<td>Mata Uang</td>
						<td align='right'>:</td>
						<td>$invoices.BaseCcy$</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr height='5.69in'>
			<td colspan='2' >
				<table width='100%' height='460px' border='0' style='margin-top:20px; border:dotted 1px #333; border-spacing: 0; font-size:11px; font-family:Tahoma;'>
					<thead>
						<tr height='40px' style='background-color:#ebebeb; font-weight:bold; font-size:13px'>
							<td style='background-color:#FAFAFA; border-right:dotted 1px #333; border-bottom:dotted 1px #333; padding:0px 0px 1px 3px;'>Barang</td>
							<td width='9%' align='center' style='background-color:#FAFAFA; border-right:dotted 1px #333; border-bottom:dotted 1px #333;'>QTY</td>
							<td width='18%' align='right' style='background-color:#FAFAFA; border-right:dotted 1px #333; border-bottom:dotted 1px #333; padding:0px 5px 1px 0;'>@Harga</td>
							<td width='14%' align='right' style='background-color:#FAFAFA; border-right:dotted 1px #333; border-bottom:dotted 1px #333; padding:0px 5px 1px 0;'>Diskon</td>
							<td width='18%' align='right' style='background-color:#FAFAFA; border-bottom:dotted 1px #333; padding:0px 5px 1px 0;'>Total</td>
						</tr>
                        <tr height='5px' border='1'>
							<td colspan='5' style='border-bottom:dotted 1px #333;'></td>
						</tr>
					</thead>
					<tbody>
						$invoices.Items:{
						<tr style='font-size:12px;' height='20px'>
							<td style='border-right:dotted 1px #333; padding:4px 3px;'>
								<div>$it.PartName$</div>
                                <div style='font-style:italic;'>$it.Description$</div>
							</td>
							<td style='border-right:dotted 1px #333; padding:4px 3px;'>
								<div align='center'>$it.QtyString$</div>
							</td>
							<td style='border-right:dotted 1px #333; padding:0px 5px 1px 0;'>
								<div align='right'>$it.RateString$</div>
							</td>
							<td style='border-right:dotted 1px #333; padding:0px 5px 1px 0;'>
								<div align='right'>$it.DiscountString$</div>
							</td>
							<td>
								<div align='right' style='padding:0px 5px 1px 0;'>$it.AmountString$</div>
							</td>
						</tr>
						}$
						<tr>
							<td style='border-right:dotted 1px #333; padding:4px 3px;'>
							</td>
							<td style='border-right:dotted 1px #333; padding:4px 3px;'>
							</td>
							<td style='border-right:dotted 1px #333; padding:4px 3px;'>
							</td>
							<td style='border-right:dotted 1px #333; padding:4px 3px;'>
							</td>
							<td>
							</td>
						</tr>
					</tbody>
					<tfoot>
						<tr>
							<td colspan='4' align='right' style='font-size:13px; border-right:dotted 1px #333; border-top:dotted 1px #333; padding:4px 3px;'>Sub Total</td>
							<td align='right' style='font-size:13px; border-top:dotted 1px #333; padding:4px 3px;'>$SubTotal$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='font-size:13px; border-right:dotted 1px #333; padding:4px 3px;'>Diskon</td>
							<td align='right' style='font-size:13px; padding:4px 3px;'>$totaldiscount$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='font-size:13px; border-right:dotted 1px #333; padding:4px 3px;'>Pajak</td>
							<td align='right' style='font-size:13px; padding:4px 3px;'>$totaltax$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='font-weight:bold; font-size:13px; border-right:dotted 1px #333; padding:4px 3px;'>Total</td>
							<td align='right' style='font-size:13px; font-weight:bold; padding:4px 3px;'>$total$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='font-size:13px; border-right:dotted 1px #333; padding:4px 3px;'>Uang muka/Pembayaran</td>
							<td align='right' style='font-size:13px; padding:4px 3px;'>0</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='font-weight:bold; font-size:13px; border-right:dotted 1px #333; padding:4px 3px;'>Total Bersih</td>
							<td align='right' style='font-weight:bold; font-size:13px; padding:4px 3px;'>$total$</td>
						</tr>
					</tfoot>
				</table>
			</td>
		</tr>
	</tbody>
	<tfoot>
		<tr>
			<table width='100%' style='font-size:12px; font-family:Tahoma; margin-top: 10px; margin-bottom: 20px;'>
				<tr>
					<td width='10%'>Terbilang</td>
					<td>: $terbilang$</td>
				</tr>
				<tr>
					<td>Keterangan</td>
					<td>: $Note$</td>
				</tr>
			</table>
		</tr>
		<tr>
			<table width='100%' style='border : dotted 1px #333; font-size:12px; font-family:Tahoma;'>
				<tr height='80px'>
					<td width='31%' style='border-right:dotted 1px #333' valign='top'><div>Disiapkan Oleh</div></td>
					<td width='31%' style='border-right:dotted 1px #333' valign='top'><div>Diperiksa Oleh</div></td>
					<td width='31%' valign='top'><div>Disetujui Oleh</div></td>
				</tr>
				<tr>
					<td style='border-right:dotted 1px #333'>
						<div valign='top'>(.........................)</div>
						<div valign='top'>Administrasi</div>
					</td>
					<td style='border-right:dotted 1px #333'>
						<div valign='top'>(.........................)</div>
						<div valign='top'>Supervisor</div>
					</td>					
					<td>
						<div valign='top'>(.........................)</div>
						<div valign='top'>Finance & Accounting Manager</div>
					</td>					
				</tr>
			</table>
		</tr>
	</tfoot>
</table>";


        public string GetInvoiceDefaultTemplate(InvoiceReport invoice, Customer customer, Organization organization, LogoOrganization logo)
        {
            int sayTotal = Convert.ToInt32(GetTotal(invoice.SubTotal, GetTotalDiscount(invoice.Items), GetTotalTax()));
            StringTemplate template = new StringTemplate(TEMPLATE);
            template.SetAttribute("invoices", invoice);
            template.SetAttribute("customer", customer);
            template.SetAttribute("organization", organization);
            template.SetAttribute("totaldiscount", GetTotalDiscount(invoice.Items).ToString("###,###,###,##0"));
            template.SetAttribute("totaltax", GetTotalTax().ToString("###,###,###,##0"));
            template.SetAttribute("total", GetTotal(invoice.SubTotal, GetTotalDiscount(invoice.Items), GetTotalTax()).ToString("###,###,###,##0"));
            template.SetAttribute("InvoiceDate",invoice.InvoiceDate.ToString("dd-MMM-yyyy"));
            template.SetAttribute("DueDate", invoice.DueDate.ToString("dd-MMM-yyyy"));
            template.SetAttribute("SubTotal", invoice.SubTotal.ToString("###,###,###,##0"));
            template.SetAttribute("terbilang", SayNumber.Terbilang(sayTotal));
            template.SetAttribute("Note", invoice.Note);
            if (logo != null)
            {
                template.SetAttribute("logodata", Convert.ToBase64String(logo.ImageData));
            }
            string InvoiceReport = template.ToString();
            return InvoiceReport;
        }
        private decimal GetTotalDiscount(List<InvoiceItemReport> items)
        {
            decimal totalDiscount = 0;
            foreach (InvoiceItemReport item in items)
            {
                totalDiscount += item.Discount;
            }
            return totalDiscount;
        }

        private decimal GetTotalTax()
        {
            decimal totalTax = 0;
            return totalTax;
        }
        private decimal GetTotal(decimal subTotal, decimal totalDiscount, decimal totalTax)
        {
            return (subTotal - totalDiscount) + totalTax;
        }
    }
}
