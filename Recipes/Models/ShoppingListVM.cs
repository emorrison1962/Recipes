using Recipes.Contracts;
using Recipes.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Models
{
    [Serializable]
    public class ShoppingListVM : IShoppingListShallow
    {
        IShoppingListService _shoppingListService;

        public IShoppingListService ShoppingListService
        {
            get
            {
                if (null == _shoppingListService)
                    _shoppingListService = Unity.Resolve<IShoppingListService>();
                return _shoppingListService;
            }
            set { _shoppingListService = value; }
        }
        public int ShoppingListId { get; set; }
        public List<int> Items { get; set; }

        public ShoppingListVM()
        {
            this.Items = new List<int>();
            var sl = this.ShoppingListService.GetAll().FirstOrDefault(); "ShoppingList >> IngredientGroupItem needs to be a many to many relationship"
            if (null != sl)
            {
                var list = sl.Items.ToList();
                this.ShoppingListId = sl.ShoppingListId;
                list.ForEach(x => this.Items.Add(x.IngredientGroupItemId));
            }
        }

    }//class
}//ns