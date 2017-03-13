using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Domain
{
    public class IngredientGroup
    {
        public int IngredientGroupId { get; set; }
        public string Text { get; set; }
        public List<IngredientGroupItem> Items { get; set; }

        public IngredientGroup()
        {
            this.Items = new List<IngredientGroupItem>();
        }
        public IngredientGroup(string text)
            : this()
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("parameter text is null or Empty.");

            this.Text = text;
        }

        public void Add(IngredientGroupItem i)
        {
            if (null == i)
                throw new ArgumentNullException("IngredientGroupItem i");

            this.Items.Add(i);
        }

        public void Add(string igiText)
        {
            if (string.IsNullOrEmpty(igiText))
                throw new ArgumentException("parameter igiText is null or Empty.");

            var i = new IngredientGroupItem(igiText);
            this.Add(i);
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }
    }//class
}//ns
