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
        static IsSameDatabaseEntityComparer<EntityBase> _eComparer;

        static ModifiedEntityLookupComparer<ModifiedEntity> _meComparer;
        IsSameDatabaseEntityComparer<EntityBase> EntityComparer
        {
            get
            {
                if (null == _eComparer)
                    _eComparer = new IsSameDatabaseEntityComparer<EntityBase>();
                return _eComparer;
            }
        }
        ModifiedEntityLookupComparer<ModifiedEntity> ModifiedEntityComparer
        {
            get
            {
                if (null == _meComparer)
                    _meComparer = new ModifiedEntityLookupComparer<ModifiedEntity>();
                return _meComparer;
            }
        }

        public HashSet<EntityBase> Entities { get; set; }
        public HashSet<ModifiedEntity> ModifiedEntities { get; set; }
        public EntityChangeResults()
        {
            this.ModifiedEntities = new HashSet<ModifiedEntity>();
            this.Entities = new HashSet<EntityBase>();
        }

        void Add(EntityBase entity, int primaryKey, EntityState entityState, string property = null, string oldValue = null, string newValue = null)
        {
            var me = new ModifiedEntity(entity, primaryKey, entityState, property, oldValue, newValue);

            if (this.ModifiedEntities.Contains(me, ModifiedEntityComparer))
            {
                var where = new Func<ModifiedEntity, bool>( delegate (ModifiedEntity candidate) 
                {
                    var result = false;
                    if (candidate.GetType().Name == me.GetType().Name)
                        if (candidate.PrimaryKey == me.PrimaryKey)
                            result = true;
                    return result;
                });
                var existing = this.ModifiedEntities.Where(x => where(x)).FirstOrDefault();
                if (existing.EntityState == EntityState.Added
                    || existing.EntityState == EntityState.Deleted)
                {
                    if (me.EntityState == EntityState.Added
                        || me.EntityState == EntityState.Deleted)
                    {
                        if (me.EntityState != existing.EntityState)
                        {
                            existing.EntityState = EntityState.Modified;
                        }
                        else
                        {
                            Debug.Assert(false);
                        }
                    }
                }
            }
            else
            {
                this.ModifiedEntities.Add(me);
                this.Entities.Add(entity);
            }
            return;
        }

        public void Modified(EntityBase entity, string property = null, string oldValue = null, string newValue = null)
        {
            this.Add(entity, entity.PrimaryKey, EntityState.Modified, property, oldValue, newValue);
        }

        public void Added(EntityBase entity)
        {
            this.Add(entity, entity.PrimaryKey, EntityState.Added);
        }

        public void Deleted(EntityBase entity)
        {
            this.Add(entity, entity.PrimaryKey, EntityState.Deleted);
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
                    this.Entity.ToString(),
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
    class ModifiedEntityLookupComparer<T> : IEqualityComparer<T>
    where T : ModifiedEntity
    {
        public bool Equals(T x, T y)
        {
            var result = x.Entity.GetType() == y.Entity.GetType();
            if (result)
            {
                result = x.Entity.PrimaryKey == y.Entity.PrimaryKey;
            }
            return result;
        }

        public int GetHashCode(T obj)
        {
            int result = obj.Entity.PrimaryKey
                ^ obj.Entity.GetType().Name.GetHashCode();
            return result;
        }
    }

}//ns
