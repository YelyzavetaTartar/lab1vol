using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDomain.Model;

public partial class Country
{
    public Country()
    {
        Volunteers = new HashSet<Volunteer>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required(ErrorMessage = "This field cannot be left blank")] 
    [Display(Name = "Country Code")]
    public string CountryCode { get; set; } = null!;


    [Required(ErrorMessage = "This field cannot be left blank")]
    [Display(Name = "Country")]
    public string? CountryName { get; set; }
    
    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
