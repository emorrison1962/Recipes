using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingListItem : GroupItemBase
    {
        public int ShoppingListItemId { get; set; }
        public bool IsChecked { get; set; }
        [JsonIgnore][XmlIgnore]
        public virtual ShoppingListGroup ShoppingListGroup { get; set; }
        [Required]
        public IngredientItem IngredientItem { get; set; }

    }
}
