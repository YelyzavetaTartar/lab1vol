using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Volunteer: Entity
{
    
    public string? FullName { get; set; }

    public string? Email { get; set; }

    public int CountryCode { get; set; }

    public virtual Country CountryCodeNavigation { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual GeneralStaff? GeneralStaff { get; set; }

    public virtual Medic? Medic { get; set; }

    public virtual Translator? Translator { get; set; }

    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
