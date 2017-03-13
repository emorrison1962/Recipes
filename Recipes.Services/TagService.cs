using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System.Collections.Generic;

namespace Recipes.Services
{
    public class TagService : ServiceBase<Tag>, IServiceBase<Tag>
    {
        IRepositoryBase<Tag> Repository { get; set; }

        public TagService(IRepositoryBase<Tag> r)
            : base(r)
        {
            this.Repository = r;
        }

        override public void Delete(Tag entity)
        {
            this.Repository.Delete(entity);
            this.Repository.Commit();
        }

        override public void Delete(int id)
        {
            this.Repository.Delete(id);
            this.Repository.Commit();
        }

        override public IEnumerable<Tag> GetAll()
        {
            var result = this.Repository.GetAll();
            return result;
        }

        override public Tag GetById(int id)
        {
            var result = this.Repository.GetById(id);
            return result;
        }

        override public Tag Insert(Tag entity)
        {
            this.Repository.Insert(entity);
            this.Repository.Commit();
            return entity;
        }

        override public Tag Update(Tag entity)
        {
            this.Repository.Update(entity);
            this.Repository.Commit();
            return entity;
        }
    }
}
