using ElConvertidor.Core.Client;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using ElConvertidor.Core.Models;

namespace ElConvertidor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageProcessingService _tiffService;
        private readonly ISessionService<ISourceImage> _imagesSessionService;
        private readonly IImageValidationService _imageValidationService;

        public HomeController(
            IImageProcessingService tiffService,
            ISessionService<ISourceImage> imagesSessionService,
            IImageValidationService imageValidationService)
        {
            _tiffService = tiffService;
            _imagesSessionService = imagesSessionService;
            _imageValidationService = imageValidationService;
        }

        [HttpGet]
        [ActionName("Index")]
        public ActionResult GetImageUploadForm()
        {
            return View();
        }

        [HttpPost]
        public string GetSessionImages()
        {
            var images = _imagesSessionService.GetCollection();
            return JsonConvert.SerializeObject(images, Formatting.Indented);
        }

        [HttpPost]
        public ActionResult ConvertImages()
        {
            var images = _imagesSessionService.GetCollection();

            if(images == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.RequestTimeout,
                    "Your session has expired. Select images for conversion again, please.");
            }

            var imageStream = _tiffService.ConvertImagesToMultipageTiff(images);
            _imagesSessionService.Clear();
            return File(imageStream, "image/tiff", "CompoundImage.tiff");
        }

        [HttpPost]
        public string AddImages(IEnumerable<ImagesViewModel> images)
        {
            var validImages = _imageValidationService.ExtractActualImages(images);
            if(validImages != null)
            {
                _imagesSessionService.AddCollection(validImages);
            }
            return JsonConvert.SerializeObject(validImages, Formatting.Indented);
        }

        [HttpPost]
        public bool RemoveImage(ImagesViewModel image)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            return _imagesSessionService.Remove(image);
        }

        [HttpPost]
        public void ClearImages()
        {
            _imagesSessionService.Clear();
        }
    }
}