﻿using System;

namespace Recipes.Domain
{
    [Serializable]
    abstract public class GroupItemBase
    {
        virtual public string Text { get; set; }

        public GroupItemBase()
        {

        }
        public GroupItemBase(string text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }
    }//class
}//ns