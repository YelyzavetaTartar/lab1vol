using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Country
{
    public int CountryCode { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
