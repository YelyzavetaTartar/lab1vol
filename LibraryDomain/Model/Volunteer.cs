using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LibraryDomain.Model;

public partial class Volunteer: Entity
{
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name="Full name")]
    public string? FullName { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Country Code")]
    public string CountryCode { get; set; } = null!;

    [Display(Name = "Country")]
    public virtual Country CountryCodeNavigation { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual GeneralStaff? GeneralStaff { get; set; }

    public virtual Medic? Medic { get; set; }

    public virtual Translator? Translator { get; set; }

    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
