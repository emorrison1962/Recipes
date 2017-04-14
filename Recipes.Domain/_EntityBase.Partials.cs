using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace Recipes.Domain
{
    abstract public partial class EntityBase<T> : EntityBase, IEquatable<T>
    {
        public AuditResult AuditChanges(EntityBase<T> other)
        {
            // there is an expectation that this! is the server object.
            var result = new AuditResult();
            var hasChanged = EntityExtensions.Audit((dynamic)other, (dynamic)this, result);
            return result;
        }

    }

    public class AuditResult
    {
        public HashSet<Delta> Deltas { get; set; }
        public AuditResult()
        {
            this.Deltas = new HashSet<Delta>();
        }
        public void Add(string className, EntityState entityState, string propertyName = null, string oldValue = null, string newValue = null)
        {
            var delta = new Delta(className, entityState, propertyName, oldValue, newValue);
            this.Deltas.Add(delta);
            return;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            foreach (var delta in Deltas)
            {
                sb.AppendFormat("\t{0}", delta.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }//class
    public class Delta
    {
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public EntityState EntityState { get;set;}

        public Delta(string className, EntityState entityState, string propertyName, string oldValue, string newValue)
        {
            this.ClassName = className;
            this.PropertyName = propertyName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.EntityState = entityState;
        }
        public override string ToString()
        {
            var result = string.Empty;
            if (this.EntityState == EntityState.Added || this.EntityState == EntityState.Deleted)
                result = string.Format("{0}, {1}:{2} {3}", 
                    base.ToString(), 
                    this.ClassName,
                    this.PropertyName,
                    this.EntityState.ToString());
            else
                result = string.Format("{0}, {1}:{2} {3} from {4} to {5}", 
                    base.ToString(), 
                    this.ClassName,
                    this.PropertyName,
                    this.EntityState.ToString(),
                    this.OldValue, 
                    this.NewValue);

            return result;
        }
    }//class

    public static partial class EntityExtensions
    {
        public static bool Audit<T>(this EntityBase<T> x, EntityBase<T> y, AuditResult auditResult)
        {
            bool result = ((x == y) || ((x != null) && (y != null)));
            if (result)
            {
                if (null != x)
                {
                    result = AuditFields(x, y, auditResult);
                    result = AuditProperties(x, y, auditResult);
                    return result;
                }
                result = false;
            }
            return result;
        }

        public static bool AuditFields<T>(this EntityBase<T> client, EntityBase<T> server, AuditResult auditResult)
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
                    if (!result)
                    {
                        auditResult.Add(client.GetType().Name, EntityState.Modified, fi.Name, srvVal.ToString(), cliVal.ToString());
                    }
                }
                else if (!cliVal.Equals(srvVal))
                {
                    result = false;
                    auditResult.Add(client.GetType().Name, EntityState.Modified, fi.Name, srvVal.ToString(), cliVal.ToString());
                }
            }
            return result;
        }

        public static bool AuditProperties<T>(this EntityBase<T> client, EntityBase<T> server, AuditResult auditResult)
        {
            bool result = true;
            var pis = GetCommonProperties(client.GetType(), server.GetType());
            foreach (var pi in pis)
            {
                var cliVal = (dynamic)pi.GetValue(client);
                var srvVal = (dynamic)pi.GetValue(server);

                if (cliVal is EntityBase)
                {
                    result = EntityExtensions.Audit(cliVal, srvVal, auditResult);
                }
                else if (cliVal is IEnumerable && !(cliVal is String))
                {
                    result = EntityExtensions.Audit(cliVal, srvVal, auditResult);
                }
                else
                {
                    result = cliVal == srvVal;
                    if (!result)
                    {
                        auditResult.Add(client.GetType().Name,
                            EntityState.Modified,
                            pi.Name,
                            (null == srvVal) ? null : srvVal.ToString(),
                            (null == cliVal) ? null : cliVal.ToString());
                    }
                }
            }
            return result;
        }

        public static bool Audit<T>(this IEnumerable<T> cli, IEnumerable<T> srv, AuditResult auditResult)
        {
            bool result = Object.ReferenceEquals(cli, srv);
            if (!result && null != cli)
            {
                var cliList = new List<T>(cli.ToList());
                var srvList = new List<T>(srv.ToList());

                var comparer = new EqualityComparer<T>();
                var additions = cliList.Where(x => !srvList.Contains(x, comparer)).ToList();
                additions.ForEach(x => srvList.Add(x));
                additions.ForEach(x => auditResult.Add(typeof(T).Name, EntityState.Added));

                var deletions = srvList.Where(x => !cliList.Contains(x, comparer)).ToList();
                deletions.ForEach(x => srvList.Remove(x));
                deletions.ForEach(x => auditResult.Add(typeof(T).Name, EntityState.Deleted));

                var ignore = additions.Concat(deletions).ToList();

                var changes = srvList.Except(cliList, comparer).ToList();

                if (additions.Count > 0
                    || deletions.Count > 0
                    || changes.Count > 0)
                {
                    result = false;
                }
            }
            return result;
        }


    }//class

}//ns
