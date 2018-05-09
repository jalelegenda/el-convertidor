using ElConvertidor.Business.Helpers;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElConvertidor.Business
{
    public class ImageValidationService : IImageValidationService
    {
        // could be delegated to a service
        // to provide at runtime
        // public interface IFormatResolver
        // {
        //     List<byte[]> SupportedFormats { get; set; }
        // }
        // edit to allow more formats

        private List<byte[]> validFormats = new List<byte[]>
        {
            Encoding.ASCII.GetBytes("BM"),          // bmp
            Encoding.ASCII.GetBytes("GIF"),         // gif
            new byte[] { 255, 216, 255, 224 },      // jpeg
            new byte[] { 255, 216, 255, 225 }       // jpeg canon
        };

        private bool IsValid(ISourceImage image)
        {
            var content = Converters.StreamToByteArray(image.Content);
            if(validFormats.Any(vf => vf.SequenceEqual(content.Take(vf.Length))))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<ISourceImage> ExtractActualImages(IEnumerable<ISourceImage> images)
        {
            var valid = new List<ISourceImage>();

            foreach(var image in images)
            {
                if (IsValid(image))
                {
                    valid.Add(image);
                }
            }

            return valid.Count == 0 ? null : valid;
        }
    }
}
