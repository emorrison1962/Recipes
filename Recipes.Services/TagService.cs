using Recipes.Contracts.Repositories;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System.Collections.Generic;

namespace Recipes.Services
{
    public class TagService : ServiceBase<Tag>, IServiceBase<Tag>
    {
        public TagService(IRepositoryBase<Tag> r)
            : base(r)
        {
        }
    }
}
