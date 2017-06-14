using Eric.Morrison;
using Newtonsoft.Json;
using Recipes.Contracts;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Services.Tests
{
    static class RandomEntityExtensions
    {
        const string SYSTEM_COLLECTIONS_GENERIC = "System.Collections.Generic";
        static public T CreateRandom<T>() where T : EntityBase<T>
        {
            T result = Activator.CreateInstance<T>();
            result.Populate();
            return result;
        }


        static void Populate<T>(this EntityBase<T> entity) where T : EntityBase<T>
        {
            var fis = typeof(T).GetFieldsEx();
            foreach (var fi in fis)
            {
                object val = null;
                if (fi.FieldType == typeof(string))
                {
                    val = RandomString.GetAlphaOnly(RandomValue.Next<uint>(8, 16));
                }
                else if (fi.FieldType.IsPrimitive)
                {
                    MethodInfo method = typeof(RandomValue).GetMethod("Next");
                    Debug.Assert(null != method);
                    MethodInfo generic = method.MakeGenericMethod(fi.FieldType);
                    val = generic.Invoke(null, null);
                }
                fi.SetValue(entity, val);
            }

            var pis = typeof(T).GetPropertiesEx();
            foreach (var pi in pis)
            {
                object val = null;
                if (pi.PropertyType == typeof(string))
                {
                    val = RandomString.GetAlphaOnly(RandomValue.Next<uint>(8, 16));
                }
                else if (pi.PropertyType.IsPrimitive)
                {
                    MethodInfo method = typeof(RandomValue).GetMethod("Next");
                    Debug.Assert(null != method);
                    MethodInfo generic = method.MakeGenericMethod(pi.PropertyType);
                    val = generic.Invoke(null, null);
                }
                pi.SetValue(entity, val);
            }
        }

        static List<Type> ExcludedAttributeTypes = new List<Type>()
                {
                    typeof(NotMappedAttribute),
                    typeof(JsonIgnoreAttribute),
                    typeof(ForeignKeyAttribute),
                    typeof(UniqueIdentifierAttribute)
                };

        static public IEnumerable<FieldInfo> GetFieldsEx(this Type t)
        {
            var arr = t.GetFields();
            var result = (
                from info in arr
                from ca in info.GetCustomAttributesData()
                where !ExcludedAttributeTypes.Contains(ca.AttributeType)
                select info);
            return result;
        }

        static public IEnumerable<PropertyInfo> GetPropertiesEx(this Type t) 
        {
            var arr = t.GetProperties();
            var result = (
                from info in arr
                from ca in info.GetCustomAttributesData()
                where !ExcludedAttributeTypes.Contains(ca.AttributeType)
                select info);
            return result;
        }

        static public IEnumerable<MemberInfo> FilterMemberInfo(this IEnumerable<MemberInfo> arr)
        {
            var result = (
                from info in arr
                from ca in info.GetCustomAttributesData()
                where !ExcludedAttributeTypes.Contains(ca.AttributeType)
                select info);

            return result;
        }







    }//class
}//ns
