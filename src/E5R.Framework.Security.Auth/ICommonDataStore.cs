using E5R.Framework.Security.Auth.Model;
using System;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth
{
    public interface ICommonDataStore<T>
    {
        T Create(T data);

        IEnumerable<T> GetAll();

        T GetById(AuthId id);

        bool Delete(T data);

        T Save(T data);
    }
}