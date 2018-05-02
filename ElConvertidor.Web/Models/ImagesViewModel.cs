using ElConvertidor.Core.Models;
using System;
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

        public override bool Equals(object obj)
        {
            var toCompare = obj as ImagesViewModel;
            if (string.Equals(Name, toCompare.Name, StringComparison.Ordinal) &&
                string.Equals(Type, toCompare.Type, StringComparison.Ordinal) &&
                Content.Length == toCompare.Content.Length)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            hash = (hash * 7) + Type.GetHashCode();
            hash = (hash * 7) + Content.Length.GetHashCode();

            return hash;
        }

        public static bool operator ==(ImagesViewModel a, ImagesViewModel b)
        {
            if (a.Equals(b))
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(ImagesViewModel a, ImagesViewModel b)
        {
            if (a.Equals(b))
            {
                return false;
            }
            return true;
        }
    }
}