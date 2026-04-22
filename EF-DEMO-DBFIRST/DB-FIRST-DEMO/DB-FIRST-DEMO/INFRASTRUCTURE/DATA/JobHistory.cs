using System;
using System.Collections.Generic;

namespace DB_FIRST_DEMO.INFRASTRUCTURE.DATA;

public partial class JobHistory
{
    public int EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string JobId { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;
}
