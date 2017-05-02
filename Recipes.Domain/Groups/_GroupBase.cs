using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    [Serializable]
    abstract public class GroupBase<T, I> : EntityBase<T> where I: GroupItemBase<I>, new()
	{
        protected NotifyCollectionChangedEventHandler CollectionChanged;

        virtual public string Text { get; set; }
        protected ObservableCollection<I> _items { get; set; }
        public ObservableCollection<I> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (null != _items)
                    _items.CollectionChanged -= Items_CollectionChanged;
                _items = value;
                if (null != _items)
                    _items.CollectionChanged += Items_CollectionChanged;
            }
        }

        abstract protected void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e);

        abstract public void Add(I item);
        abstract public void Remove(I item);

        public GroupBase()
        {
            this._items = new ObservableCollection<I>();
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
