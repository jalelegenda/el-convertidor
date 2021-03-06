﻿using System.Collections.Generic;

namespace ElConvertidor.Core.Client
{
    public interface ISessionService<T>
    {
        T Get();
        bool Store(T model, bool? overwrite = true);
        bool Remove(T model);
        void Clear();

        IEnumerable<T> GetCollection();
        bool StoreCollection(IEnumerable<T> models, bool? overwrite = true);
        void Add(T model);
        void AddCollection(IEnumerable<T> models);
    }
}
