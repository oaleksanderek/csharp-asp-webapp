using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
    public ICollection<Review> Reviews { get; set; }
}