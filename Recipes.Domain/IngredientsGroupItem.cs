using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
    public class IngredientGroupItem : GroupItemBase
    {

        #region Properties

        public int IngredientGroupItemId { get; set; }

        public int? IngredientGroupRefId { get; set; }

        [ForeignKey("IngredientGroupRefId")]
        [JsonIgnore]
        public virtual IngredientGroup IngredientGroup { get; set; }
        [JsonIgnore]
        public HashSet<ShoppingList> ShoppingLists { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public Amount Amount { get; set; }

        #endregion

        public IngredientGroupItem()
        {

        }
        public IngredientGroupItem(string text)
        {
            this.Text = text;
            //this.Product = new Domain.Product(text);
        }
        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }

    }//class
}//ns
