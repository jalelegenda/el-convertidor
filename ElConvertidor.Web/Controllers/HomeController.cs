using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElConvertidor.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult UploadImages()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Stream imageStream = new MemoryStream();

            using (EncoderParameters encParams = new EncoderParameters(1))
            using (EncoderParameter multiParam = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame))
            using (EncoderParameter pageParam = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage))
            using (EncoderParameter flushParam = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.Flush))
            {
                var tiffEncoderInfo = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(dec => dec.FormatID == ImageFormat.Tiff.Guid);

                Queue<HttpPostedFileBase> fileQueue = new Queue<HttpPostedFileBase>(files);
                encParams.Param[0] = multiParam;

                var file = fileQueue.Dequeue();

                Image mainTiff = Image.FromStream(file.InputStream, true, true);
                mainTiff.Save(imageStream, tiffEncoderInfo, encParams);

                while (fileQueue.Count > 0)
                {
                    encParams.Param[0] = pageParam;

                    file = fileQueue.Dequeue();
                    Image page = Image.FromStream(file.InputStream, true, true);
                    mainTiff.SaveAdd(page, encParams);
                }
                encParams.Param[0] = flushParam;
                imageStream.Position = 0;
            }

            return File(imageStream, "image/tiff", "image.tif");
        }
    }
}