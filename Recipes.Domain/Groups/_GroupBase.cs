using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    [Serializable]
    abstract public class GroupBase<T, I> : EntityBase<T> where I : GroupItemBase<I>, new()
    {
        virtual public string Text { get; set; }

        public List<I> Items { get; set; }

        abstract public void Add(I item);
        abstract public void Remove(I item);

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

        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }

    }
}
