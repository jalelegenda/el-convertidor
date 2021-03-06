﻿using ElConvertidor.Core.Data;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Core.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ElConvertidor.Business
{
    public sealed class ImageProcessingService : IImageProcessingService
    {
        private IConversionsStore _conversionsStore;

        public ImageProcessingService(IConversionsStore conversionsStore)
        {
            _conversionsStore = conversionsStore;
        }

        public Stream ConvertImagesToMultipageTiff(IEnumerable<ISourceImage> images)
        {
            Stream imageStream = new MemoryStream();

            using (EncoderParameters encParams = new EncoderParameters(1))
            using (EncoderParameter multiParam = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame))
            using (EncoderParameter pageParam = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage))
            using (EncoderParameter flushParam = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.Flush))
            {
                var tiffEncoder = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(dec => dec.FormatID == ImageFormat.Tiff.Guid);

                Queue<ISourceImage> fileQueue = new Queue<ISourceImage>(images);
                encParams.Param[0] = multiParam;

                var file = fileQueue.Dequeue();

                Image mainTiff = Image.FromStream(file.Content, true, true);
                mainTiff.Save(imageStream, tiffEncoder, encParams);

                while (fileQueue.Count > 0)
                {
                    encParams.Param[0] = pageParam;

                    file = fileQueue.Dequeue();
                    Image page = Image.FromStream(file.Content, true, true);
                    mainTiff.SaveAdd(page, encParams);
                }
                encParams.Param[0] = flushParam;
                imageStream.Position = 0;
            }
            _conversionsStore.StoreConversion(images);
            return imageStream;
        }
    }
}
