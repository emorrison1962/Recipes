using System.Collections.Generic;
using System.Data;
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
        public void Add(EntityBase e, EntityState entityState, string property = null, string oldValue = null, string newValue = null)
        {
            var me = new ModifiedEntity(e, entityState, property, oldValue, newValue);
            this.ModifiedEntities.Add(me);
            this.Entities.Add(e);
            return;
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
        public string ClassName { get { return this.Entity.GetType().Name; } }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public EntityState EntityState { get; set; }

        public ModifiedEntity(EntityBase entity, EntityState entityState, string propertyName, string oldValue, string newValue)
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
