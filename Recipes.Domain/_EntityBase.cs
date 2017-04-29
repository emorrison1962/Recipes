using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    abstract public class EntityBase
    {
        #region Fields

        [NotMapped]
        static int _nextInstanceID = 0;
        [NotMapped]
        int _instanceID;

        #endregion

        [NotMapped]
        [JsonIgnore]
        public bool IsDetached { get; set; }

        public EntityBase()
        {
            this.Init();
        }

        void Init()
        {
            this._instanceID = ++_nextInstanceID;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            this.IsDetached = true;
            Debug.WriteLine(this.GetType().Name);
        }
    }

    abstract public partial class EntityBase<T> : EntityBase, IEquatable<EntityBase<T>>
    {
        [NotMapped]
        [JsonIgnore]
        abstract public int PrimaryKey { get; }

        public AuditResult AuditChanges(EntityBase<T> client)
        {
            // there is an expectation that "other" is the client object.
            var result = new AuditResult();
            var hasChanged = EntityExtensions.Equals((dynamic)this, (dynamic)client, true, result);
            return result;
        }

        public bool Equals(EntityBase<T> other)
        {
            var result = this.PrimaryKey == other.PrimaryKey;
            if (result)
            {
                result = EntityExtensions.Equals((dynamic)this, (dynamic)other);
            }
            return result;
        }

        public override int GetHashCode()
        {
            var result = int.MinValue;

            result = this.PrimaryKey.GetHashCode();


#if false
            var type = this.GetType();
            var pis = type.GetProperties().ToList();

            var exclude = (
                from pi in pis
                from ca in pi.CustomAttributes
                where ca.AttributeType == typeof(JsonIgnoreAttribute)
                select (pi)).ToList();
            exclude.ForEach(x => pis.Remove(x));

            foreach (var pi in pis)
            {
                var val = pi.GetValue(this);
                if (val is IEnumerable && !(val is String))
                {
                    continue;
                }
                if (null != val)
                {
                    result ^= val.GetHashCode();
                }
                else
                {
                    result ^= int.MinValue;
                }
            }

            if (type == typeof(PlannerGroup))
            {
                Debug.WriteLine(string.Format("{0}, GetHashCode={1}", this.ToString(), result));
            }


#endif
            return result;
        }

    }//class



}//ns
