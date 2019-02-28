using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GraphQL.Conventions.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// This Methode extends the System.Type-type to get all extended methods. It searches hereby in all assemblies which are known by the current AppDomain.
        /// </summary>
        /// <remarks>
        /// Insired by Jon Skeet from his answer on http://stackoverflow.com/questions/299515/c-sharp-reflection-to-identify-extension-methods
        /// </remarks>
        /// <returns>returns MethodInfo[] with the extended Method</returns>

        public static MethodInfo[] GetExtensionMethods(this TypeInfo t, Assembly assembly)
        {
            var query = from type in assembly.GetTypes()
                        where !type.IsNested
                        from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        where method.IsDefined(typeof(ExtensionAttribute), false)
                        where method.GetParameters()[0].ParameterType == t.UnderlyingSystemType
                        select method;
            return query.ToArray<MethodInfo>();
        }
    }
}