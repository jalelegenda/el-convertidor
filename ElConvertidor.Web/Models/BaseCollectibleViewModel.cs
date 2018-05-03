using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElConvertidor.Web.Models
{
    public abstract class BaseCollectibleViewModel
    {
        protected abstract bool CompareParameters(object item);

        protected abstract List<int> GetParameters();

        public override bool Equals(object obj)
        {
            var toCompare = obj as ImagesViewModel;
            if (CompareParameters(obj))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            foreach (var parameter in GetParameters())
            {
                hash = (hash * 7) + parameter.GetHashCode();
            }

            return hash;
        }

        public static bool operator ==(BaseCollectibleViewModel a, BaseCollectibleViewModel b)
        {
            if (a.Equals(b))
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(BaseCollectibleViewModel a, BaseCollectibleViewModel b)
        {
            if (a.Equals(b))
            {
                return false;
            }
            return true;
        }
    }
}