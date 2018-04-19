using ElConvertidor.Web.Models;
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
        public ActionResult UploadImages(HttpPostedFileBase file)
        {
            var tiffEncoder = ImageCodecInfo.GetImageEncoders()
                .SingleOrDefault(dec => dec.FormatID == ImageFormat.Tiff.Guid);
            Stream imgStream = new MemoryStream();
            using (EncoderParameters encParams = new EncoderParameters(1))
            using (Image img = Image.FromStream(file.InputStream, true, true))
            {
                encParams.Param[0] = new EncoderParameter(Encoder.Quality, 100);
                img.Save(imgStream, tiffEncoder, encParams);
            }
            imgStream.Position = 0;
            var result = File(imgStream, "image/tiff", "image.tif");
            return result;
        }
    }
}