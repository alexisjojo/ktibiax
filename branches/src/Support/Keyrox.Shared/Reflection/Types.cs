using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Keyrox.Shared.Reflection {
    public static class Types {

        /// <summary>
        /// Gets the types with attribute.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static IList<Type> GetTypesWithAttribute(this Assembly asm, Type attribute) {
            return (from type in asm.GetTypes() where type.HasAttribute(attribute) select type).ToList();
        }

        /// <summary>
        /// Gets the methods with attribute.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static IList<MethodInfo> GetMethodsWithAttribute(this Type type, Type attribute) {
            return (from method in type.GetMethods() where method.HasAttribute(attribute) select method).ToList();
        }

        /// <summary>
        /// Gets the properties with attribute.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetPropertiesWithAttribute(this Type type, Type attribute) {
            return (from prop in type.GetProperties() where prop.HasAttribute(attribute) select prop).ToList();
        }
    }
}
