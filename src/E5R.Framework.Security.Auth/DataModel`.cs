using System;
using System.Reflection;

namespace E5R.Framework.Security.Auth
{
    public class DataModel <T>
        where T : class
                , new()
    {
        public static T Create()
        {
            return new T();
        }
    }

    public class DataModel <T, D>
        where T : class
                , new()
    {
        public static T Create(D dependence)
        {
            var type = typeof(T);
            var parameters = new Type[1];
            parameters[0] = typeof(D);

            var constructor = type.GetConstructor(parameters);

            if(constructor == null){
                throw new TypeInitializationException(
                    string.Format("{0} does not contain a constructor with the type parameter {1}.",
                    type, parameters[0]), null);
            }

            return (T) constructor.Invoke(new object[]{ dependence });
        }
    }
}
