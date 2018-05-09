using ElConvertidor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElConvertidor.Core.Infrastructure
{
    public interface IImageValidationService
    {
        IEnumerable<ISourceImage> ExtractActualImages(IEnumerable<ISourceImage> images);
    }
}
