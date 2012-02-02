using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dokuku.sales.invoices.model;
using Antlr3.ST;
using dokuku.sales.customer.model;

namespace dokuku.sales.invoices.viewtemplating
{
    public class DefaultTemplate
    {
        const string TEMPLATE = @"<table width='100%' border='0' cellspacing='0' cellpadding='0' style='font-size:11px; font-family:Tahoma;'>
	<tbody>
		<tr>
			<td style='padding-left:10px'>
				<div width='100%' style='font-weight:bold; font-size:18px'>PT. Inforsys Indonesia</div>
				<div width='100%' style='font-size:14px'>Jln. Laksamana Bintan BLok B</div>
				<div width='100%' style='font-size:14px'>No 5 & 6 Batam Indonesia</div>
				<div width='100%' style='font-size:14px'>Telp. 0778-472111; Fax. 0778-472004</div>
			</td>	
			<td style='text-align:center;'> <img class='logo' src='../logoOrganization'/> </td>
		</tr>
		<tr>
			<td colspan ='2'><hr size='1' color='black'></td>
		</tr>
		<tr>
			<td colspan='2' align='center'><div style='font-weight:bold; font-size:18px; letter-spacing:2px; margin-top: 10px; margin-bottom: 15px;'>INVOICE</div></td>
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
		<tr>
			<td colspan='2' >
				<table width='100%' border='0' style='margin-top:20px; border:solid 1px black; border-spacing: 0; font-size:11px; font-family:Tahoma;'>
					<thead>
						<tr height='40px' style='background-color:#ebebeb; font-weight:bold'>
							<td style='background-color:#ebebeb; border-right:solid 1px black; border-bottom:solid 1px black; padding:0px 0px 1px 3px;'>Barang</td>
							<td width='9%' align='center' style='background-color:#ebebeb; border-right:solid 1px black; border-bottom:solid 1px black;'>Kuantitas</td>
							<td width='15%' align='right' style='background-color:#ebebeb; border-right:solid 1px black; border-bottom:solid 1px black; padding:0px 5px 1px 0;'>@Harga</td>
							<td width='15%' align='right' style='background-color:#ebebeb; border-right:solid 1px black; border-bottom:solid 1px black; padding:0px 5px 1px 0;'>Diskon</td>
							<td width='15%' align='right' style='background-color:#ebebeb; border-bottom:solid 1px black; padding:0px 5px 1px 0;'>Total</td>
						</tr>
                        <tr height='10px' border='1'>
							<td colspan='5' style='border-bottom:solid 1px black'></td>
						</tr>
					</thead>
					<tbody>
						$invoices.Items:{
						<tr>
							<td style='border-right:solid 1px black; padding:4px 3px;'>
								<div>$it.PartName$</div>
							</td>
							<td style='border-right:solid 1px black; padding:4px 3px;'>
								<div align='center'>$it.Qty$</div>
							</td>
							<td style='border-right:solid 1px black; padding:4px 3px;'>
								<div align='right'>$it.Rate$</div>
							</td>
							<td style='border-right:solid 1px black; padding:4px 3px;'>
								<div align='right'>$it.Discount$</div>
							</td>
							<td>
								<div align='right'>$it.Amount$</div>
							</td>
						</tr>
						}$
					</tbody>
					<tfoot>
						<tr>
							<td colspan='4' align='right' style='border-right:solid 1px black; border-top:solid 1px black; padding:4px 3px;'>Sub Total</td>
							<td align='right' style='border-top:solid 1px black; padding:4px 3px;'>$SubTotal$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='border-right:solid 1px black; padding:4px 3px;'>Diskon</td>
							<td align='right' style='padding:4px 3px;'>$totaldiscount$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='border-right:solid 1px black; padding:4px 3px;'>Pajak</td>
							<td align='right' style='padding:4px 3px;'>$totaltax$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='border-right:solid 1px black; padding:4px 3px;'>Total</td>
							<td align='right' style='padding:4px 3px;'>$total$</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='border-right:solid 1px black; padding:4px 3px;'>Uang muka/Pembayaran</td>
							<td align='right' style='padding:4px 3px;'>0</td>
						</tr>
						<tr>
							<td align='right' colspan='4' style='border-right:solid 1px black; padding:4px 3px;'>Total Bersih</td>
							<td align='right' style='padding:4px 3px;'>$total$</td>
						</tr>
					</tfoot>
				</table>
			</td>
		</tr>
	</tbody>
</table>";


        public string GetInvoiceDefaultTemplate(Invoices invoice, Customer customer)
        {
            StringTemplate template = new StringTemplate(TEMPLATE);
            template.SetAttribute("invoices", invoice);
            template.SetAttribute("customer", customer);
            template.SetAttribute("totaldiscount", GetTotalDiscount(invoice.Items).ToString("###,###,###,##0"));
            template.SetAttribute("totaltax", GetTotalTax().ToString("###,###,###,##0"));
            template.SetAttribute("total", GetTotal(invoice.SubTotal, GetTotalDiscount(invoice.Items), GetTotalTax()).ToString("###,###,###,##0"));
            template.SetAttribute("InvoiceDate",invoice.InvoiceDate.ToString("dd-MMM-yyyy"));
            template.SetAttribute("DueDate", invoice.DueDate.ToString("dd-MMM-yyyy"));
            template.SetAttribute("SubTotal", invoice.SubTotal.ToString("###,###,###,##0"));
            string InvoiceReport = template.ToString();
            return InvoiceReport;
        }
        private decimal GetTotalDiscount(InvoiceItem[] items)
        {
            decimal totalDiscount = 0;
            foreach (InvoiceItem item in items)
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
