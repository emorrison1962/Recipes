using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Recipes.Domain
{
    public class EntityChangeResults
    {
        public HashSet<EntityBase> Entities { get; set; }
        public HashSet<ModifiedEntity> ModifiedEntities { get; set; }
        public EntityChangeResults()
        {
            this.ModifiedEntities = new HashSet<ModifiedEntity>();
            this.Entities = new HashSet<EntityBase>();
        }

        void Add(EntityBase entity, int primaryKey, EntityState entityState, string property = null, string oldValue = null, string newValue = null)
        {
            if (this.Entities.Contains(entity))
                Debug.Assert(false);

            var me = new ModifiedEntity(entity, primaryKey, entityState, property, oldValue, newValue);
            this.ModifiedEntities.Add(me);
            this.Entities.Add(entity);
            return;
        }

        public void Modified(EntityBase entity, int primaryKey, EntityState entityState, string property = null, string oldValue = null, string newValue = null)
        {
            this.Add(entity, primaryKey, entityState, property, oldValue, newValue);
        }

        public void Added(EntityBase entity, int primaryKey, EntityState entityState)
        {
            this.Add(entity, primaryKey, entityState, null, null, null);
        }

        public void Deleted(EntityBase entity, int primaryKey, EntityState entityState)
        {
            this.Add(entity, primaryKey, entityState, null, null, null);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            foreach (var me in ModifiedEntities)
            {
                sb.AppendFormat("\t{0}", me.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }//class
    public class ModifiedEntity
    {
        public EntityBase Entity { get; set; }
        public int PrimaryKey { get; set; }
        public string ClassName { get { return this.Entity.GetType().Name; } }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public EntityState EntityState { get; set; }

        public ModifiedEntity(EntityBase entity, int primaryKey, EntityState entityState, string propertyName, string oldValue, string newValue)
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
                result = string.Format("{1}({2}): {3} {4}",
                    base.ToString(),
                    this.Entity.GetType().Name,
                    this.PrimaryKey,
                    this.PropertyName,
                    this.EntityState.ToString());
            else
                result = string.Format("{1}({2}):{3} {4} from {5} to {6}",
                    base.ToString(),
                    this.Entity.GetType().Name,
                    this.PrimaryKey,
                    this.PropertyName,
                    this.EntityState.ToString(),
                    this.OldValue,
                    this.NewValue);

            return result;
        }
    }//class

}//ns
