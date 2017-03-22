using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingListGroup : GroupBase<ShoppingListItem>
    {
        public int ShoppingListGroupId { get; set; }
        public List<int> CheckedItems { get; set; }

        public ShoppingListGroup() : base()
        {   }
    }
}
