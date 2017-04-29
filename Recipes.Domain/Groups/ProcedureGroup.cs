using System;
using System.Collections.Generic;

namespace Recipes.Domain
{
    [Serializable]
    public class ProcedureGroup : GroupBase<ProcedureGroup, ProcedureItem>
    {
        public int ProcedureGroupId { get; set; }

        public override int PrimaryKey
        {
            get
            {
                return ProcedureGroupId;
            }
        }

        public ProcedureGroup() : base()
        {
            this.Init();
        }
        public ProcedureGroup(string text)
            : base(text)
        {
            this.Init();
        }

        void Init()
        {
        }


    }//class
}//ns
