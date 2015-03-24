// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Framework.Logging;

namespace E5R.Framework.Security.Auth.Data.InMemory
{
    public class InMemoryRepository<T>
        where T : IDataModel
    {
        private readonly ILogger _logger;

        public InMemoryRepository(ILoggerFactory loggerFactory)
        {
            var loggerName = typeof(InMemoryDatabase).FullName.Split('.').LastOrDefault();

            _logger = loggerFactory.Create(loggerName);
        }

        public IEnumerable<T> All
        {
            get
            {
                return InMemoryDatabase.GetDatabase<T>(_logger).AsEnumerable();
            }
        }

        public T Add(T data)
        {
            if (All.Count(where => where.GetHashCode() == data.GetHashCode()) > 0)
                throw new Exception("Object already exists in the database.");

            (All as IList<T>).Add(data);

            return data;
        }

        public IEnumerable<T> Get(Func<T, bool> filter)
        {
            return All.Where(filter);
        }

        public T Get(string id)
        {
            return All.SingleOrDefault(where => where.StringId == id);
        }

        public void Remove(IEnumerable<T> data)
        {
            foreach (var item in data)
                Remove(item);
        }

        public void Remove(T data)
        {
            if (All.Count(where => where.GetHashCode() == data.GetHashCode()) != 1)
                throw new Exception("Object not found in the database.");

            (All as IList<T>).Remove(data);
        }

        public T Replace(T data)
        {
            var old = All.SingleOrDefault(where => where.GetHashCode() == data.GetHashCode());

            if (old == null)
                throw new Exception("Object not found in the database.");

            var list = (All as IList<T>);
            var index = list.IndexOf(old);

            if (index < 0)
                throw new Exception("Object not found in the database.");

            list[index] = data;

            return list[index];
        }
    }
}