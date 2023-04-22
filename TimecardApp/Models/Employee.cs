using System;
using System.Collections.Generic;

namespace TimecardApp.Models;

public partial class Employee
{
    public int EId { get; set; }

    public string EFirstName { get; set; } = null!;

    public string ELastName { get; set; } = null!;

    public string EPassword { get; set; } = null!;

    public string ETitle { get; set; } = null!;

    public int? ManagerId { get; set; }

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Employee? Manager { get; set; }

    public virtual ICollection<TimeRecord> TimeRecords { get; set; } = new List<TimeRecord>();
}
