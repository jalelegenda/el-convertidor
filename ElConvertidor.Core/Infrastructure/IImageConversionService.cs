using System.Collections.Generic;
using ElConvertidor.Core.Models;
using System.IO;

namespace ElConvertidor.Core.Infrastructure
{
    public interface IImageProcessingService
    {
        Stream ConvertImagesToMultipageTiff(IEnumerable<ISourceImage> images);
    }
}
