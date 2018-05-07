using ElConvertidor.Core.Models;
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ElConvertidor.Web.Models
{
    public class ImagesViewModel : BaseCollectibleViewModel, ISourceImage
    {
        public string Name { get; set; }
        public string Type { get; set; }
        [ScriptIgnore]
        [JsonIgnore]
        public Stream Content { get; private set; }
        private HttpPostedFileBase _file;
        [ScriptIgnore]
        [JsonIgnore]
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
                (Name == temp.Name &&
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