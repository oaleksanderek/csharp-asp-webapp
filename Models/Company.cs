namespace WebApp.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Company
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Comapny name is required.")]
    [StringLength(100, ErrorMessage = "Company name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [StringLength(50, ErrorMessage = "Industry name cannot be longer than 50 characters.")]
    public string Industry { get; set; }

    [Range(1800, 2100, ErrorMessage = "Founding year must be at least 1800.")]
    public int? FoundingYear { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}