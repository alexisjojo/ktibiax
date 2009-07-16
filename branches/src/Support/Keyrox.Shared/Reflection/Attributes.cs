using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Keyrox.Shared.Reflection {
    public static class Attributes {

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MethodInfo method) where T : class {
            var atts = method.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.First() as T : default(T);
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static T[] GetAttributes<T>(this MethodInfo method) where T : class {
            var atts = method.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.Cast<T>().ToArray() : null;
        }

        /// <summary>
        /// Gets the prop attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static T GetPropAttribute<T>(this PropertyInfo property) where T : class {
            var atts = property.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.First() as T : default(T);
        }

        /// <summary>
        /// Gets the class attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static T GetClassAttribute<T>(this Type type) where T : class {
            var atts = type.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? atts.First() as T : default(T);
        }

        /// <summary>
        /// Determines whether the specified type has attribute.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type has attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(this Type type, Type attribute) {
            return type.GetCustomAttributes(attribute, true).Count() > 0;
        }

        /// <summary>
        /// Determines whether the specified method has attribute.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>
        /// 	<c>true</c> if the specified method has attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(this MethodInfo method, Type attribute) {
            return method.GetCustomAttributes(attribute, true).Count() > 0;
        }

        /// <summary>
        /// Determines whether the specified property has attribute.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns>
        /// 	<c>true</c> if the specified property has attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(this PropertyInfo property, Type attribute) {
            return property.GetCustomAttributes(attribute, true).Count() > 0;
        }

    }
}
