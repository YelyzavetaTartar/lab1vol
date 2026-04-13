using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Language
{
    public int Name { get; set; }

    public string? LanguageLevel { get; set; }

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
