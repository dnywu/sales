using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Cryptography;
namespace dokuku.security
{
    public class DokukuKeyGenerator : IKeyGenerator
    {
        public byte[] GetBytes(int count)
        {
            byte[] result = Encoding.UTF8.GetBytes("S31panas");
            if (result.Length != count)
                Array.Resize(ref result, count);
            return result;
        }
    }
}