using System;
using System.Reflection;

namespace CommonReadModel
{
    /// <summary>
    /// 'casting' base to derived class
    /// https://stackoverflow.com/questions/729527/is-it-possible-to-assign-a-base-class-object-to-a-derived-class-reference-with-a
    /// </summary>
    public static class ValueDateExtensions
    {
        public static T As<T>(this ValueDate valueDate) where T : ValueDate
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance(type);

            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                property.SetValue(instance, property.GetValue(valueDate, null), null);
            }

            return (T)instance;
        }
    }
}
