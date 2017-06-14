using Newtonsoft.Json;
using Recipes.Contracts;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        abstract public int PrimaryKey { get; }
        abstract public string Text { get; set; }

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
        public EntityChangeResults DetectChanges(EntityBase<T> client)
        {
            if (this.IsProxy())
                throw new NotSupportedException("\"this\" must not be a \"System.Data.Entity.DynamicProxies\". Please detach object first.");
            // there is an expectation that "other" is the client object.
            var result = new EntityChangeResults();

            var cliGraph = client.GetEntityGraph();
            var srvGraph = this.GetEntityGraph();

            var comparer = new EntityBaseEqualityComparer<EntityBase>();

            var additions = cliGraph.Where(x => !srvGraph.Contains<EntityBase>(x, comparer)).ToList();
            additions.ForEach(x => result.Added(x));

            var deletions = srvGraph.Where(x => !cliGraph.Contains<EntityBase>(x, comparer)).ToList();
            deletions.ForEach(x => result.Deleted(x));

            new object();

            var hasChanged = EntityExtensions.Equivalent((dynamic)this, (dynamic)client, true, result);
            return result;
        }

        public bool IsProxy()
        {
            var result = false;
            if (Constants.PROXY_NAMESPACE == this.GetType().Namespace)
            {
                result = true;
            }
            return result;
        }

        public bool Equals(EntityBase<T> other)
        {
            var result = this.PrimaryKey == other.PrimaryKey;
            if (result)
            {
                result = EntityExtensions.Equivalent(this, other, true);
            }
            return result;
        }

        public override string ToString()
        {
            var result = string.Format("{0}, PrimaryKey={1}, Text={2}", base.ToString(), this.PrimaryKey, this.Text);
            return result;
        }

        public override int GetHashCode()
        {
            var result = int.MinValue;
            result = this.PrimaryKey.GetHashCode();
            return result;
        }

    }//class



}//ns
