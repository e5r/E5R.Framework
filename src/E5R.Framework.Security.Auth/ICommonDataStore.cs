using System;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth
{
    public interface ICommonDataStore<T>
    {
        T Create(T data);

        T Update(T data);

        ICollection<T> GetAll();

        T GetOne(AuthId id);

        bool Remove(T data);
    }
}