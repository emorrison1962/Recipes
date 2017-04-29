using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    [Serializable]
    abstract public class GroupBase<T, I> : EntityBase<T> where I: GroupItemBase<I>, new()
	{
		virtual public string Text { get; set; }
		public List<I> Items { get; set; }

        public GroupBase()
        {
            this.Items = new List<I>();
        }
        public GroupBase(string text)
            : this()
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("parameter text is null or Empty.");

            this.Text = text;
        }

        public void Add(I i)
        {
            if (null == i)
                throw new ArgumentNullException("parameter igiText is null or Empty.");

            this.Items.Add(i);
        }

        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }

    }
}
