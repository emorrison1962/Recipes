using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class IngredientItem : GroupItemBase<IngredientItem>
    {

        #region Properties

        public int IngredientItemId { get; set; }

        [JsonIgnore]
        [ForeignKey("IngredientGroupId")]
        public virtual IngredientGroup IngredientGroup { get; set; }
        public int? IngredientGroupId { get; set; }
        
        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public Amount Amount { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return IngredientItemId;
            }
        }

        #endregion

        public IngredientItem()
        {
            this.Init();
        }
        public IngredientItem(string text)
        {
            this.Init();
            this.Text = text;
            //this.Product = new Domain.Product(text);
        }
        void Init()
        {
        }


        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            this.Init();
            if (null != this.IngredientGroup)
            {
                this.IngredientGroupId = this.IngredientGroup.IngredientGroupId;
                this.IngredientGroup = null;
            }
        }
    }//class
}//ns
