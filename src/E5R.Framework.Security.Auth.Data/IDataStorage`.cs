// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Collections.Generic;

namespace E5R.Framework.Security.Auth.Data
{
    public interface IDataStorage<T>
        where T : class
                , new()
    {
        /// <summary>
        /// List all objects in storage
        /// </summary>
        IEnumerable<T> All { get; }

        /// <summary>
        /// Add one object in storage
        /// </summary>
        /// <param name="data"><see cref="T"/> object</param>
        /// <returns><see cref="T"/> object with Id filled</returns>
        /// <exception cref="Exception">If any errors occurred</exception>
        T Add(T data);

        /// <summary>
        /// Replace a existing object in storage
        /// </summary>
        /// <param name="data"><see cref="T"/> object</param>
        /// <returns><see cref="T"/> object</returns>
        /// <exception cref="Exception">If any errors occurred</exception>
        T Replace(T data);

        /// <summary>
        /// Remove a existing object in storage
        /// </summary>
        /// <param name="data"><see cref="T"/> object</param>
        /// <exception cref="Exception">If any errors occurred</exception>
        void Remove(T data);

        /// <summary>
        /// Remove a list of existing objects in storage
        /// </summary>
        /// <param name="data"><see cref="IEnumerable{T}"/> list of objects</param>
        /// <exception cref="Exception">If any errors occurred</exception>
        void Remove(IEnumerable<T> data);

        /// <summary>
        /// Get one object of storage by string representation of <see cref="T"/>.Id
        /// </summary>
        /// <param name="id">A string representation of object.Id</param>
        /// <returns><see cref="T"/> object or null if not found</returns>
        /// <exception cref="Exception">If any errors occurred</exception>
        T Get(string id);

        /// <summary>
        /// Get a list of objects in storage that meet the filter
        /// </summary>
        /// <param name="filter">Lambda expression to filter the result</param>
        /// <returns>List with objects founds</returns>
        IEnumerable<T> Get(Func<T, bool> filter);
    }
}