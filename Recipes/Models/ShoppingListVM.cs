using Recipes.Contracts;
using Recipes.Contracts.Services;
using Recipes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Models
{
    [Serializable]
    public class ShoppingListVM //: IShoppingListShallow
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
        public List<IngredientGroupItem> Items { get; set; }

        public ShoppingListVM()
        {
            this.Items = new List<IngredientGroupItem>();
        }

        public void Load()
        {
            var slim = this.ShoppingListService.GetAll().FirstOrDefault();
            if (null != slim)
            {
                var sl = this.ShoppingListService.GetFullObject(slim.ShoppingListId);

                var list = sl.Items.ToList();
                this.ShoppingListId = sl.ShoppingListId;
                list.ForEach(x => x.IsChecked = true);
                list.ForEach(x => this.Items.Add(x));
            }
        }

    }//class

}//ns