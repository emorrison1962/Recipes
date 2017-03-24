using Recipes.Contracts;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Models
{
    [Serializable]
    [Obsolete("Obsolete", true)]
    class ShoppingListVM 
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
        public int ShoppingListId { get; set; }
        public List<IngredientItem> Items { get; set; }

        public ShoppingListVM()
        {
            this.Items = new List<IngredientItem>();
        }

        public void Load(bool isChecked = true)
        {
            var slim = this.ShoppingListService.GetAll().FirstOrDefault();
            if (null != slim)
            {
                var sl = this.ShoppingListService.GetFullObject(slim.ShoppingListId);
                throw new NotImplementedException();

                //var list = sl.Items.ToList();
                //this.ShoppingListId = sl.ShoppingListId;
                //list.ForEach(x => x.IsChecked = isChecked);
                //list.ForEach(x => this.Items.Add(x));
            }
        }

    }//class

}//ns