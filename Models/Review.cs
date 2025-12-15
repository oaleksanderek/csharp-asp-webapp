using WebApp.Validators;

namespace WebApp.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Review
{
    public int Id { get; set; }

    [ForeignKey("Company")]
    public int CompanyId { get; set; }

    [Required(ErrorMessage = "Comment is required.")]
    [NoProfanity]
    public string Comment { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Company? Company { get; set; }

    [ForeignKey("User")]
    public string? UserId { get; set; }

    public IdentityUser? User { get; set; }
}