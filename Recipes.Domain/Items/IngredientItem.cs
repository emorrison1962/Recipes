using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Recipes.Domain
{
    [Serializable]
    public class IngredientItem : GroupItemBase
    {

        #region Properties

        public int IngredientItemId { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public virtual IngredientGroup IngredientGroup { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public Amount Amount { get; set; }

        #endregion

        public IngredientItem()
        {

        }
        public IngredientItem(string text)
        {
            this.Text = text;
            //this.Product = new Domain.Product(text);
        }

    }//class
}//ns
