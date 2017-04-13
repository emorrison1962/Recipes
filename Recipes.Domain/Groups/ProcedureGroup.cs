using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    [Serializable]
    public class ProcedureGroup : GroupBase<ProcedureGroup, ProcedureItem>
    {
        public int ProcedureGroupId { get; set; }

        public ProcedureGroup() : base()
        {  }
        public ProcedureGroup(string text)
            : base(text)
        {  }


    }//class
}//ns
