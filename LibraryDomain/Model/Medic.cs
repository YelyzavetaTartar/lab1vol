using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Medic: Entity
{
   
    public string? DiplomaNumber { get; set; }

    public string? Specialization { get; set; }

    public virtual Volunteer IdVolunteerNavigation { get; set; } = null!;
}
