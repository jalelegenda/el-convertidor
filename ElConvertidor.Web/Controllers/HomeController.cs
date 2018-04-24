using ElConvertidor.Core.Infrastructure;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ElConvertidor.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITiffService _tiffService;

        public HomeController(ITiffService tiffService)
        {
            _tiffService = tiffService;
        }

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

            var result = _tiffService.ConvertImagesToMultipageTiff(files);

            return File(result, "image/tiff", "image.tif");
        }
    }
}