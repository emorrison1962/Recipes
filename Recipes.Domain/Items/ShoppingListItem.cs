using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingListItem : GroupItemBase<ShoppingListItem>
    {
        public int ShoppingListItemId { get; set; }
        public bool IsChecked { get; set; }

        //Navigation property
        [JsonIgnore]
        public virtual ShoppingListGroup ShoppingListGroup { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return ShoppingListItemId;
            }
        }

        public ShoppingListItem()
        {
            this.Init();
        }
        void Init()
        {
        }

    }
}
