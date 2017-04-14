using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Recipes.Domain
{
    abstract public class EntityBase
    {
    }

    abstract public partial class EntityBase<T> : EntityBase, IEquatable<T>
    {
        public bool Equals(T other)
        {
            var result = EntityExtensions.Equals((dynamic)this, (dynamic)other);
            return result;
        }

        public override int GetHashCode()
        {
            var result = int.MinValue;
            var type = this.GetType();
            var pis = type.GetProperties().ToList();
            foreach (var pi in pis)
            {
                var val = pi.GetValue(this);

                if (null != val)
                {
                    result ^= val.GetHashCode();
                }
                else
                {
                    result ^= int.MinValue;
                }
            }
            return result;
        }

    }//class



    public static partial class EntityExtensions
    {
        public static void Copy<T>(this EntityBase<T> dst, EntityBase<T> src)
        {
            dst.CopyFields(src);
            dst.CopyProperties(src);
        }

        public static void CopyFields<T>(this EntityBase<T> dst, EntityBase<T> src)
        {
            var fis = dst.GetType().GetFields().ToList();
            foreach (var fi in fis)
            {
                var srcVal = (dynamic)fi.GetValue(src) as IComparable;
                fi.SetValue(dst, srcVal);
            }
        }

        public static void CopyProperties<T>(this EntityBase<T> dst, EntityBase<T> src)
        {
            var pis = dst.GetType().GetProperties().ToList();
            foreach (var pi in pis)
            {
                var srcVal = (dynamic)pi.GetValue(src);// as IComparable;
                pi.SetValue(dst, srcVal);
            }
        }

        public static bool Equals<T>(this EntityBase<T> x, EntityBase<T> y)
        {
            bool result = ((x == y) || ((x != null) && (y != null)));
            if (result)
            {
                if (null != x)
                {
                    result = CompareFields(x, y);
                    if (result)
                    {
                        result = CompareProperties(x, y);
                    }
                    return result;
                }
                result = false;
            }
            return result;
        }

        public static bool CompareFields<T>(this EntityBase<T> client, EntityBase<T> server, AuditResult auditResult = null)
        {
            bool result = true;
            var fis = GetCommonFields(client.GetType(), server.GetType());
            foreach (var fi in fis)
            {
                var cliVal = (dynamic)fi.GetValue(client) as IComparable;
                var srvVal = (dynamic)fi.GetValue(server) as IComparable;

                if (((object)cliVal) == null || ((object)srvVal) == null)
                {
                    result = Object.Equals(cliVal, srvVal);
                    if (null != auditResult && !result)
                    {
                        auditResult.Add(client.GetType().Name, EntityState.Modified, fi.Name, srvVal.ToString(), cliVal.ToString());
                    }
                }
                else if (!cliVal.Equals(srvVal))
                {
                    result = false;
                    if (null != auditResult)
                    {
                        auditResult.Add(client.GetType().Name, EntityState.Modified, fi.Name, srvVal.ToString(), cliVal.ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return result;
        }

        static List<FieldInfo> GetCommonFields(Type a, Type b)
        {
            var cliFis = a.GetType().GetFields().ToList();
            var srvFis = b.GetType().GetFields().ToList();
            var result = cliFis.Intersect(srvFis).ToList();
            return result;
        }

        public static bool CompareProperties<T>(this EntityBase<T> client, EntityBase<T> server, , AuditResult auditResult = null)
        {
            "this method is preliminarialy done."
            bool result = true;
            var pis = GetCommonProperties(client.GetType(), server.GetType());
            foreach (var pi in pis)
            {
                var cliVal = (dynamic)pi.GetValue(client);
                var srvVal = (dynamic)pi.GetValue(server);

                if (cliVal is EntityBase)
                {
                    result = EntityExtensions.Equals(cliVal, srvVal, auditResult);
                }
                else if (cliVal is IEnumerable && !(cliVal is String))
                {
                    result = EntityExtensions.Equals(cliVal, srvVal, auditResult);
                }
                else
                {
                    result = (cliVal == srvVal);
                    if (!result && null != auditResult)
                    {
                        auditResult.Add(client.GetType().Name,
                            EntityState.Modified,
                            pi.Name,
                            (null == srvVal) ? null : srvVal.ToString(),
                            (null == cliVal) ? null : cliVal.ToString());
                    }

                    if (!result && null == auditResult)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        class PropertyInfoComparer : IEqualityComparer<PropertyInfo>
        {
            public bool Equals(PropertyInfo x, PropertyInfo y)
            {
                var result = x == y;
                if (!result)
                    new object();
                return result;
            }

            public int GetHashCode(PropertyInfo obj)
            {
                return obj.GetHashCode();
            }
        }//class

        static List<PropertyInfo> GetCommonProperties(Type a, Type b)
        {
            const string PROXY = "System.Data.Entity.DynamicProxies";
            if (a.Namespace == PROXY)
                a = a.BaseType;
            if (b.Namespace == PROXY)
                b = b.BaseType;


            var aProps = a.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty).OrderBy(x => x.Name).ToList();
            var bProps = b.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty).OrderBy(x => x.Name).ToList();
            var result = aProps.Intersect(bProps, new PropertyInfoComparer()).ToList();

            var exclude = (
                from pi in result
                from ca in pi.CustomAttributes
                where ca.AttributeType == typeof(JsonIgnoreAttribute)
                select (pi)).ToList();
            exclude.ForEach(x => result.Remove(x));

            return result;
        }

        public static bool Equals<T>(this List<T> cliList, List<T> srvList)
        {
            bool result = Object.ReferenceEquals(cliList, srvList);
            if (!result && null != cliList)
            {
                var cliCopy = new List<T>(cliList);
                var srvCopy = new List<T>(srvList);

                var additions = cliCopy.Where(x => !srvCopy.Contains(x)).ToList();
                additions.ForEach(x => srvCopy.Add(x));

                var deletions = srvCopy.Where(x => !cliCopy.Contains(x)).ToList();
                deletions.ForEach(x => srvCopy.Remove(x));

                var ignore = additions.Concat(deletions).ToList();

                var comparer = new EqualityComparer<T>();
                var changes = srvCopy.Except(cliCopy, comparer).ToList();

                if (additions.Count > 0
                    || deletions.Count > 0
                    || changes.Count > 0)
                {
                    result = false;
                }
            }
            return result;
        }

        public static bool Equals<T>(this HashSet<T> cli, HashSet<T> srv)
        {
            bool result = Object.ReferenceEquals(cli, srv);
            if (!result && null != cli)
            {
                var cliList = cli.ToList();
                var srvList = srv.ToList();

                result = cliList.Equals(srvList);
            }
            return result;
        }


        class EqualityComparer<T> : IEqualityComparer<T>
        {
            public EqualityComparer()
            {
            }

            public bool Equals(T x, T y)
            {
                var result = EntityExtensions.Equals((dynamic)x, (dynamic)y);
                if (!result)
                    new object();
                return result;
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }//class

    }//class


}
