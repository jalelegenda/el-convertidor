using ElConvertidor.Core;
using ElConvertidor.Core.Infrastructure;
using ElConvertidor.Web.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ElConvertidor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITiffService _tiffService;
        private readonly ISessionService<ImagesViewModel> _imagesSessionService;

        public HomeController(
            ITiffService tiffService,
            ISessionService<ImagesViewModel> imagesSessionService)
        {
            _tiffService = tiffService;
            _imagesSessionService = imagesSessionService;
        }

        [HttpGet]
        [ActionName("Index")]
        public ActionResult UploadImages()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public bool UploadImages(IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            return _tiffService.ConvertImagesToMultipageTiff(files);
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
    }
}