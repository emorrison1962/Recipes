using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Domain
{
    public class Procedure
    {
        public HashSet<ProcedureGroup> Groups { get; set; }
    }
}
