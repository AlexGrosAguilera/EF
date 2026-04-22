using System;
using System.Collections.Generic;

namespace DB_FIRST_DEMO.INFRASTRUCTURE.DATA;

public partial class Region
{
    public int RegionId { get; set; }

    public string? RegionName { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
