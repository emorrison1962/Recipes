using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Reflection;

namespace Recipes.Domain
{
    public class AuditResult
    {
        public HashSet<EntityBase> AuditedItems { get; set; }
        public HashSet<Delta> Deltas { get; set; }
        public AuditResult()
        {
            this.Deltas = new HashSet<Delta>();
            this.AuditedItems = new HashSet<EntityBase>();
        }
        public void Add(EntityBase e, EntityState entityState, string property = null, string oldValue = null, string newValue = null)
        {
            if (e.GetType() == typeof(PlannerGroup))
                new object();
            var delta = new Delta(e, entityState, property, oldValue, newValue);
            this.Deltas.Add(delta);
            this.AuditedItems.Add(e);
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
        EntityBase Entity { get; set; }
        public string ClassName { get { return this.Entity.GetType().Name; } }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public EntityState EntityState { get; set; }

        public Delta(EntityBase entity, EntityState entityState, string propertyName, string oldValue, string newValue)
        {
            this.Entity = entity;
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
                    this.Entity.GetType().Name,
                    this.PropertyName,
                    this.EntityState.ToString());
            else
                result = string.Format("{0}, {1}:{2} {3} from {4} to {5}",
                    base.ToString(),
                    this.Entity.GetType().Name,
                    this.PropertyName,
                    this.EntityState.ToString(),
                    this.OldValue,
                    this.NewValue);

            return result;
        }
    }//class

}//ns
