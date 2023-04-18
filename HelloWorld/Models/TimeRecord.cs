using System;
using System.Collections.Generic;

namespace HelloWorld.Models;

public partial class TimeRecord
{
    public DateTime TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public int EId { get; set; }

    public sbyte? IsStarred { get; set; }

    public virtual Employee EIdNavigation { get; set; } = null!;
}
