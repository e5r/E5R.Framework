// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;

#if DNXCORE50
using System.Reflection;
#endif

namespace E5R.Framework.Security.Auth.Data
{
    public interface IDataModel
    {
        string StringId { get; }
    }

    public class DataModel <T, I> : IDataModel
        where T : class
                , new()
        where I : class
                , new()
    {
        public I Id { get; set; }

        public string StringId
        {
            get { return Id.ToString(); }
        }

        public static T Create()
        {
            return new T();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class DataModel <T, I, D> : IDataModel
        where T : class
                , new()
        where I : class
                , new()
    {
        public I Id { get; set; }

        public string StringId
        {
            get { return Id.ToString(); }
        }

        public static T Create(D dependence)
        {
            var type = typeof(T);
            var parameters = new Type[1];
            parameters[0] = typeof(D);

            var constructor = type.GetConstructor(parameters);

            if (constructor == null)
            {
                throw new TypeInitializationException(
                    string.Format("{0} does not contain a constructor with the type parameter {1}.",
                    type, parameters[0]), null);
            }

            return (T)constructor.Invoke(new object[] { dependence });
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
