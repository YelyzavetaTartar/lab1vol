using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Driver: Entity
{
    
    public string? DrivingLicenseId { get; set; }

    public string? DrivingLicenseCategories { get; set; }

    public virtual Volunteer IdVolunteerNavigation { get; set; } = null!;
}
