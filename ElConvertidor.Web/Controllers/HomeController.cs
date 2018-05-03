using ElConvertidor.Core.Client;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace ElConvertidor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageProcessingService _tiffService;
        private readonly ISessionService<ImagesViewModel> _imagesSessionService;

        public HomeController(
            IImageProcessingService tiffService,
            ISessionService<ImagesViewModel> imagesSessionService)
        {
            _tiffService = tiffService;
            _imagesSessionService = imagesSessionService;
        }

        [HttpGet]
        [ActionName("Index")]
        public ActionResult GetImageUploadForm()
        {
            return View();
        }

        [HttpPost]
        public void UploadImages()
        {
            var images = _imagesSessionService.GetCollection();
            if(images == null)
            {
                throw new Exception("Nothing to upload");
            }

            var imageStream = _tiffService.ConvertImagesToMultipageTiff(images);
            _imagesSessionService.Clear();
            //return File(imageStream, "image/tiff", "CompoundImage.tiff");
        }

        [HttpPost]
        public bool AddImages(IEnumerable<ImagesViewModel> images)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            _imagesSessionService.AddCollection(images);
            return true;
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
    }
}