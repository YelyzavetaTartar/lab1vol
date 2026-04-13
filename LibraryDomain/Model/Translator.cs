using System;
using System.Collections.Generic;

namespace LibraryDomain.Model;

public partial class Translator: Entity
{
    
    public string? LanguagePair { get; set; }

    public string? LanguageLevel { get; set; }

    public virtual Volunteer IdVolunteerNavigation { get; set; } = null!;
}
