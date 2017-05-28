using Newtonsoft.Json;
using Recipes.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    public static partial class EntityExtensions
    {
        const string PROXY_NAMESPACE = "System.Data.Entity.DynamicProxies";

        public static void Copy<T>(this EntityBase<T> dst, EntityBase<T> src)
        {
            dst.CopyFields(src);
            dst.CopyProperties(src);
        }

        public static void CopyFields<T>(this EntityBase<T> dst, EntityBase<T> src)
        {
            var fis = dst.GetType().UnproxyType().GetFields().ToList();
            foreach (var fi in fis)
            {
                var srcVal = (dynamic)fi.GetValue(src) as IComparable;
                fi.SetValue(dst, srcVal);
            }
        }

        public static void CopyProperties<T>(this EntityBase<T> dst, EntityBase<T> src)
        {
            var pis = dst.GetType().UnproxyType().GetProperties().ToList();
            foreach (var pi in pis)
            {
                var srcVal = (dynamic)pi.GetValue(src);
                pi.SetValue(dst, srcVal);
            }
        }

        static Type UnproxyType(this Type t)
        {
            if (t.Namespace == PROXY_NAMESPACE)
                t = t.BaseType;
            return t;
        }

        static List<FieldInfo> GetCommonFields(Type a, Type b)
        {
            a = a.UnproxyType();
            b = b.UnproxyType();

            var cliFis = a.GetType().UnproxyType().GetFields().ToList();
            var srvFis = b.GetType().UnproxyType().GetFields().ToList();
            var result = cliFis.Intersect(srvFis).ToList();
            return result;
        }

        static List<PropertyInfo> GetCommonProperties(Type a, Type b)
        {
            a = a.UnproxyType();
            b = b.UnproxyType();

            var aProps = a.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty).OrderBy(x => x.Name).ToList();
            var bProps = b.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty).OrderBy(x => x.Name).ToList();
            var result = aProps.Intersect(bProps, new PropertyInfoComparer()).ToList();

            var exclude = (
                from pi in result
                from ca in pi.CustomAttributes
                where ca.AttributeType == typeof(JsonIgnoreAttribute) 
                    || ca.AttributeType == typeof(NavigationPropertyAttribute) 
                select (pi)).ToList();
            exclude.ForEach(x => result.Remove(x));

            return result;
        }

        public static bool Equals<T>(this EntityBase<T> server, EntityBase<T> client, bool traverse, EntityChangeResults auditResult = null)
        {
            var cliCopy = client;
            var srvCopy = server;

            if (null != auditResult && auditResult.Entities.Contains(srvCopy))
                srvCopy = null;
            if (null != auditResult && auditResult.Entities.Contains(cliCopy))
                cliCopy = null;

            var bothNotNull = ((srvCopy != null) && (cliCopy != null));
            bool result = ((srvCopy == cliCopy) || bothNotNull);

            if (bothNotNull)
            {
                if (client is IngredientItem)
                    new object();
                if (cliCopy.PrimaryKey != srvCopy.PrimaryKey)
                {
                    result = false;
                }
                else
                {
                    result = CompareFields(srvCopy, cliCopy, auditResult);
                    if (result || null != auditResult)
                    {
                        result = CompareProperties(srvCopy, cliCopy, traverse, auditResult);
                    }
                }
            }

            return result;
        }

        public static bool IsProxy<T>(this EntityBase<T> entity)
            where T : EntityBase<T>
        {
            var result = false;
            if (PROXY_NAMESPACE == entity.GetType().Namespace)
            {
                result = true;
            }
            return result;
        }

        public static bool Equals<T>(this IEnumerable<T> srvList, IEnumerable<T> cliList, bool unused, EntityChangeResults auditResult = null)
            where T : EntityBase<T>
        {
            bool result = Object.ReferenceEquals(cliList, srvList);
            if (!result && null != cliList)
            {
                var cliCopy = new List<T>(cliList);
                var srvCopy = new List<T>(srvList);

                var before = cliCopy.Count;
                cliCopy = cliCopy.Except(auditResult.Entities.OfType<T>()).ToList();
                var after = cliCopy.Count;
                if (before - after > 0)
                    Debug.WriteLine(string.Format("Ignored {0} audited entities.", before - after));

                before = srvCopy.Count;
                srvCopy = srvCopy.Except(auditResult.Entities.OfType<T>()).ToList();
                after = srvCopy.Count;
                if (before - after > 0)
                    Debug.WriteLine(string.Format("Ignored {0} audited entities.", before - after));

                var deepComparer = new DeepEqualityComparer<T>(auditResult);
                var additions = cliCopy.Where(x => !srvCopy.Contains<T>(x, deepComparer)).ToList();
                if (null != auditResult && additions.Count > 0)
                {
                    foreach (var addition in additions)
                    {
                        if (!auditResult.Entities.Contains(addition))
                        { 
                            if (!addition.IsProxy())
                            {
                                auditResult.Added(addition, addition.PrimaryKey, EntityState.Added);
                            }
                        }
                    }
                }

                var deletions = srvCopy.Where(x => !cliCopy.Contains(x, deepComparer)).ToList();
                if (null != auditResult && deletions.Count > 0)
                {
                    foreach (var deletion in deletions)
                    {
                        if (!auditResult.Entities.Contains(deletion))
                        {
                            if (!deletion.IsProxy())
                            {
                                auditResult.Deleted(deletion, deletion.PrimaryKey, EntityState.Deleted);
                            }
                        }
                    }
                }

                var ignore = additions.Concat(deletions).ToList();
                var shallowComparer = new ShallowEqualityComparer<T>();
                cliCopy = cliCopy.Except(ignore, shallowComparer).ToList();
                srvCopy = srvCopy.Except(ignore, shallowComparer).ToList();

                if (cliCopy is IEnumerable<IngredientItem>)
                    new object();
                var changes = srvCopy.Except(cliCopy, deepComparer).ToList();
                if (null != auditResult && changes.Count > 0)
                {
                    changes.ForEach(x => auditResult.Modified(x, x.PrimaryKey, EntityState.Modified));
                }

                if (additions.Count == 0
                    && deletions.Count == 0
                    && changes.Count == 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool CompareFields<T>(this EntityBase<T> server, EntityBase<T> client, EntityChangeResults auditResult = null)
        {
            bool result = true;
            var cliCopy = client;
            var srvCopy = server;

            var fis = GetCommonFields(cliCopy.GetType().UnproxyType(), srvCopy.GetType().UnproxyType());
            foreach (var fi in fis)
            {
                var cliVal = (dynamic)fi.GetValue(cliCopy) as IComparable;
                var srvVal = (dynamic)fi.GetValue(srvCopy) as IComparable;

                if (((object)cliVal) == null || ((object)srvVal) == null)
                {
                    result = Object.Equals(cliVal, srvVal);
                    if (null != auditResult && !result)
                    {
                        auditResult.Modified(cliCopy, cliCopy.PrimaryKey, EntityState.Modified, fi.Name, srvVal.ToString(), cliVal.ToString());
                    }
                }
                else if (!cliVal.Equals(srvVal))
                {
                    result = false;
                    if (null != auditResult)
                    {
                        auditResult.Modified(cliCopy, cliCopy.PrimaryKey, EntityState.Modified, fi.Name, srvVal.ToString(), cliVal.ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public static bool CompareProperties<T>(this EntityBase<T> server, EntityBase<T> client, bool traverse, EntityChangeResults auditResult = null)
        {
            bool result = true;
            if (server is Recipe)
                new Object();
            var pis = GetCommonProperties(client.GetType().UnproxyType(), server.GetType().UnproxyType());
            foreach (var pi in pis)
            {
                var cliVal = (dynamic)pi.GetValue(client);
                var srvVal = (dynamic)pi.GetValue(server);

                if (cliVal is string)
                    new Object();
                if (cliVal is EntityBase)
                {
                    result &= EntityExtensions.Equals(srvVal, cliVal, traverse, auditResult);
                }
                else if (cliVal is IEnumerable && !(cliVal is String))
                {
                    if (traverse)
                    {
                        if (null != auditResult)
                        {
                            EntityExtensions.Equals(srvVal, cliVal, traverse, auditResult);
                            new Object();
                        }
                        else
                            result &= EntityExtensions.Equals(srvVal, cliVal, traverse, auditResult);

                    }
                }
                else
                {
                    if (client.PrimaryKey == server.PrimaryKey)
                    {
                        result &= (cliVal == srvVal);
                        if ((cliVal != srvVal) && null != auditResult)
                        {
                            auditResult.Modified(client,
                                client.PrimaryKey, 
                                EntityState.Modified,
                                pi.Name,
                                (null == srvVal) ? null : srvVal.ToString(),
                                (null == cliVal) ? null : cliVal.ToString());
                        }
                    }
                }
                if (!result && null == auditResult)
                {
                    break;
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
        
        class DeepEqualityComparer<T> : IEqualityComparer<T>
            where T : EntityBase<T>
        {
            EntityChangeResults AuditResult { get; set; }
            public DeepEqualityComparer(EntityChangeResults auditResult)
            {
                this.AuditResult = auditResult;
            }

            public bool Equals(T x, T y)
            {
                if (x is IngredientItem)
                    new object();
                var result = EntityExtensions.Equals(x, y, true, this.AuditResult);
                if (!result)
                    new object();
                return result;
            }

            public int GetHashCode(T obj)
            {
                var result = obj.PrimaryKey.GetHashCode();
                if (obj is IngredientItem)
                    Debug.WriteLine(string.Format("{0}: {1}: {2}", obj.PrimaryKey.ToString(), result.ToString(), obj.ToString()));
                return result;
            }

        }//class
        class ShallowEqualityComparer<T> : IEqualityComparer<T>
            where T : EntityBase<T>
        {
            public bool Equals(T x, T y)
            {
                if (x is PlannerGroup)
                    new object();
                var result = EntityExtensions.Equals(x, y, false);
                if (!result)
                    new object();
                return result;
            }

            public int GetHashCode(T obj)
            {
                return obj.PrimaryKey.GetHashCode();
            }

        }//class

    }//class
}
