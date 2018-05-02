using System.IO;
using System.Web;

namespace ElConvertidor.Web.Models
{
    public class ImagesViewModel
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public Stream Content { get; private set; }
        public HttpPostedFileBase File { get; set; }
    }
}