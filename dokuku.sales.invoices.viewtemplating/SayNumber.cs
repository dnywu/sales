using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku.sales.invoices.viewtemplating
{
    public static class SayNumber
    {
        public static string Terbilang(int x)
        {
            string[] bilangan = {"", "Satu", "Dua", "Tiga", "Empat", "Lima",
                            "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh",
                            "Sebelas"};
            string temp = "";

            if (x < 12)
            {
                temp = " " + bilangan[x];
            }
            else if (x < 20)
            {
                temp = Terbilang(x - 10).ToString() + " Belas";
            }
            else if (x < 100)
            {
                temp = Terbilang(x / 10) + " Puluh" + Terbilang(x % 10);
            }
            else if (x < 200)
            {
                temp = " Seratus" + Terbilang(x - 100);
            }
            else if (x < 1000)
            {
                temp = Terbilang(x / 100) + " Ratus" + Terbilang(x % 100);
            }
            else if (x < 2000)
            {
                temp = " Seribu" + Terbilang(x - 1000);
            }
            else if (x < 1000000)
            {
                temp = Terbilang(x / 1000) + " Ribu" + Terbilang(x % 1000);
            }
            else if (x < 1000000000)
            {
                temp = Terbilang(x / 1000000) + " Juta" + Terbilang(x % 1000000);
            }

            return temp;
        }
    }
}

