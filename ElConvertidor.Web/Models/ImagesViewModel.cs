using ElConvertidor.Core.Models;
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace ElConvertidor.Web.Models
{
    public class ImagesViewModel : BaseCollectibleViewModel, ISourceImage
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

        protected override bool CompareParameters(object obj)
        {
            var temp = obj as ImagesViewModel;
            return
                (Name == temp.Name &&
                Type == temp.Type &&
                Content.Length == temp.Content.Length);
        }

        protected override List<int> GetParameters()
        {
            return new List<int>()
            {
                Name.GetHashCode(),
                Type.GetHashCode(),
                Content.Length.GetHashCode()
            };
        }
    }
}