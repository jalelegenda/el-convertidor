using ElConvertidor.Core.Models;
using System.IO;
using System.Web;

namespace ElConvertidor.Web.Models
{
    public class ImagesViewModel : IImage
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public Stream Content { get; private set; }
        private HttpPostedFileBase _file;
        public HttpPostedFileBase File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
                Name = File.FileName;
                Type = File.ContentType;
                Content = new MemoryStream();
                File.InputStream.CopyTo(Content);
            }
        }
    }
}