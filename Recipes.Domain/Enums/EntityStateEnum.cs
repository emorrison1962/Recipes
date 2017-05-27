using System;

namespace Recipes.Domain
{
    [Flags]
    public enum EntityState
    {
        Added = 4,
        Deleted = 8,
        Detached = 1,
        Modified = 0x10,
        Unchanged = 2
    }
}

