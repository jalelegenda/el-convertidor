using ElConvertidor.Core.Models;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace ElConvertidor.Web.Models
{
    public class ImagesViewModel : BaseCollectibleViewModel, ISourceImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public bool ShouldSerializeContent() { return false; }
        public Stream Content { get; private set; }


        private HttpPostedFileBase _file;

        public bool ShouldSerializeFile() { return false; }
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

        protected override bool CompareParameters(object obj)
        {
            var temp = obj as ImagesViewModel;
            if (temp == null)
            {
                return false;
            }
            return
                (Id == temp.Id && 
                Name == temp.Name &&
                Type == temp.Type);
        }

        protected override List<int> GetParameters()
        {
            return new List<int>()
            {
                Name.GetHashCode(),
                Type.GetHashCode()
            };
        }
    }
}