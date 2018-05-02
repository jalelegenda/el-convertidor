using ElConvertidor.Core;
using System.Web;
using System.Collections.Generic;


namespace ElConvertidor.Web
{
    public class SessionService<T> : ISessionService<T> where T : class
    {
        private readonly string _key;

        public SessionService()
        {
            _key = typeof(T).Name;
        }

        public void Add(T model)
        {
            var session = GetCollection() as List<T> ?? new List<T>();
            session.Add(model);
            AddCollection(session);
        }

        public void AddCollection(IEnumerable<T> models)
        {
            var session = GetCollection() as List<T>;
            if(session == null)
            {
                StoreCollection(models);
                return;
            }
            session.AddRange(models);
            StoreCollection(session);
        }

        public void Clear()
        {
            HttpContext.Current.Session[_key] = null;

        }

        public T Get()
        {
            return HttpContext.Current.Session[_key] as T;
        }

        public IEnumerable<T> GetCollection()
        {
            return HttpContext.Current.Session[_key] as IEnumerable<T>;
        }

        public bool Store(T model, bool? overwrite = true)
        {
            var session = Get();
            if(session != null && overwrite == false)
            {
                return false;
            }
            HttpContext.Current.Session[_key] = model;
            return true;
        }

        public bool StoreCollection(IEnumerable<T> models, bool? overwrite = true)
        {
            var session = GetCollection();
            if(session != null && overwrite == false)
            {
                return false;
            }
            HttpContext.Current.Session[_key] = models;
            return true;
        }
    }
}