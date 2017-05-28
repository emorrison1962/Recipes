using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class ShoppingListItem : GroupItemBase<ShoppingListItem>
    {
        public int ShoppingListItemId { get; set; }
        public override int PrimaryKey
        {
            get
            {
                return ShoppingListItemId;
            }
        }
        public bool IsChecked { get; set; }
        [JsonIgnore]
        [ForeignKey("ShoppingListGroupId")]
        public virtual ShoppingListGroup ShoppingListGroup { get; set; }
        public int? ShoppingListGroupId { get; set; }


        public ShoppingListItem()
        {
            this.Init();
        }
        void Init()
        {
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            if (null != this.ShoppingListGroup)
            {
                this.ShoppingListGroupId = this.ShoppingListGroup.ShoppingListGroupId;
                this.ShoppingListGroup = null;
            }
        }

    }//class
}//ns
