using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElConvertidor.Business.Helpers
{
    public static class Converters
    {
        public static byte[] StreamToByteArray(Stream input)
        {
            using(var ms = new MemoryStream())
            {
                input.Position = 0;
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
