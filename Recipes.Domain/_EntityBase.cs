using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
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

        public EntityBase()
        {
            this.Init();
        }

        void Init()
        {
            this._instanceID = ++_nextInstanceID;
        }

    }

    abstract public partial class EntityBase<T> : EntityBase, IEquatable<EntityBase<T>>
    {
        [NotMapped]
        [JsonIgnore]
        abstract public int PrimaryKey { get; }

        public EntityChangeResults DetectChanges(EntityBase<T> client)
        {
            // there is an expectation that "other" is the client object.
            var result = new EntityChangeResults();
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
