using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class GeneralStaff: Entity
{
    
    public string? TypeOfWork { get; set; }

    public string? Experience { get; set; }

    public virtual Volunteer IdVolunteerNavigation { get; set; } = null!;
}
