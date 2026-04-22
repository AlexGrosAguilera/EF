using System;
using System.Collections.Generic;

namespace DB_FIRST_DEMO.INFRASTRUCTURE.DATA;

public partial class Job
{
    public string JobId { get; set; } = null!;

    public string JobTitle { get; set; } = null!;

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<JobHistory> JobHistories { get; set; } = new List<JobHistory>();
}
