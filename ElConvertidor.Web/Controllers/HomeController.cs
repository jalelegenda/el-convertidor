using ElConvertidor.Core.Client;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Core.Models;
using ElConvertidor.Web.Models;
using System.Collections.Generic;
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
        public bool UploadImages()
        {
            var images = _imagesSessionService.GetCollection();
            return _tiffService.ConvertImagesToMultipageTiff(images);
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

            var isSuccess = _imagesSessionService.Remove(image);
            return isSuccess;
        }
    }
}