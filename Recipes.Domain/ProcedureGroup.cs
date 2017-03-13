using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    public class ProcedureGroup
    {
        public int ProcedureGroupId { get; set; }
        public string Text { get; set; }
        public List<ProcedureGroupItem> Items { get; set; }


        public ProcedureGroup()
        {
            this.Items = new List<ProcedureGroupItem>();
        }
        public ProcedureGroup(string text)
            : this()
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("parameter text is null or Empty.");

            this.Text = text;
        }

        public void Add(ProcedureGroupItem i)
        {
            if (null == i)
                throw new ArgumentNullException("parameter igiText is null or Empty.");

            this.Items.Add(i);
        }

        public void Add(string pgiText)
        {
            if (string.IsNullOrEmpty(pgiText))
                throw new ArgumentException("parameter igiText is null or Empty.");

            var i = new ProcedureGroupItem(pgiText);
            this.Add(i);
        }


        public override string ToString()
        {
            return base.ToString() + string.Format(", Text={0}", this.Text);
        }

    }//class
}//ns
