using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryDomain.Model;

public partial class Country
{
    public Country()
    {
        Volunteers = new HashSet<Volunteer>();
    }

    [Required(ErrorMessage = "This field cannot be left blank")] 
    [Display(Name = "Country Code")]
    public int CountryCode { get; set; }
    
    
    [Required(ErrorMessage = "This field cannot be left blank")]
    [Display(Name = "Country")]
    public string? CountryName { get; set; }
    
    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
