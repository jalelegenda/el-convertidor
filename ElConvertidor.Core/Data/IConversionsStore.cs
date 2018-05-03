using ElConvertidor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElConvertidor.Core.Data
{
    public interface IConversionsStore
    {
        void StoreConversion(IEnumerable<ISourceImage> source);
    }
}
