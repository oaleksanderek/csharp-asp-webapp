using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;

public class CompanyController : Controller
{
    private readonly ApplicationDbContext _context;

    public CompanyController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var companies = await _context.Companies.ToListAsync();
        return View(companies);
    }

    public async Task<IActionResult> Details(int id)
    {
        var company = await _context.Companies
            .Include(c => c.Reviews)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }
}