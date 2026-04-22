using System;
using System.Collections.Generic;

namespace DB_FIRST_DEMO.INFRASTRUCTURE.DATA;

public partial class JobGrade
{
    public string? GradeLevel { get; set; }

    public int? LowestSal { get; set; }

    public int? HighestSal { get; set; }
}
