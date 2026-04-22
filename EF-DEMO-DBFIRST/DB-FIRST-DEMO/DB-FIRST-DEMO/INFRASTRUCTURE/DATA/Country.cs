using System;
using System.Collections.Generic;

namespace DB_FIRST_DEMO.INFRASTRUCTURE.DATA;

public partial class Country
{
    public string CountryId { get; set; } = null!;

    public string? CountryName { get; set; }

    public int? RegionId { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual Region? Region { get; set; }
}
