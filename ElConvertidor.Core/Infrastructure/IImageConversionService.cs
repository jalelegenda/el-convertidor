using System.Collections.Generic;
using ElConvertidor.Core.Models;

namespace ElConvertidor.Core.Infrastructure
{
    public interface IImageProcessingService
    {
        bool ConvertImagesToMultipageTiff(IEnumerable<IImage> images);
    }
}
