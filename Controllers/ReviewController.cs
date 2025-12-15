using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ReviewController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Create(int companyId)
    {
        if (companyId == 0)
        {
            return BadRequest("Company ID is missing.");
        }

        ViewBag.CompanyId = companyId;
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int companyId, Review review)
    {
        review.UserId = _userManager.GetUserId(User);
        Console.WriteLine(review.UserId);
        
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }

            ViewBag.CompanyId = companyId;
            return View(review);
        }
        
        review.CompanyId = companyId;
        review.CreatedAt = DateTime.Now;

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Company", new { id = companyId });
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        if (review.UserId != _userManager.GetUserId(User) && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        return View(review);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        if (review.UserId != _userManager.GetUserId(User) && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Company", new { id = review.CompanyId });
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        Console.WriteLine($"Editing id: {id}");
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return NotFound();
        }

        if (review.UserId != _userManager.GetUserId(User))
        {
            return Forbid();
        }

        return View(review);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Review review)
    {
        var existingReview = await _context.Reviews.FindAsync(review.Id);
        if (existingReview == null)
        {
            return NotFound();
        }

        review.UserId = existingReview.UserId;
        existingReview.Rating = review.Rating;
        existingReview.Comment = review.Comment;
        
        _context.Update(existingReview);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Company", new { id = review.CompanyId });
    }

}
