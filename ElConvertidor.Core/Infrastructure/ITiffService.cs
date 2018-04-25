using System.Web;
using System.Collections.Generic;
using System.IO;

namespace ElConvertidor.Core.Infrastructure
{
    public interface ITiffService
    {
        bool ConvertImagesToMultipageTiff(IEnumerable<HttpPostedFileBase> images);
    }
}
