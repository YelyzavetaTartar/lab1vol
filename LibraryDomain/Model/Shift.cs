using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Shift: Entity
{
   
    public DateOnly? ShiftDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
