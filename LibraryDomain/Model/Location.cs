using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Location: Entity
{
   
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
