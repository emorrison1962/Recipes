using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Models
{
    [Serializable]
    public class ShoppingListVM
    {
        IServiceBase<ShoppingList> _shoppingListService;

        public IServiceBase<ShoppingList> ShoppingListService
        {
            get
            {
                if (null == _shoppingListService)
                    _shoppingListService = Unity.Resolve<IServiceBase<ShoppingList>>();
                return _shoppingListService;
            }
            set { _shoppingListService = value; }
        }
        public List<int> Items { get; set; }

        public ShoppingListVM()
        {
            this.Items = new List<int>();
            var sl = this.ShoppingListService.GetAll().FirstOrDefault();
            if (null != sl)
            {
                var list = sl.Items.ToList();
                list.ForEach(x => this.Items.Add(x.IngredientGroupItemId));
            }

        }
    }//class
}//ns